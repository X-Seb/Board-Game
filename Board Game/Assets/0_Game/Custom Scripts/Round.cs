using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu()]
public class Round : ScriptableObject
{
    [Header("Round information: ")]
    [SerializeField] public string roundName;
    [SerializeField] public string roundDescription;
    [SerializeField] public int roundDifficulty;

    [Header("Round Audio")]
    [SerializeField] public bool overrideSFX;
    [SerializeField] public AudioClip roundStartSFX;
    [SerializeField] public AudioClip roundEndSFX;

    [Header("Waves of enemies: ")]
    [SerializeField] private Wave[] m_waves;

    // Returns wave from the round (starts at 1)
    public Wave GetWave(int waveNum)
    {
        return m_waves[waveNum - 1];
    }

    public int GetNumberOfWaves()
    {
        return m_waves.Length;
    }
}