using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomizeText : MonoBehaviour
{
    #region Variables

    [Header("Settings")]
    [SerializeField] private bool m_debugMessages;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_text;
    [Header("Random Text Options:")]
    [SerializeField] private string[] m_textOptions;

    #endregion

    void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        if (m_debugMessages) { Debug.Log("RandomizeText"); }
        string text = m_textOptions[Random.Range(0, m_textOptions.Length - 1)];
        m_text.text = text;
    }
}