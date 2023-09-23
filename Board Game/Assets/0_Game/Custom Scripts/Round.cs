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

    /// <summary>
    /// Returns the wave from the round (STARTS at 1, not 0)
    /// </summary>
    public Wave GetWave(int waveNum)
    {
        return m_waves[waveNum - 1];
    }

    public int GetNumberOfWaves() { return m_waves.Length; }
}