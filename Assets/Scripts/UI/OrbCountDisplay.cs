using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCountDisplay : MonoBehaviour
{
    //private Player m_playerRef;
    private Text m_textDisplay;
    private int m_iIndex;
    public List<OrbGate> GateList = new List<OrbGate>();

    public static OrbCountDisplay m_orbCountDisplay;

    private void Awake()
    {
        if (m_orbCountDisplay == null)
        {
            m_orbCountDisplay = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_iIndex = 0;
        m_textDisplay = GetComponent<Text>();
    }

    void Update()
    {
        if (GateList[m_iIndex].m_bUnlocked && m_iIndex != GateList.Count - 1)
        {
              if (m_iIndex < GateList.Count - 1)
              {
                m_iIndex++;
              }
        }
        else
        {
            if (Player.m_player != null && m_textDisplay != null)
            {
                m_textDisplay.text = Player.m_player.m_orbsCollected.ToString() + "/" + GateList[m_iIndex].NumberOfOrbsToOpen;
            }
        }
    }

    public void ResetIndex()
    {
        m_iIndex = 0;
    }
}
