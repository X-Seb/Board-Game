using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

/// <summary>
/// This class is responsible for starting and ending rounds/waves of enemies
/// Lots of the logic is controlled by the feedbacks
/// </summary>
public class RoundManager : MonoBehaviour
{
    #region Variables
    [Header("Testing: ")]
    [SerializeField] private bool m_shouldSpawnWaves;
    [Header("Round to start with: (won't do anything if left empty)")]
    [SerializeField] private Round m_testStartingRound;
    [Header("Round Setup: ")]
    [SerializeField] private RoundGroup[] m_roundGroups;

    [Header("Enemy Spawners: ")]
    [SerializeField] private EnemySpawner[] m_enemySpawners;
    [Header("Enemy Spawner for bosses: ")]
    [SerializeField] private EnemySpawner m_bossSpawner;

    [Header("RoundUI Elements: ")]
    [SerializeField] private TextMeshProUGUI m_roundNameText;
    [SerializeField] private TextMeshProUGUI m_roundDescriptionText;
    [Header("GameUI Elements: ")]
    [SerializeField] private TextMeshProUGUI m_totalEnemiesKilledText;
    [SerializeField] private TextMeshProUGUI m_currentRoundText;
    [SerializeField] private TextMeshProUGUI m_targetsRemainingText;
    //[SerializeField] private TextMeshProUGUI m_currentScoreText;
    //[SerializeField] private TextMeshProUGUI m_deathUIbestScoreText;
    [SerializeField] private TextMeshProUGUI m_elapsedTimeText;

    [Header("Feedbacks: ")]
    public MMFeedbacks m_startGameFeedback;
    public MMFeedbacks m_roundStartFeedback;
    public MMFeedbacks m_waveStartFeedback;
    public MMFeedbacks m_waveEndFeedback;
    public MMFeedbacks m_killedEnemyFeedback;
    public MMFeedbacks m_winFeedback;

    [Header("For reference: ")]
    [SerializeField] private int m_currentScore;
    [SerializeField] private bool m_firstPlay;
    [SerializeField] private int m_roundGroupIndex;
    [SerializeField] private RoundGroup m_currentRoundGroup;
    [SerializeField] private Round m_currentRound;
    [SerializeField] private Wave m_currentWave;
    [SerializeField] private int m_currentWaveNumber;
    [SerializeField] private int m_roundsFromCurrentGroup;
    [SerializeField] private int m_totalEnemiesKilled;
    [SerializeField] private int m_targetsRemainingInWave;
    [SerializeField] private int m_totalRoundNumber;
    [SerializeField] private float m_totalTimeElapsed;
    [SerializeField] private bool m_isTimerCounting = true;

    #endregion

    public int GetScore() { return m_currentScore; }
    public int GetEnemiesKilled() { return m_totalEnemiesKilled; }
    public int GetTotalRoundNumber() { return m_totalRoundNumber; }
    public int GetTotalRoundsCompleted() { return m_totalRoundNumber - 1; }
    public float GetTotalTimeElapsed() { return m_totalTimeElapsed; }

    public void Start()
    {
        m_totalEnemiesKilledText.text = "0";
        m_targetsRemainingText.text = "0";
        m_currentRoundText.text = "Round 0";

        ShuffleRoundGroups();
        Reshuffle(m_enemySpawners);

        // Start at the correct round group index
        if (PlayerPrefs.HasKey("StartingRoundGroupIndex"))
        {
            m_roundGroupIndex = PlayerPrefs.GetInt("StartingRoundGroupIndex");
        }
        else
        {
            m_roundGroupIndex = 0;
        }

        // Setup
        m_isTimerCounting = true;
        m_totalTimeElapsed = 0.0f;
        m_totalEnemiesKilled = 0;
        m_currentRoundGroup = m_roundGroups[m_roundGroupIndex];
        m_roundsFromCurrentGroup = 1;
        m_totalRoundNumber = 1;
        m_currentWaveNumber = 1;
        m_firstPlay = true;

        if (m_shouldSpawnWaves && m_testStartingRound)
        {
            m_currentRound = m_testStartingRound;
        }
        else
        {
            m_currentRound = m_currentRoundGroup.GetRounds()[0];
        }
        m_currentWave = m_currentRound.GetWave(1);

        if (m_shouldSpawnWaves) { m_startGameFeedback.PlayFeedbacks(); }
    }

