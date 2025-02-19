using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    public string m_strMessage;

    public Text m_message;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Transform>().name == "PlayerAlpha")
        {
            m_message.text = m_strMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Transform>().name == "PlayerAlpha")
        {
            m_message.text = "";
        }
    }
}
