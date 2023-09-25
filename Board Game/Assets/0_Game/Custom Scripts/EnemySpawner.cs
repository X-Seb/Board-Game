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
        Vector3 position = m_spawnPosition.position;
        m_spawnFeedback.PlayFeedbacks();
        StartCoroutine(UpdateJustSpawned());

        if (m_spawnFromPool)
        {
            GameObject pooledObject = m_objectPooler.GetPooledGameObjectOfType("Poolable " + objectToSpawn.name);

            if (pooledObject == null)
            {
                Instantiate(objectToSpawn, position, new Quaternion(0, 0, 0, 0));
                return;
            }
            pooledObject.SetActive(true);
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            Instantiate(objectToSpawn, position, new Quaternion(0, 0, 0, 0));
        }
        
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