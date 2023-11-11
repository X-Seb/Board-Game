using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GGSetQualityTool : MonoBehaviour
{
    [Header("When to automatically set the quality")]
    [SerializeField] private TriggerTime m_whenToSetQuality;
    [Header("The dropdown to change the value of: ")]
    [SerializeField] private TMP_Dropdown m_dropdown;

    public enum TriggerTime
    {
        none,
        awake
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Quality") && m_whenToSetQuality == TriggerTime.awake)
        {
            SetQualityFromPlayerPrefs();
        }
        else if (m_whenToSetQuality == TriggerTime.awake)
        {
            SetDropdownValueFromCurrentQualityLevel();
        }
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    private void SetQualityFromPlayerPrefs()
    {
        int quality = PlayerPrefs.GetInt("Quality");
        QualitySettings.SetQualityLevel(quality);
        m_dropdown.value = quality;
    }

    private void SetDropdownValueFromCurrentQualityLevel()
    {
        int quality = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("Quality", quality);
        m_dropdown.value = quality;
    }
}