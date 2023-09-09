using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Round : ScriptableObject
{
    [SerializeField] private string m_roundName;
    [SerializeField] private string m_description;
    [SerializeField] private int m_roundDifficulty;
    [SerializeField] private string m_numberOfWaves;
    [SerializeField] private GameObject[] m_wave1;
    [SerializeField] private GameObject[] m_wave2;
    [SerializeField] private GameObject[] m_wave3;
    [SerializeField] private GameObject[] m_wave4;
    [SerializeField] private GameObject[] m_wave5;
}