using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.Events;

public class UpgradeHolder : MonoBehaviour
{
    [SerializeField] private InventoryItem m_upgrade;
    [SerializeField] private string m_rarity;
    [SerializeField] private UnityEvent m_upgradeGainedEvent;

    public void InvokeUpgradeGainedEvent()
    {
        m_upgradeGainedEvent.Invoke();
    }
}