    private void Update()
    {
        // Update the timer
        if (m_isTimerCounting)
        {
            m_totalTimeElapsed += Time.deltaTime;
            m_elapsedTimeText.text = GetTimeElapsedStringFormat(m_totalTimeElapsed);
        }
    }

    /// <summary>
    /// Very important function
    /// Determines the order in which rounds and waves are played
    /// When called, it's find the right round or wave and start it
    /// (round start will eventually start the first wave of the current round)
    /// </summary>
    public void StartNextWaveOrRound()
    {
        // If we just started => start current round
        if (m_firstPlay == true)
        {
            m_firstPlay = false;
            RoundStart(m_currentRound);
        }
        // If the round is in progress => next wave
        else if (m_currentRound.GetNumberOfWaves() > m_currentWaveNumber)
        {
            m_currentWaveNumber++;
            m_currentWave = m_currentRound.GetWave(m_currentWaveNumber);
            WaveStart();
        }
        // If the round is over and there are more rounds in the current round group => next round, wave 1
        else if (m_roundsFromCurrentGroup < m_currentRoundGroup.GetNumberOfRounds())
        {
            m_roundsFromCurrentGroup++;
            m_totalRoundNumber++;
            m_currentWaveNumber = 1; //Go back to wave 1, of the new round
            m_currentRound = m_currentRoundGroup.GetRounds()[m_roundsFromCurrentGroup - 1];
            m_currentWave = m_currentRound.GetWave(1);
            RoundStart(m_currentRound);
        }

        // If the round is over and there are no more rounds of that type => next round group, first round, wave 1
        //TODO: do this
        else if (m_roundsFromCurrentGroup >= m_currentRoundGroup.GetNumberOfRounds() && m_roundGroupIndex < m_roundGroups.Length - 1)
        {
            m_roundGroupIndex++;
            m_roundsFromCurrentGroup = 1;
            m_totalRoundNumber++;
            m_currentWaveNumber = 1;
            m_currentRoundGroup = m_roundGroups[m_roundGroupIndex];
            m_currentRound = m_currentRoundGroup.GetRounds()[0];
            m_currentWave = m_currentRound.GetWave(1);
            RoundStart(m_currentRound);
        }
        else if (m_roundsFromCurrentGroup >= m_currentRoundGroup.GetNumberOfRounds())
        {
            Win();
        }
    }

    public void WaveEnd()
    {
        m_currentScore += m_currentWave.GetPointReward();
        m_waveEndFeedback.PlayFeedbacks();
    }

    /// <summary>
    /// Will start spawning the enemies from the given wave
    /// Sets the # of enemies you have to kill to end the wave
    /// </summary>
    public void WaveStart()
    {
        m_targetsRemainingInWave = m_currentWave.NumberOfEnemies();
        m_targetsRemainingText.text = m_targetsRemainingInWave.ToString();
        m_waveStartFeedback.PlayFeedbacks();
        StartCoroutine(SpawnEnemies(m_currentWave));
    }

    /// <summary>
    /// Will update + animate UI, wait, and eventually start the current wave
    /// </summary>
    public void RoundStart(Round round)
    {
        m_roundNameText.text = round.roundName;
        m_roundDescriptionText.text = round.roundDescription;
        m_currentRoundText.text = "ROUND " + m_totalRoundNumber.ToString();
        m_roundStartFeedback.PlayFeedbacks();
    }

    /// <summary>
    /// Will start spawning the enemies (from the current wave)
    /// Will check if the spawner is overriden for the current wave
    /// Will check if the spawn intervals are overriden for the current wave
    /// </summary>
    IEnumerator SpawnEnemies(Wave wave)
    {
        GameObject[] enemies = wave.GetEnemies();
        bool spawnerOverride = wave.IsSpawnerOverride();
        bool intervalOverride = wave.IsSpawnIntervalOverride();

        // Spawn enemies in the correct spawner mode and spawn interval time
        if (!spawnerOverride && !intervalOverride)
        {
            float spawnInterval = wave.GetSpawnInterval();
            foreach (GameObject enemy in enemies)
            {
                yield return new WaitForSeconds(spawnInterval);
                EnemySpawner spawner = GetRandomValidSpawner();
                spawner.Spawn(enemy);
            }
        }
        else if (!spawnerOverride && intervalOverride)
        {
            float[] newSpawnIntervals = wave.GetOverridenSpawnIntervals();
            int i = 0;
            foreach (GameObject enemy in enemies)
            {
                yield return new WaitForSeconds(newSpawnIntervals[i]);
                EnemySpawner spawner = GetRandomValidSpawner();
                spawner.Spawn(enemy);
                i++;
            }
        }
        else if (spawnerOverride && !intervalOverride)
        {
            float spawnInterval = wave.GetSpawnInterval();
            foreach (GameObject enemy in enemies)
            {
                yield return new WaitForSeconds(spawnInterval);
                m_bossSpawner.Spawn(enemy);
            }
        }
        else if (spawnerOverride && intervalOverride)
        {
            float[] newSpawnIntervals = wave.GetOverridenSpawnIntervals();
            int i = 0;
            foreach (GameObject enemy in enemies)
            {
                yield return new WaitForSeconds(newSpawnIntervals[i]);
                m_bossSpawner.Spawn(enemy);
                i++;
            }
        }

        yield return null;
    }

