using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGSetPlayerPrefs : MonoBehaviour
{
    [Header("The name and type of the key: ")]
    [SerializeField] private string m_keyName;
    [SerializeField] private KeyType m_keyType;

    [Header("Key value (only use the one for the key type): ")]
    [SerializeField] private string m_stringValue;
    [SerializeField] private int m_intValue;
    [SerializeField] private float m_floatValue;

    public enum KeyType
    {
        intType,
        floatType,
        stringType
    }

    public void SetBoolAsInt(bool value)
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

    public void SetKey()
    {
        if (m_keyName != null)
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
}