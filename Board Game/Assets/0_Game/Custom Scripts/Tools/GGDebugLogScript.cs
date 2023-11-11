using UnityEngine;

public class GGDebugLogScript : MonoBehaviour
{
    [SerializeField] private string m_simpleMessage;

    public void SimpleDebugLogMessage()
    {
        Debug.Log(m_simpleMessage);
    }
}