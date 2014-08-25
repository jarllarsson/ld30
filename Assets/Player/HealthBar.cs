using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public int m_hp=2*3;
    public GameObject[] m_hearts;
    public GameObject[] m_heartsH;
    private int[] m_heartsPtHp;

    bool m_hurt = false;
    float m_invulnCount = 2.0f;
    float m_hurtTick = 0.0f;
    int m_latestHIndex = 0;
    public SpriteRenderer m_playerSprite;

    public bool hurtTrig=false;
    private bool m_latestHIndexVisibleEndstate;

	// Use this for initialization
	void Start () 
    {
        m_heartsPtHp = new int[m_hearts.Length];
	    for (int i=0;i<m_hearts.Length;i++)
        {
            m_heartsPtHp[i] = 2;
        }
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        if (hurtTrig)
        {
            decreaseHp();
            hurtTrig = false;
        }

        for (int i = 0; i < m_hearts.Length; i++)
        {
            if (m_hp == i*2+1) // half heart
            {
                if (m_heartsPtHp[i] > 1)
                {
                    m_hearts[i].SetActive(false);
                    m_heartsH[i].SetActive(true);
                    m_latestHIndex = i;
                    m_latestHIndexVisibleEndstate = true;
                    m_heartsPtHp[i] = 1;
                }
            }
            else if (m_hp < i*2+1) // empty heart
            {
                if (m_heartsPtHp[i] > 0)
                {
                    m_hearts[i].SetActive(false);
                    m_heartsH[i].SetActive(false);
                    m_latestHIndexVisibleEndstate = false;
                    m_latestHIndex = i;
                    m_heartsPtHp[i] = 0;
                }
            }
        }        
        
        if (m_hurt)
        {
            m_hurtTick -= Time.deltaTime;
            bool blink = ((int)(m_hurtTick * 100)) % 21 < 10;
            Debug.Log(blink);
            m_heartsH[m_latestHIndex].SetActive(blink);
            if (blink)
                m_playerSprite.color = Color.white;
            else
                m_playerSprite.color = new Color(1.0f,1.0f,1.0f,0.5f);
            if (m_hurtTick <= 0.0f)
            {
                m_hurt = false;
                m_playerSprite.color = Color.white;
                m_heartsH[m_latestHIndex].SetActive(m_latestHIndexVisibleEndstate);
            }
        }
	}

    public bool isDead()
    {
        return (m_hp <= 0);
    }

    public void decreaseHp()
    {
        if (!m_hurt)
        {
            m_hurt = true;
            m_hurtTick = m_invulnCount;
            m_hp -= 1;
        }
    }
}
