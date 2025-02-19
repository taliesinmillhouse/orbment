using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeBackButton : MonoBehaviour
{
    private float m_fGrowMultiplier = 1.5f;
    private float m_fShrinkMultiplier = 1.0f;
    private float m_fGrowShrinkSpeed = 0.05f;

    private bool m_bIsHightlighted = false;
    public bool IsHightlighted { get { return m_bIsHightlighted; } set { m_bIsHightlighted = value; } }

    public GameObject m_perkTreePanel;

    public List<GameObject> m_perkTreeIcons = new List<GameObject>();

    public PerkButton m_childPerkTreeButton;

    public void OnCursorEnter()
    {
        if (PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.m_perkUpgradeConfirmation.activeSelf)
        {
            return;
        }

        m_bIsHightlighted = true;
    }

    public void OnCursorExit()
    {
        m_bIsHightlighted = false;
    }

    public void OnClick()
    {
        if (PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.m_perkUpgradeConfirmation.activeSelf)
        {
            return;
        }

        ExpManager.m_experiencePointsManager.DisablePerkTree();

        //AudioManager.m_audioManager.PlayOneShotMenuClick();
        //PerkTreeManager.m_perkTreeManager.gameObject.SetActive(false);
        //PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(false);
        //IsoCam.m_playerCamera.gameObject.SetActive(true);
        //Time.timeScale = 1.0f;
        //m_perkTreePanel.SetActive(false);
        //
        //foreach (GameObject perkTreeIcon in m_perkTreeIcons)
        //{
        //    perkTreeIcon.SetActive(true);
        //}
    }

    private void Update()
    {
        if (m_bIsHightlighted)
        {
            Grow();
        }
        else
        {
            Shrink();
        }
    }

    private void Grow()
    {
        if (transform.localScale.x < m_fGrowMultiplier)
        {
            transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    private void Shrink()
    {
        if (transform.localScale.x > m_fShrinkMultiplier)
        {
            transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}
