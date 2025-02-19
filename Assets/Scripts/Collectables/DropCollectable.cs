using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{
    [Range(0, 100)]
    public int m_healDropChance = 0;
    [Range(0, 100)]
    public int m_manaDropChance = 0;


    private int m_yellowOrbsMin = 1;
    private int m_yellowOrbsMax = 1;

    private int m_greenOrbsMin = 1;
    private int m_greenOrbsMax = 1;

    private int m_blueOrbsMin = 1;
    private int m_blueOrbsMax = 1;

    private EnemyLootManager m_lootManager;
    private Collectable m_Collectable;

    private void Start()
    {
        m_lootManager = GameObject.FindObjectOfType<EnemyLootManager>();
        m_Collectable = GameObject.FindObjectOfType<Collectable>();
    }

    private void OnDisable()
    {
        if (m_lootManager != null)
        {
            m_lootManager.RequestLootsplosion(this.transform.position, m_yellowOrbsMin, m_yellowOrbsMax, Collectable.CollectableType.YellowOrb);

            if(m_Collectable.m_healthCap)
            {
                m_healDropChance = 0;
            }
            else
            {
                m_healDropChance = 50;
            }
            
            if(m_Collectable.m_manaCap)
            {
                m_manaDropChance = 0;
            }
            else
            {
                m_manaDropChance = 50;
            }

            if (Random.Range(0, 100) <= m_healDropChance)
            {
                m_lootManager.RequestLootsplosion(this.transform.position, m_greenOrbsMin, m_greenOrbsMax, Collectable.CollectableType.GreenOrb);
            }

            if (Random.Range(0, 100) <= m_manaDropChance)
            {
                m_lootManager.RequestLootsplosion(this.transform.position, m_blueOrbsMin, m_blueOrbsMax, Collectable.CollectableType.BlueOrb);
            }
        }
    }
}
