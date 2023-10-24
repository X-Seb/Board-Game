using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GGReplaceTextTool : MonoBehaviour
{
    [Header("UI: ")]
    [SerializeField] private TextMeshProUGUI m_text;
    [Header("Use '{}' to replace {} with the variable you're getting.")]
    [SerializeField] private string m_replaceWith;
    [Header("One way to get the variable information: ")]
    [SerializeField] private GGComponentInfoFetcherTool m_componentInfoFetcher;
    [SerializeField] private GameObject m_objectToGetFrom;
    [SerializeField] private string m_componentName;
    [SerializeField] private string m_getterMethodName;
    [SerializeField] private InfoType m_infoType;

    public enum InfoType
    {
        stringType,
        floatType,
        intType,
        boolType
    }

    public void SetTextWithFloat(float valueToInsert)
    {
        m_text.text = m_replaceWith.Replace("{}", valueToInsert.ToString());
    }

    public void SetTextByUsingReflection()
    {
        switch (m_infoType)
        {
            case InfoType.stringType:
                string infoString = m_componentInfoFetcher.GetStringInfoFromComponent(m_objectToGetFrom, m_componentName, m_getterMethodName);
                m_text.text = m_replaceWith.Replace("{}", infoString);
                break;
            case InfoType.floatType:
                float infoFloat = m_componentInfoFetcher.GetFloatInfoFromComponent(m_objectToGetFrom, m_componentName, m_getterMethodName);
                m_text.text = m_replaceWith.Replace("{}", infoFloat.ToString());
                break;
            case InfoType.intType:
                int infoInt = m_componentInfoFetcher.GetIntInfoFromComponent(m_objectToGetFrom, m_componentName, m_getterMethodName);
                m_text.text = m_replaceWith.Replace("{}", infoInt.ToString());
                break;
            case InfoType.boolType:
                bool infoBool = m_componentInfoFetcher.GetBoolInfoFromComponent(m_objectToGetFrom, m_componentName, m_getterMethodName);
                m_text.text = m_replaceWith.Replace("{}", infoBool.ToString());
                break;
            default:
                break;
        }
    }
}