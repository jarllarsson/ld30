using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public float m_walkspeed;
    public swing[]  m_walkswings;
    public wobble[] m_walkwobbles;
    public swing[]  m_idleswings;
    public wobble[] m_idlewobbles;
    public float m_gravity = 9.8f * 4.0f;
    public float m_jumpForce = 15.0f;
    private bool m_jumpToggle = true;
    private bool m_hasBegunFalling = false;
    private bool m_onGround = false;
    private float m_oldVelY;
    public float m_linDragJump = 10.0f;
    public float m_linDragWalk = 20.0f;
    public Transform m_playerCharacterTransform;
    private float m_inAirBoost=0.0f;
    float m_horiz;
    float m_vert;
    bool m_jumpTrigger=false;
    public float m_wallCollPenalty = 1.0f;

    private List<float> m_speeds = new List<float>();
    private Vector3 m_oldPos;


	// Use this for initialization
	void Awake() 
    {
        SetSpeedOfAllWalkAnims(1.2f);
        DeactivateAllWalkAnims();
        foreach (swing n in m_idleswings)
            if (n != null) n.m_active = false;
        foreach (wobble n in m_idlewobbles)
            if (n != null) n.m_active = false;
	}
	
    void Update()
    {
        //m_speeds.Add((transform.position - m_oldPos).x);
        //while (m_speeds.Count > 100) m_speeds.RemoveAt(0);
        //m_oldPos = transform.position;
    }

    void ActivateAllWalkAnims()
    {
        foreach (swing n in m_walkswings)
            if (n != null) n.m_active = true;
        foreach (wobble n in m_walkwobbles)
            if (n != null) n.m_active = true;
    }

    void ActivateAllIdleAnims()
    {
        foreach (swing n in m_idleswings)
            if (n != null) n.m_active = true;
        foreach (wobble n in m_idlewobbles)
            if (n != null) n.m_active = true;
    }

    void SetSpeedOfAllWalkAnims(float p_speed)
    {
        foreach (swing n in m_walkswings)
            if (n != null) n.m_speedMp = p_speed;
        foreach (wobble n in m_walkwobbles)
            if (n != null) n.m_speedMp = p_speed;
    }


    void DeactivateAllWalkAnims()
    {
        foreach (swing n in m_walkswings)
            if (n != null) n.m_active = false;
        foreach (wobble n in m_walkwobbles)
            if (n != null) n.m_active = false;
    }

    void DeactivateAllIdleAnims()
    {
        foreach (swing n in m_idleswings)
            if (n != null) n.m_active = false;
        foreach (wobble n in m_idlewobbles)
            if (n != null) n.m_active = false;
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        m_horiz = Input.GetAxis("Horizontal");
        m_vert = Input.GetAxis("Vertical");
        m_jumpTrigger = Input.GetAxis("Jump") > 0.0f;  
      
        m_wallCollPenalty = Mathf.Lerp(m_wallCollPenalty,1.0f,0.2f);
        if (m_onGround)
        {
            rigidbody.drag = m_linDragWalk;
            m_inAirBoost = 1.0f;
        }
        else
        {
            rigidbody.drag = m_linDragJump;
            m_inAirBoost = 1.0f / m_linDragWalk;
        }
        Vector3 combine = Vector3.ClampMagnitude(Vector3.right * m_horiz + Vector3.forward * m_vert,1.0f);
        rigidbody.AddForce(combine * m_inAirBoost * m_walkspeed/* * m_wallCollPenalty*/);
        //Debug.DrawLine(transform.position, transform.position + combine, new Color(combine.x,.5f,combine.z), 1.0f);

        if (Mathf.Abs(m_horiz) > 0.1f || Mathf.Abs(m_vert) > 0.1f)
        {
            ActivateAllWalkAnims();
            DeactivateAllIdleAnims();

            if (m_playerCharacterTransform)
                m_playerCharacterTransform.LookAt(m_playerCharacterTransform.position + combine);
/*
            if (m_horiz > 0.0f)
                m_playerSprite.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else
                m_playerSprite.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
 * */
        }
        else
        {
            DeactivateAllWalkAnims();
            ActivateAllIdleAnims();
        }

        // THE JUMP CODEZ
        if (m_jumpTrigger)
        {
            if (m_onGround && m_jumpToggle)
            {
                m_hasBegunFalling = false;
                m_onGround = false;
                rigidbody.drag = m_linDragJump;
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector2.up * m_jumpForce);
            }
            m_jumpToggle = false;
        }
        else
        {
            m_jumpToggle = true;
        }
        //
        if (rigidbody.velocity.y <= 0.0f)
        {
            m_hasBegunFalling = true;
            if (rigidbody.velocity.y < 0.1f) m_onGround = false;
        }
        if (rigidbody.velocity.y < 0.05f && rigidbody.velocity.y >= 0.0f)
        {
            if (m_hasBegunFalling && m_oldVelY<=0.0f)
                m_hasBegunFalling = false;
            //m_onGround = true;
        }
        m_onGround = false;
        m_oldVelY = rigidbody.velocity.y;

        rigidbody.AddForce(Vector3.down * m_gravity);
	}

    void OnCollisionStay(Collision p_hit)
    {
        //WallCollision(p_hit);
        GroundCollision(p_hit);
    }

    void OnCollisionEnter(Collision p_hit)
    {
        //WallCollision(p_hit);
        GroundCollision(p_hit);
    }

    void WallCollision(Collision p_hit)
    {
        if (rigidbody.velocity.y<0.1f || rigidbody.velocity.y>-0.1f)
        {
            Vector3 normal = Vector3.zero;
            int count = 0;
            foreach (ContactPoint p in p_hit.contacts)
            {
                normal += p.normal;
                count++;
            }
            normal /= (float)count;
            if (Mathf.Abs(normal.x) > 0.5f)
            {
                //rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);
                m_wallCollPenalty = 0.0f;
                //Debug.DrawLine(p_hit.contacts[0].point, p_hit.contacts[0].point + normal,Color.green,1.0f);
                rigidbody.AddForce(new Vector2(normal.x * m_walkspeed * m_inAirBoost, 0.0f));
            }
        }
    }

    void GroundCollision(Collision p_hit)
    {
        Vector3 normal = Vector2.zero;
        int count = 0;
        foreach (ContactPoint p in p_hit.contacts)
        {
            normal += p.normal;
            count++;
        }
        normal /= (float)count;
        if (normal.y > 0.5f)
        {
            //Debug.DrawLine(p_hit.contacts[0].point, p_hit.contacts[0].point + normal, Color.red, 1.0f);
            m_onGround = true;
        }
        // Debug.Log("!");
    }

}
