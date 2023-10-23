using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Array of Upgrades: ")]
    [SerializeField] private GameObject m_commonUpgrades;
    [SerializeField] private GameObject m_rareUpgrades;
    [SerializeField] private GameObject m_epicUpgrades;
    [SerializeField] private GameObject m_legendaryUpgrades;
    [SerializeField] private Inventory m_availableUpgradeInventory;
    [SerializeField] private Inventory m_aquiredUpgradeInventory;


}