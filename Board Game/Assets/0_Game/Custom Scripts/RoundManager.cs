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
    [Header("Round Setup: ")]
    [SerializeField] private Round[] m_tutorialRounds;
    [SerializeField] private Round[] m_easyRounds;
    [SerializeField] private Round[] m_mediumRounds;
    [SerializeField] private Round[] m_hardRounds;
    [SerializeField] private Round[] m_insaneRounds;

    [Header("Enemy Spawners")]
    [SerializeField] private EnemySpawner[] m_enemySpawners;

    [Header("RoundUI Elements: ")]
    [SerializeField] private TextMeshProUGUI m_roundNameText;
    [SerializeField] private TextMeshProUGUI m_roundDescriptionText;
    [Header("GameUI Elements: ")]
    [SerializeField] private TextMeshProUGUI m_totalEnemiesKilledText;
    [SerializeField] private TextMeshProUGUI m_currentRoundText;
    [SerializeField] private TextMeshProUGUI m_targetsRemainingText;
    [SerializeField] private TextMeshProUGUI m_elapsedTimeText;

    [Header("Feedbacks: ")]
    public MMFeedbacks m_startGameFeedback;
    public MMFeedbacks m_roundStartFeedback;
    public MMFeedbacks m_waveEndFeedback;
    public MMFeedbacks m_killedEnemyFeedback;

    [Header("For reference: ")]
    [SerializeField] private RoundType m_currentRoundType;
    [SerializeField] private Round m_currentRound;
    [SerializeField] private Wave m_currentWave;
    [SerializeField] private int m_currentWaveNumber;
    [SerializeField] private int m_roundsFromCurrentCategory;
    [SerializeField] private int m_totalEnemiesKilled;
    [SerializeField] private int m_targetsRemainingInWave;
    [SerializeField] private int m_totalRoundNumber;
    [SerializeField] private float m_totalTimeElapsed;

    #endregion

    #region Enum

    enum RoundType
    {
        tutorial,
        easy,
        medium,
        hard,
        insane
    }

    #endregion

    public void Start()
    {
        m_totalEnemiesKilledText.text = "0";
        m_targetsRemainingText.text = "0";
        m_currentRoundText.text = "Round 0";
        Reshuffle(m_easyRounds);
        //Reshuffle(m_mediumRounds);
        //Reshuffle(m_hardRounds);
        //Reshuffle(m_insaneRounds);
        Reshuffle(m_enemySpawners);

        if (m_shouldSpawnWaves) { m_startGameFeedback.PlayFeedbacks(); }
    }

    /// <summary>
    /// Very important function
    /// Determines the order in which rounds and waves are played
    /// When called, it's find the right round or wave and start it
    /// (round start will eventually start the first wave of the current round)
    /// </summary>
    public void StartNextWaveOrRound()
    {
        // If we just started => start tutorial 1, wave 1
        if (m_currentRound == null)
        {
            m_currentRoundType = RoundType.tutorial;
            m_roundsFromCurrentCategory = 1;
            m_totalRoundNumber = 1;
            m_currentWaveNumber = 1;
            m_currentRound = m_tutorialRounds[m_roundsFromCurrentCategory - 1];
            m_currentWave = m_currentRound.GetWave(1);
            RoundStart(m_currentRound);
        }
        // If the round is in progress => next wave
        else if (m_currentRound.GetNumberOfWaves() > m_currentWaveNumber)
        {
            m_currentWaveNumber++;
            m_currentWave = m_currentRound.GetWave(m_currentWaveNumber);
            WaveStart();
        }
        // If the round is over and there are more rounds of that type => next round, wave 1
        else if (m_roundsFromCurrentCategory < GetRoundsFromType(m_currentRoundType).Length)
        {
            m_roundsFromCurrentCategory++;
            m_totalRoundNumber++;
            m_currentWaveNumber = 1;
            m_currentRound = GetRoundsFromType(m_currentRoundType)[m_roundsFromCurrentCategory - 1];
            m_currentWave = m_currentRound.GetWave(1);
            RoundStart(m_currentRound);
        }

        // If the round is over and there are no more rounds of that type => next category, random round, wave 1
        //TODO: do this
        // If there are no more rounds => you win!
        //TODO: do this too!
    }

    // Will start spawning the enemies from the given wave
    // Sets the # of enemies you have to kill to end the wave
    public void WaveStart()
    {
        m_targetsRemainingInWave = m_currentWave.NumberOfEnemies();
        GameObject[] enemies = m_currentWave.GetEnemies();
        StartCoroutine(SpawnEnemies(enemies));
    }

    // Will update + animate UI, wait, and eventually start the current wave
    public void RoundStart(Round round)
    {
        m_roundNameText.text = round.roundName;
        m_roundDescriptionText.text = round.roundDescription;
        m_currentRoundText.text = "ROUND " + m_totalRoundNumber.ToString();
        m_roundStartFeedback.PlayFeedbacks();
    }

    // Will start spawning the enemies (from the current wave)
    IEnumerator SpawnEnemies(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies) {
            EnemySpawner spawner = GetRandomValidSpawner();
            spawner.Spawn(enemy);
            yield return new WaitForSeconds(m_currentWave.GetSpawnInterval());
        }
        yield return null;
    }

    // Will return an enemy spawner that didn't just spawn an enemy (in the last 0.5 seconds)
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
            m_waveEndFeedback.PlayFeedbacks(); // Play SFX, wait, StartNextRoundOrWave
        }
    }

    // Returns the array of rounds of the given round type
    private Round[] GetRoundsFromType(RoundType type)
    {
        switch (type)
        {
            case RoundType.tutorial:
                return m_tutorialRounds;
            case RoundType.easy:
                return m_easyRounds;
            case RoundType.medium:
                return m_mediumRounds;
            case RoundType.hard:
                return m_hardRounds;
            case RoundType.insane:
                return m_insaneRounds;
            default:
                return null;
        }
    }

    private void Reshuffle(Round[] rounds)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < rounds.Length; t++)
        {
            Round tmp = rounds[t];
            int r = Random.Range(t, rounds.Length);
            rounds[t] = rounds[r];
            rounds[r] = tmp;
        }
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