using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EnemySpawner : MonoBehaviour
{
    [Header("Setup: ")]
    [SerializeField] private Transform m_spawnPosition;
    [SerializeField] private MMFeedbacks m_spawnFeedback;

    [Header("Just Spawned delay: ")]
    [SerializeField] private bool m_justSpawned = false;
    [SerializeField] private float m_waitSeconds;

    public void Spawn(GameObject objectToSpawn)
    {
        m_spawnFeedback.PlayFeedbacks();
        Instantiate(objectToSpawn, m_spawnPosition);
        StartCoroutine(UpdateJustSpawned());
    }

    // Getter method
    public bool IsAvailable() { return m_justSpawned;  }

    private IEnumerator UpdateJustSpawned()
    {
        m_justSpawned = true;
        yield return new WaitForSeconds(m_waitSeconds);
        m_justSpawned = false;
    }
}