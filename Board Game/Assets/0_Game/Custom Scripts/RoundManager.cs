using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class RoundManager : MonoBehaviour
{
    [Header("Round Setup: ")]
    [SerializeField] private Round[] m_tutorialRounds;
    [SerializeField] private Round[] m_easyRounds;
    [SerializeField] private Round[] m_mediumRounds;
    [SerializeField] private Round[] m_hardRounds;
    [SerializeField] private Round[] m_finalRounds;

    [Header("Feedbacks: ")]
    public MMFeedbacks m_roundStartFeedback;
    public MMFeedbacks m_roundEndFeedback;
}