using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{

    public Animator m_playerCharacterAnimator;
    private int m_animAttackHash;
    public AudioSource[] m_swordSfxs;

    public bool m_attackBtn=false, m_isAttacking=false, m_canAttack=true;

	// Use this for initialization
	void Awake () 
    {
        m_animAttackHash = Animator.StringToHash("attack");
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_attackBtn = Input.GetAxis("Fire1")>0.0f;
        if (m_attackBtn && !m_isAttacking && m_canAttack)
        {
            m_playerCharacterAnimator.SetBool(m_animAttackHash,true);
            m_isAttacking = true;
            m_canAttack = false;

        }
        if (!m_attackBtn && !m_isAttacking)
        {
            m_canAttack = true;
        }
	}


    public void disableAttack()
    {
        m_isAttacking = false;
        m_playerCharacterAnimator.SetBool(m_animAttackHash, false);
    }

    public void attackSound()
    {
        int soundId = Random.Range(0, m_swordSfxs.Length);
        //Debug.Log(soundId);
        if (!m_swordSfxs[soundId].isPlaying)
            m_swordSfxs[soundId].Play();
        else
        {
            for (int x = 0; x < m_swordSfxs.Length; x++)
            {
                if (!m_swordSfxs[x].isPlaying) m_swordSfxs[x].Play();
                break;
            }
        }
    }

}
