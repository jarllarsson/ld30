using UnityEngine;
using System.Collections;

public class WorshipStone : MonoBehaviour 
{

    [System.Serializable]
    public class WorshipPlate
    {
        public Transform m_plateTransform;
        [HideInInspector]
        public Worshipper m_worshipper = null;
    }

    public WorshipPlate[] m_plates;


    public float m_percentageOutput; // Based on number of occupied plates.
    public float m_integrity = 100.0f; // hp


    public ParticleSystem m_worshipPercentageFx;
    public Transform m_bossPos;

    private LineRenderer m_energyLine;
    public Light m_lamp;
    float m_intAnimTick = 0.0f;
    private bossFireballSpawner m_bossManaRecepticle;

    float m_lenToBoss;

    public float getManaOutput()
    {
        return (float) m_plates.Length* m_percentageOutput;
    }

	// Use this for initialization
	void Start () 
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj) m_bossManaRecepticle = bossObj.GetComponent<bossFireballSpawner>();
        GameObject bossEyeObj = GameObject.FindGameObjectWithTag("BossEye");
        m_bossPos = bossEyeObj.transform;
        if (m_energyLine == null)
            m_energyLine = transform.GetComponentInChildren<LineRenderer>();

        m_lenToBoss = Vector3.Magnitude(m_bossPos.position - transform.position);
	}
	
	// Update is called once per frame
	void Update () 
    {
        int activeWorshippers = calcWorshippers();
        m_bossManaRecepticle.addMana(activeWorshippers);
        if (m_plates.Length>0)
            m_percentageOutput = (float)activeWorshippers / (float)m_plates.Length;

        //
        if (m_percentageOutput>0.0f)
        {
            if (!m_energyLine.enabled)
            {
                float lenToBoss = Vector3.Magnitude(m_bossPos.position - transform.position);
                m_energyLine.enabled = true;
                int vcount = (int)(m_lenToBoss * 0.1f);
                m_energyLine.SetVertexCount(vcount);
                for (int i=0;i<vcount;i++)
                {
                    float t=(float)i / (float)vcount;
                    Vector3 linPos = Vector3.Lerp(transform.position, m_bossPos.position, t);
                    linPos += Vector3.up*(Mathf.Sin(t*Mathf.PI)*150.0f);
                    m_energyLine.SetPosition(i, linPos);
                }
                m_lamp.enabled = true;
            }
            else
            {
                if (m_intAnimTick<1.0f) m_intAnimTick+=0.5f*Time.deltaTime;
                m_lamp.intensity = m_intAnimTick;
                m_energyLine.material.mainTextureScale = new Vector2(Mathf.Lerp(m_lenToBoss*0.2f,0.5f,m_intAnimTick),1.0f);
            }
        }
        else
        {
            m_energyLine.SetVertexCount(0);
            m_energyLine.enabled=false;
            m_energyLine.material.mainTextureScale = new Vector2(10000.0f, 1.0f);
            m_intAnimTick = 0.0f;
            m_lamp.enabled = false;
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
            if (plate.m_worshipper != null)
            {
                if (plate.m_worshipper.isWorshipping())
                    count++;
            }
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
                plate.m_worshipper=p_self;
                p_self.setPlate(plate);
                reVal = true;
                break;
            }
        }
        return reVal;
    }

}
