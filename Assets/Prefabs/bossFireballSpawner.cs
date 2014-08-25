using UnityEngine;
using System.Collections;

public class bossFireballSpawner : MonoBehaviour 
{
    int m_mana=10;
    int m_manaUse=0;
    public float m_cooldown = 10.0f;
    public float m_cooldownTick = 0.0f;

    public float m_subcooldown = 1.0f;
    public float m_subcooldownTick=0.0f;

    public Transform m_fireball;
    public Transform m_spawnPoint;

	// Use this for initialization
	void Start () {
        m_cooldownTick = m_cooldown;
        m_manaUse = m_mana;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_cooldownTick<=0.0f)
        {
            if (m_subcooldownTick<=0.0f)
            {
                Instantiate(m_fireball, m_spawnPoint.position, m_spawnPoint.rotation);
                m_subcooldownTick = m_subcooldown;
                m_manaUse--;
                if (m_manaUse <= 0)
                {
                    m_manaUse = m_mana;
                    m_cooldownTick = m_cooldown;
                }
            }
            else
            {
                m_subcooldownTick -= Time.deltaTime;
            }
        }
        else
        {
            m_cooldownTick -= Time.deltaTime;
        }
	}
}
