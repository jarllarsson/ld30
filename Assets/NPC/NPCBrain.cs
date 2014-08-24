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

    private Vector3 m_randWalkDir;
    private float m_randWalkTick;
    private bool m_coolDown = true;

    private Vector3 m_oldPos;
    private float m_standingStillCounter = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
    void Update()
    {
        countStandingStillTime();
        m_oldPos = transform.position;
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 dir = Vector3.zero;

        if (m_coolDown) // if not strolling
            dir=m_waypointHandler.GetCurrentHeading(); // try to get to waypoint


        if ((dir == Vector3.zero || m_standingStillCounter>2.0f) && !m_worshipper.isWorshipping())
        {
            if (m_randWalkTick <= 0.0f)
            {
                m_randWalkTick = Random.Range(1.0f, 8.0f);
                m_coolDown = !m_coolDown; // pingpong
                if (m_coolDown)
                    m_randWalkDir = Vector3.zero;
                else
                    m_randWalkDir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
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
        rigidbody.AddForce(combine * m_walkspeed/* * m_wallCollPenalty*/);
        Debug.DrawLine(transform.position, transform.position + combine, new Color(combine.x,.5f,combine.z), 1.0f);



        if (Mathf.Abs(m_horiz) > 0.00001f || Mathf.Abs(m_vert) > 0.00001f)
        {
            //ActivateAllWalkAnims();
            //DeactivateAllIdleAnims();
            //
            //
            //
            ////             Vector3 camRot = Camera.main.transform.rotation.eulerAngles;
            ////             Quaternion currentFacing = Quaternion.Euler(0.0f, camRot.y, 0.0f);
            //m_currentDir = combine/* - (Camera.main.transform.right - Camera.main.transform.forward)*/;
            //m_currentRealDir = (m_recentFacing * Vector3.Normalize(combine));
            //if (m_currentDir.x > 0.0f) // right
            //{
            //    m_playerCharacterAnimator.SetInteger(m_animMoveDirHash, 3);
            //    m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 3);
            //}
            //else if (m_currentDir.x < 0.0f) // left
            //{
            //    m_playerCharacterAnimator.SetInteger(m_animMoveDirHash, 2);
            //    m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 2);
            //}
            //else if (m_currentDir.z > 0.0f) // up (backface)
            //{
            //    m_playerCharacterAnimator.SetInteger(m_animMoveDirHash, 4);
            //    m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 4);
            //}
            //else if (m_currentDir.z < 0.0f) // down (frontface)
            //{
            //    m_playerCharacterAnimator.SetInteger(m_animMoveDirHash, 1);
            //    m_playerCharacterAnimator.SetInteger(m_animFacingDirHash, 1);
            //}

        }
        else
        {

            //DeactivateAllWalkAnims();
            //ActivateAllIdleAnims();
        }
        rigidbody.AddForce(Vector3.down * m_gravity);
	}

    void countStandingStillTime()
    {
        if ((transform.position - m_oldPos).magnitude < 0.01f)
            m_standingStillCounter += Time.deltaTime;
        else
            m_standingStillCounter = 0.0f;
    }
}
