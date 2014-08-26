using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bossFireballSpawner : MonoBehaviour 
{
    int m_mana=0;
    int m_manaUse=0;
    public float m_cooldown = 10.0f;
    public float m_cooldownTick = 0.0f;

    public float m_subcooldown = 1.0f;
    public float m_subcooldownTick=0.0f;

    public Transform m_fireball;
    public Transform m_bossHead;
    public Transform m_spawnPoint;
    public Text m_text;

    public Collider m_playerHindrance;

	// Use this for initialization
	void Start () {
        m_cooldownTick = m_cooldown;
        m_manaUse = m_mana;
        m_text.text = "x " + m_mana;
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        float manascale = Mathf.Min(((float)(m_mana+1)*0.1f+0.25f),1.011f);
        m_bossHead.localScale = Vector3.Lerp(m_bossHead.localScale,new Vector3(manascale, manascale, manascale),Time.deltaTime);
        m_text.text = "x " + m_mana;

        if (m_mana <= 0)
        {
            m_playerHindrance.enabled = false;
            m_playerHindrance.transform.localScale = Vector3.Lerp(m_playerHindrance.transform.localScale,
                new Vector3(m_playerHindrance.gameObject.transform.localScale.x, 0.0f, m_playerHindrance.transform.localScale.z), Time.deltaTime);
        }
        else
        {
            m_playerHindrance.transform.localScale = Vector3.Lerp(m_playerHindrance.transform.localScale,
    new Vector3(m_playerHindrance.gameObject.transform.localScale.x, 12.0f, m_playerHindrance.transform.localScale.z), Time.deltaTime);
            m_playerHindrance.enabled = true;
        }

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
        m_mana = 0; // recalc every regular update.
	}

    public void addMana(int p_amound)
    {
        m_mana+=p_amound;
    }

}
