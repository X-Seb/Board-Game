using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform m_spawnPosition;

    [Header("Just Spawned delay: ")]
    [SerializeField] private bool m_justSpawned;
    [SerializeField] private float m_waitSeconds;

    public void Spawn(GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, m_spawnPosition);
        StartCoroutine(UpdateJustSpawned());
    }

    // Getter method
    public bool JustSpawned() { return m_justSpawned;  }

    private IEnumerator UpdateJustSpawned()
    {
        m_justSpawned = true;
        yield return new WaitForSeconds(m_waitSeconds);
        m_justSpawned = false;
    }
}