using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GGInvokeEvent : MonoBehaviour
{
    [Header("When to trigger events? ")]
    [SerializeField] private TriggerTime m_triggerTime;
    [Header("Unity Event: ")]
    [SerializeField] private UnityEvent m_event;

    public enum TriggerTime
    {
        Awake,
        Start,
    }

    void Awake()
    {
        if (m_triggerTime == TriggerTime.Awake)
        {
            m_event?.Invoke();
        }
    }

    void Start()
    {
        if (m_triggerTime == TriggerTime.Start)
        {
            m_event?.Invoke();
        }
    }
}