    /// <summary>
    /// Will return an enemy spawner that didn't just spawn an enemy (in the last 0.5 seconds)
    /// </summary>
    public EnemySpawner GetRandomValidSpawner()
    {
        for (int i = 0; i < 40; i++)
        {
            EnemySpawner spawner = m_enemySpawners[Random.Range(0, m_enemySpawners.Length - 1)];
            if (spawner.IsAvailable())
            {
                return spawner;
            }
        }
        // If none are valid, return any spawner
        return m_enemySpawners[Random.Range(0, m_enemySpawners.Length - 1)];
    }

    // Increase the # of killed enemies, bump the text,
    // If we killed enough enemies => StartNextWaveOrRound
    public void KilledEnemy()
    {
        m_totalEnemiesKilled++;
        m_targetsRemainingInWave--;
        m_totalEnemiesKilledText.text = m_totalEnemiesKilled.ToString();
        m_targetsRemainingText.text = m_targetsRemainingInWave.ToString();
        m_killedEnemyFeedback.PlayFeedbacks(); // bump text, play SFX

        if (m_targetsRemainingInWave <= 0)
        {
            WaveEnd(); // Update Score, play SFX, wait, StartNextRoundOrWave
        }
    }

    public void AddToTargetCount(int amountToAdd)
    {
        m_targetsRemainingInWave += amountToAdd;
        m_targetsRemainingText.text = m_targetsRemainingInWave.ToString();
    }

    public void AddToScore(int amountToAdd)
    {
        m_currentScore += amountToAdd;
    }

    public void IsTimerCounting(bool value) { m_isTimerCounting = value; }

    public void UpdateBestScore()
    {
        if (!PlayerPrefs.HasKey("BestScore") || m_currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", m_currentScore);
            PlayerPrefs.Save();
        }
    }

    public string GetTimeElapsedWithMilisStringFormat()
    {
        return GetTimeElapsedStringFormat(m_totalTimeElapsed, true);
    }

    private string GetTimeElapsedStringFormat(float timeInSec, bool showMiliseconds = false)
    {
        int hours = ((int)timeInSec) / 3600;
        int mins = (((int)timeInSec) % 3600) / 60;
        int sec = (((int)timeInSec) % 60);

        if (!showMiliseconds)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, sec);
        }
        else
        {
            int milis = (int)(timeInSec * 1000) % 1000;
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", hours, mins, sec, milis);
        }
    }

    private void Win()
    {
        Debug.Log("You win!");
        UpdateBestScore();
        m_winFeedback.PlayFeedbacks();
    }
    
    /// <summary>
    /// Will shuffle the round groups that should be reshuffled
    /// </summary>
    private void ShuffleRoundGroups()
    {
        for (int i = 0; i < m_roundGroups.Length; i++)
        {
            if (m_roundGroups[i].ShouldReshuffle())
            {
                m_roundGroups[i].SetRounds(Reshuffle(m_roundGroups[i].GetRounds()));
            }
        }
    }

    private Round[] Reshuffle(Round[] rounds)
    {
        Round[] final = rounds;
        // Knuth shuffle algorithm
        for (int t = 0; t < final.Length; t++)
        {
            Round tmp = final[t]; 
            int r = Random.Range(t, final.Length); //Get new value from the rest of the array
            final[t] = final[r]; // Set the current value to the new random one
            final[r] = tmp;
        }

        return final;
    }

    private void Reshuffle(EnemySpawner[] spawner)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < spawner.Length; t++)
        {
            EnemySpawner tmp = spawner[t];
            int r = Random.Range(t, spawner.Length);
            spawner[t] = spawner[r];
            spawner[r] = tmp;
        }
    }
}