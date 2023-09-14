using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Wave : ScriptableObject
{
    [Header("Essential information: ")]
    [SerializeField] private GameObject[] m_enemies;
    [SerializeField] private float m_spawnInterval;
    [Header("Should override the spawner for all enemies?")]
    [SerializeField] private bool m_overrideSpawner;
    [SerializeField] private EnemySpawner m_spawnerOverride;
    [Header("Should override the spawn interval for each enemy? (make sure the array size >= enemy array size)")]
    [SerializeField] private bool m_overrideSpawnIntervals;
    [SerializeField] private float[] m_newSpawnIntervals;

    public int NumberOfEnemies()
    {
        return m_enemies.Length;
    }
    public float GetSpawnInterval() { return m_spawnInterval; }
    public GameObject[] GetEnemies() { return m_enemies; }

    public bool IsSpawnerOverride() { return m_overrideSpawner; }
    public EnemySpawner GetOverrideSpawner() { return m_spawnerOverride; }

    public bool IsSpawnIntervalOverride() { return m_overrideSpawnIntervals; }
    public float[] GetOverridenSpawnIntervals() { return m_newSpawnIntervals; }

    public float GetSpawnIntervalForEnemy(int enemy)
    {
        return m_newSpawnIntervals[enemy];
    }
}