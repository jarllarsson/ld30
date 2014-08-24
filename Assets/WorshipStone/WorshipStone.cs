using UnityEngine;
using System.Collections;

public class WorshipStone : MonoBehaviour 
{

    [System.Serializable]
    public class WorshipPlate
    {
        public Transform m_plateTransform;
        [HideInInspector]
        public GameObject m_worshipper = null;
    }

    public WorshipPlate[] m_plates;


    public float m_percentageOutput; // Based on number of occupied plates.
    public float m_integrity = 100.0f; // hp


    public ParticleSystem m_worshipPercentageFx;
    public Transform m_bossPos;

    private LineRenderer m_energyLine;
    float intAnimTick = 0.0f;

    float m_lenToBoss;

	// Use this for initialization
	void Start () 
    {
        if (m_energyLine == null)
            m_energyLine = transform.GetComponentInChildren<LineRenderer>();

        m_lenToBoss = Vector3.Magnitude(m_bossPos.position - transform.position);
	}
	
	// Update is called once per frame
	void Update () 
    {
        int activeWorshippers = calcWorshippers();
        if (m_plates.Length>0)
            m_percentageOutput = (float)activeWorshippers / (float)m_plates.Length;

        //
        if (m_percentageOutput>0.0f)
        {
            if (!m_energyLine.enabled)
            {
                float lenToBoss = Vector3.Magnitude(m_bossPos.position - transform.position);
                m_energyLine.enabled = true;
                int vcount = (int)(m_lenToBoss * 0.01f);
                m_energyLine.SetVertexCount(vcount);
                for (int i=0;i<vcount;i++)
                {
                    float t=(float)i / (float)vcount;
                    Vector3 linPos = Vector3.Lerp(transform.position, m_bossPos.position, t);
                    linPos += Vector3.up*(Mathf.Sin(t*Mathf.PI)*150.0f);
                    m_energyLine.SetPosition(i, linPos);
                }
            }
            else
            {
                if (intAnimTick<1.0f) intAnimTick+=0.5f*Time.deltaTime;
                m_energyLine.material.mainTextureScale = new Vector2(Mathf.Lerp(m_lenToBoss*0.2f,0.5f,intAnimTick),1.0f);
            }
        }
        else
        {
            m_energyLine.SetVertexCount(0);
            m_energyLine.enabled=false;
        }
        
        if (m_worshipPercentageFx)
        {
            m_worshipPercentageFx.emissionRate = m_percentageOutput * 10.0f;
        }
	}

    int calcWorshippers()
    {
        int count = 0;
        foreach (WorshipPlate plate in m_plates)
        {
            if (plate.m_worshipper != null) count++;
        }
        return count;
    }

    public bool registerWorshipper(Worshipper p_self)
    {
        bool reVal = false;
        foreach (WorshipPlate plate in m_plates)
        {
            if (plate.m_worshipper == null)
            {
                plate.m_worshipper=p_self.gameObject;
                p_self.setPlate(plate);
                reVal = true;
                break;
            }
        }
        return reVal;
    }

}
