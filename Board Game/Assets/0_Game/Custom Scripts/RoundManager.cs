using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;
using MoreMountains.Tools;

//public struct KilledEnemy

/// <summary>
/// Lots of the logic is controlled by the feedbacks
/// </summary>
public class RoundManager : MonoBehaviour, MMEventListener<MMGameEvent>
{
    [Header("Round Setup: ")]
    [SerializeField] private Round[] m_tutorialRounds;
    [SerializeField] private Round[] m_easyRounds;
    [SerializeField] private Round[] m_mediumRounds;
    [SerializeField] private Round[] m_hardRounds;
    [SerializeField] private Round[] m_insaneRounds;

    [Header("Enemy Spawners")]
    [SerializeField] private EnemySpawner[] m_enemySpawners;

    [Header("Text Elements: ")]
    [SerializeField] private TextMeshProUGUI m_roundNameText;
    [SerializeField] private TextMeshProUGUI m_roundDescriptionText;
    //[SerializeField] private TextMeshProUGUI m_roundNameText;

    [Header("Important Stuff: ")]
    [SerializeField] private int m_totalEnemiesKilled;
    [SerializeField] private int m_targetsRemaining;
    [SerializeField] private int m_totalRoundNumber;
    [SerializeField] private float m_totalTimeElapsed;

    [Header("Feedbacks: ")]
    public MMFeedbacks m_startGameFeedback;
    public MMFeedbacks m_roundStartFeedback;
    public MMFeedbacks m_roundEndFeedback;
    public MMFeedbacks m_killedEnemyFeedback;

    [Header("For reference: ")]
    [SerializeField] private RoundType m_currentRoundType;
    [SerializeField] private Round m_currentRound;
    [SerializeField] private Wave m_currentWave;
    [SerializeField] private int m_currentWaveNumber;
    [SerializeField] private int m_roundsFromCurrentCategory;

    enum RoundType
    {
        tutorial,
        easy,
        medium,
        hard,
        insane
    }

    public void Start()
    {
        m_startGameFeedback.PlayFeedbacks();
    }

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

        // If there are no more rounds => you win

    }

    // Will start spawning the enemies from the given wave

    public void WaveStart()
    {
        GameObject[] enemies = m_currentWave.GetEnemies();
        StartCoroutine(SpawnEnemies(enemies));
    }

    // Will update + animate UI, wait, start the current wave
    public void RoundStart(Round round)
    {
        m_roundNameText.text = round.roundName;
        m_roundDescriptionText.text = round.roundDescription;
        m_roundStartFeedback.PlayFeedbacks();
    }

    // Called when all enemies from the wave have died
    public void RoundEnd()
    {
        
    }

    IEnumerator SpawnEnemies(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies) {
            EnemySpawner spawner = GetRandomValidSpawner();
            spawner.Spawn(enemy);
            yield return new WaitForSeconds(m_currentWave.GetSpawnInterval());
        }

        yield return null;
    }

    public EnemySpawner GetRandomValidSpawner()
    {
        return m_enemySpawners[Random.Range(0, m_enemySpawners.Length - 1)];
    }

    // Increase the # of killed enemies, bump the text
    public void EnemyDied()
    {
        m_totalEnemiesKilled++;
        m_killedEnemyFeedback.PlayFeedbacks();
    }

    public void SetKills(int numberOfKills)
    {
        m_totalEnemiesKilled = numberOfKills;
    }

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

    public virtual void OnMMEvent(MMGameEvent gameEvent)
    {
        Debug.Log(gameEvent);
    }

    void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }

    void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
}