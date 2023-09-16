using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class EnemySpawner : MonoBehaviour
{
    [Header("Setup: ")]
    [SerializeField] private Transform m_spawnPosition;
    [SerializeField] private MMFeedbacks m_spawnFeedback;

    [Header("Settings: ")]
    [SerializeField] private bool m_spawnFromPool;
    [SerializeField] private MMMultipleObjectPooler m_objectPooler;

    [Header("Just Spawned delay: ")]
    [SerializeField] private bool m_justSpawned = false;
    [SerializeField] private float m_waitSeconds;

    public void Spawn(GameObject objectToSpawn)
    {
        m_spawnFeedback.PlayFeedbacks();
        Transform position = m_spawnPosition;
        Instantiate(objectToSpawn, position);
        StartCoroutine(UpdateJustSpawned());
    }

    public void Spawn()
    {
        Debug.Log("Spawned object! ");
        GameObject objectToSpawn = m_objectPooler.GetPooledGameObject();
        objectToSpawn.transform.position = m_spawnPosition.transform.position;
        objectToSpawn.SetActive(true);
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