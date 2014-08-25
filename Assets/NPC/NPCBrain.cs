using UnityEngine;
using System.Collections;

public class NPCBrain : MonoBehaviour 
{
    public WaypointWalker m_waypointHandler;

    public float m_gravity = 9.8f * 4.0f;
    public float m_linDragWalk = 20.0f;
    public Animator m_characterAnimator;
    float m_horiz;
    float m_vert;
    public float m_wallCollPenalty = 1.0f;
    public float m_walkspeed;

    public Worshipper m_worshipper;
    public Animator m_playerCharacterAnimator;

    private Vector3 m_randWalkDir;
    private float m_randWalkTick;
    private bool m_coolDown = true;

    private Vector3 m_oldPos;
    private float m_standingStillCounter = 0.0f;

    private int m_animFacingDirHash,m_animTriggerAttackHash, m_animIsAngryHash, m_animIsWorshippingHash;

    private bool m_angry = false;
    private float m_angryTime = 3.0f;
    private float m_angryTick = 0.0f;
    public Vector3 m_currentDir;

    public bool triggerAngry = false;
    private Transform m_player;

    private bool m_atkState;

	// Use this for initialization
	void Awake () {
        m_animFacingDirHash = Animator.StringToHash("npc_facing_dir");
        m_animTriggerAttackHash = Animator.StringToHash("npc_atk");
        m_animIsAngryHash = Animator.StringToHash("npc_isAngry");
        m_animIsWorshippingHash = Animator.StringToHash("npc_isWorshipping");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) m_player = player.transform;
	}
	
    void Update()
    {
        countStandingStillTime();
        if (triggerAngry)
            makeAngry();
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        if (!m_worshipper.isWorshipping())
            m_playerCharacterAnimator.SetBool(m_animIsWorshippingHash, false);
        Vector3 dir = Vector3.zero;

        if (m_coolDown && !m_angry) // if not strolling
            dir=m_waypointHandler.GetCurrentHeading(); // try to get to waypoint


        if ((dir == Vector3.zero || m_standingStillCounter>2.0f) && 
            !m_worshipper.isWorshipping() && !m_angry)
        {
            if (m_randWalkTick <= 0.0f)
            {
                m_randWalkTick = Random.Range(1.0f, 8.0f);
                m_coolDown = !m_coolDown; // pingpong
                if (m_coolDown)
                    m_randWalkDir = Vector3.zero;
                else
                {
                    m_worshipper.disableWorshipping();
                    m_randWalkDir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
                }
            }
            else
            {
                m_randWalkTick -= Time.deltaTime;
            }
            dir = m_randWalkDir;
        }


        m_wallCollPenalty = Mathf.Lerp(m_wallCollPenalty, 1.0f, 0.2f);

        rigidbody.drag = m_linDragWalk;

        Vector3 combine = Vector3.ClampMagnitude(dir, 1.0f);
        float spd = m_walkspeed;
        if (m_angry && m_player)
        { 
            spd *= 1.5f; 
            combine = Vector3.Normalize(m_player.position - transform.position);
            combine = new Vector3(combine.x, 0.0f, combine.z);
        }
        if (!m_atkState) rigidbody.AddForce(combine * spd/* * m_wallCollPenalty*/);
        Debug.DrawLine(transform.position, transform.position + combine, new Color(combine.x,.5f,combine.z), 1.0f);


        //Debug.Log((transform.position - m_oldPos).sqrMagnitude);
        if ((transform.position - m_oldPos).sqrMagnitude > 0.0001f)
        {                
            //ActivateAllWalkAnims();
            //DeactivateAllIdleAnims();
            //
            //
            //
            ////             Vector3 camRot = Camera.main.transform.rotation.eulerAngles;
            ////             Quaternion currentFacing = Quaternion.Euler(0.0f, camRot.y, 0.0f);
            m_currentDir = combine/* - (Camera.main.transform.right - Camera.main.transform.forward)*/;
            //m_currentRealDir = (m_recentFacing * Vector3.Normalize(combine));
            if (m_currentDir.x > 0.0f) // right
            {
                m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 3);
            }
            else if (m_currentDir.x <= 0.0f) // left
            {
                m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 2);
            }

        }
        else
        {
            if (m_worshipper.isWorshipping())
                m_playerCharacterAnimator.SetBool(m_animIsWorshippingHash, true);
            else
                m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 0);
            //DeactivateAllWalkAnims();
            //ActivateAllIdleAnims();
        }


        if (m_angry)
        {
            m_angryTick -= Time.deltaTime;

            if (Random.Range(0,1000)>990)
            {
                attack();
            }

            if (m_angryTick<=0.0f)
            {
                m_angry = false;
                setAtkState(false);
                m_playerCharacterAnimator.SetBool(m_animIsAngryHash, false);
            }
        }
        m_oldPos = transform.position;
        rigidbody.AddForce(Vector3.down * m_gravity);
	}

    void countStandingStillTime()
    {
        if ((transform.position - m_oldPos).magnitude < 0.01f)
            m_standingStillCounter += Time.deltaTime;
        else
            m_standingStillCounter = 0.0f;
    }

    public void makeAngry()
    {
        m_angry = true;
        m_worshipper.disableWorshipping();
        m_angryTick = m_angryTime;
        m_playerCharacterAnimator.SetBool(m_animIsAngryHash, true);
    }

    public void setAtkState(bool p_state)
    {
        m_atkState = p_state;
        m_playerCharacterAnimator.SetBool(m_animTriggerAttackHash, m_atkState);
    }

    void attack()
    {
        if (!m_atkState) setAtkState(true);
    }
}
