using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RoundGroup : ScriptableObject
{
    [Header("Round Group information: ")]
    [SerializeField] private string m_groupName;
    [SerializeField] private bool m_shouldReshuffle;
    [Header("Rounds: ")]
    [SerializeField] private Round[] m_rounds;

    public Round[] GetRounds() { return m_rounds; }
    public void SetRounds(Round[] newRounds) { m_rounds = newRounds; }
    public int GetNumberOfRounds() { return m_rounds.Length;  }
    public bool ShouldReshuffle() { return m_shouldReshuffle; }
}