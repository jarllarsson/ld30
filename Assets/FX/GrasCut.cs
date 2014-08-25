using UnityEngine;
using System.Collections;

public class GrasCut : MonoBehaviour
{
    public SpriteRenderer[] m_sprites;
    public bool m_cut;
    public bool trig;
    float m_animTime = 0.3f;
    float m_animTimeTick;
    public AudioSource m_sound;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (trig) cut();
	    if (m_cut && m_animTimeTick>0.0f)
        {
            foreach (SpriteRenderer spr in m_sprites)
            {
                spr.transform.position += Vector3.up*Time.deltaTime*2.0f;
                if (m_animTimeTick>m_animTime*0.2f)
                    spr.color = new Color(spr.color.r, spr.color.g, spr.color.b,
                        m_animTimeTick / (m_animTime*0.8f));
            }
            m_animTimeTick-=Time.deltaTime;
            if (m_animTimeTick<=0.0f)
            {
                m_animTimeTick = -10.0f;
                foreach (SpriteRenderer spr in m_sprites)
                {
                    Destroy(spr.gameObject);
                }
                m_sprites = null;
            }
        }
	}

    public void cut()
    {
        if (!m_cut)
        {
            m_animTimeTick = m_animTime;
            m_cut = true;
            m_sound.Play();
        }
    }
}
