using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GGGetPlayerPrefs : MonoBehaviour
{
    [Header("The name and type of the key: ")]
    [SerializeField] private string m_keyName;
    [SerializeField] private KeyType m_keyType;
    [Header("When to trigger events? ")]
    [SerializeField] private TriggerTime m_triggerTime;
    [Header("General Events: ")]
    [SerializeField] private UnityEvent m_hasKeyEvent;
    [SerializeField] private UnityEvent m_doesNotHaveKeyEvent;

    [Header("Should run comparrison? ")]
    [SerializeField] private bool m_shouldCompare;
    [Header("What should return true (only for int or float comparisons)")]
    [SerializeField] private ComparisonType m_comparisonType;
    [Header("For String Type Only")]
    [SerializeField] private string m_compareToString;
    [Header("For Int Type Only")]
    [SerializeField] private int m_compareToInt;
    [Header("For Float Type Only")]
    [SerializeField] private float m_compareToFloat;

    [Header("Comparison Events: ")]
    [SerializeField] private UnityEvent m_comparisonTrueEvent;
    [SerializeField] private UnityEvent m_comparisonFalseEvent;

    #region enums
    public enum TriggerTime
    {
        none,
        awake,
        start,
    }

    public enum KeyType
    {
        intType,
        floatType,
        stringType
    }

    public enum ComparisonType
    {
        equal,
        greater,
        greaterOrEqual,
        smaller,
        smallerOrEqual
    }
    #endregion

    private void Awake()
    {
        if (m_triggerTime == TriggerTime.awake)
        {
            KeyExistsEvents(m_keyName);

            if (m_shouldCompare && PlayerPrefs.HasKey(m_keyName))
            {
                KeyCompareEvents(m_keyName);
            }
        }
    }

    private void Start()
    {
        if (m_triggerTime == TriggerTime.start)
        {
            KeyExistsEvents(m_keyName);

            if (m_shouldCompare && PlayerPrefs.HasKey(m_keyName))
            {
                KeyCompareEvents(m_keyName);
            }
        }
    }

    public string GetStringFromKey()
    {
        if (PlayerPrefs.HasKey(m_keyName))
        {
            return PlayerPrefs.GetString(m_keyName);
        }
        else { return ""; }
    }

    public float GetFloatFromKey()
    {
        if (PlayerPrefs.HasKey(m_keyName))
        {
            return PlayerPrefs.GetFloat(m_keyName);
        }
        else { return 0.0f; }
    }

    public int GetIntFromKey()
    {
        if (PlayerPrefs.HasKey(m_keyName))
        {
            return PlayerPrefs.GetInt(m_keyName);
        }
        else { return 0; }
    }

    /// <summary>
    /// Will check if the key exists to invoke the corresponding unity event.
    /// </summary>
    public void KeyExistsEvents(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            m_hasKeyEvent.Invoke();
        }
        else
        {
            m_doesNotHaveKeyEvent.Invoke();
        }
    }

    /// <summary>
    /// Will compare the key value with the other set value using the set comparison method
    /// to then invoke the corresponding unity event.
    /// </summary>
    public void KeyCompareEvents(string key)
    {
        bool value = false;

        if (m_keyType == KeyType.stringType)
        {
            if (PlayerPrefs.GetString(m_keyName) == m_compareToString) value = true;
        }
        else if (m_keyType == KeyType.intType)
        {
            if (m_comparisonType == ComparisonType.equal)
            {
                if (PlayerPrefs.GetInt(m_keyName) == m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.greater)
            {
                if (PlayerPrefs.GetInt(m_keyName) > m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.greaterOrEqual)
            {
                if (PlayerPrefs.GetInt(m_keyName) >= m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.smaller)
            {
                if (PlayerPrefs.GetInt(m_keyName) < m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.smallerOrEqual)
            {
                if (PlayerPrefs.GetInt(m_keyName) <= m_compareToInt) value = true;
            }
        }
        else if (m_keyType == KeyType.floatType)
        {
            if (m_comparisonType == ComparisonType.equal)
            {
                if (PlayerPrefs.GetFloat(m_keyName) == m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.greater)
            {
                if (PlayerPrefs.GetFloat(m_keyName) > m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.greaterOrEqual)
            {
                if (PlayerPrefs.GetFloat(m_keyName) >= m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.smaller)
            {
                if (PlayerPrefs.GetFloat(m_keyName) < m_compareToInt) value = true;
            }
            else if (m_comparisonType == ComparisonType.smallerOrEqual)
            {
                if (PlayerPrefs.GetFloat(m_keyName) <= m_compareToInt) value = true;
            }
        }

        if (value == true)
        {
            m_comparisonTrueEvent.Invoke();
        }
        else
        {
            m_comparisonFalseEvent.Invoke();
        }
    }

    public void SetSliderFloatValue(Slider slider)
    {
        if (m_keyName != null) { slider.value = PlayerPrefs.GetFloat(m_keyName); }
    }

    public void SetSliderIntValue(Slider slider)
    {
        if (m_keyName != null) { slider.value = PlayerPrefs.GetInt(m_keyName); }
    }
}