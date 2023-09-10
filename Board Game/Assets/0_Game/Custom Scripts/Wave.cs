using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Wave : ScriptableObject
{
    [SerializeField] private GameObject[] m_enemies;
    [SerializeField] private float m_spawnInterval;
    [SerializeField] private bool m_overrideSpawner;
    [SerializeField] private EnemySpawner m_spawnerOverride;

    public EnemySpawner GetOverrideSpawner() { return m_spawnerOverride; }
    public bool IsSpawnerOverride() { return m_overrideSpawner; }
    public float GetSpawnInterval() { return m_spawnInterval; }
    public GameObject[] GetEnemies() { return m_enemies; }

    public int NumberOfEnemies()
    {
        return m_enemies.Length;
    }
}