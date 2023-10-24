using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGSetPlayerPrefs : MonoBehaviour
{
    [Header("The NAME and TYPE of the key: ")]
    [SerializeField] private string m_keyName;
    [SerializeField] private KeyType m_keyType;

    [Header("Key Value, Only used in SetKeyFromPredeterminedValue(), Only use the value of the key type")]
    [SerializeField] private string m_stringValue;
    [SerializeField] private int m_intValue;
    [SerializeField] private float m_floatValue;

    [Header("Array of Key Values, Only used in SetKeyFromArrayIndex()) Only use the array of the key type)")]
    [SerializeField] private string[] m_stringArray;
    [SerializeField] private int[] m_intArray;
    [SerializeField] private float[] m_floatArray;


    public enum KeyType
    {
        intType,
        floatType,
        stringType
    }

    /// <summary>
    /// Sets "KeyName" the value the index from the array of "KeyType"
    /// Make sure the array of the 
    /// </summary>
    /// <param name="index">The index of the value to get from the array</param>
    public void SetKeyFromArrayIndex(int index)
    {
        if (m_keyName != null && m_keyName != "")
        {
            switch (m_keyType)
            {
                case KeyType.stringType:
                    PlayerPrefs.SetString(m_keyName, m_stringArray[index]);
                    break;
                case KeyType.intType:
                    PlayerPrefs.SetInt(m_keyName, m_intArray[index]);
                    break;
                case KeyType.floatType:
                    PlayerPrefs.SetFloat(m_keyName, m_floatArray[index]);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Sets "KeyName" the value of KeyValue of "KeyType"
    /// </summary>
    public void SetKeyFromPredeterminedValue()
    {
        if (m_keyName != null && m_keyName != "")
        {
            switch (m_keyType)
            {
                case KeyType.stringType:
                    PlayerPrefs.SetString(m_keyName, m_stringValue);
                    break;
                case KeyType.intType:
                    PlayerPrefs.SetInt(m_keyName, m_intValue);
                    break;
                case KeyType.floatType:
                    PlayerPrefs.SetFloat(m_keyName, m_floatValue);
                    break;
                default:
                    break;
            }
        }
    }

    public void SetString(string value)
    {
        if (m_keyName != null) { PlayerPrefs.SetString(m_keyName, value); }
    }

    public void SetFloat(float value)
    {
        if (m_keyName != null) { PlayerPrefs.SetFloat(m_keyName, value); }
    }

    public void SetInt(int value)
    {
        if (m_keyName != null) { PlayerPrefs.SetInt(m_keyName, value); }
    }

    public void SetIntFromFloat(float value)
    {
        if (m_keyName != null) { PlayerPrefs.SetInt(m_keyName, (int)value); }
    }

    public void SetIntFromBool(bool value)
    {
        if (m_keyName != null)
        {
            if (value)
            {
                PlayerPrefs.SetInt(m_keyName, 1);
            }
            else
            {
                PlayerPrefs.SetInt(m_keyName, 0);
            }
        }
    }

    public void SaveAllPlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}