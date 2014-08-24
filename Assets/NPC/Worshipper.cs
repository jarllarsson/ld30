using UnityEngine;
using System.Collections;

public class Worshipper : MonoBehaviour 
{
   	public class PlateInfo
	{
		public PlateInfo(WorshipStone.WorshipPlate p_plate)
		{
			m_worshipPlate=p_plate;
		}
		// wrap because unity instantiates the plate otherwise
		// as it is serializable
		public WorshipStone.WorshipPlate m_worshipPlate;
	}

	public PlateInfo m_plateInfo=null;

    public WorshipStone m_myHomeStone = null;

    public float m_searchTime = 15.0f;
    private float m_searchTimeTick = 0.0f;


	// Use this for initialization
	void Start () 
    {
        m_searchTimeTick = Random.Range(0.0f, m_searchTime / 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_searchTimeTick += Time.deltaTime;
        if (m_searchTimeTick>=m_searchTime)
        {
            m_searchTimeTick = 0.0f;
            if (m_myHomeStone && !hasFoundFreeWorshipPlate())
            {
                m_myHomeStone.registerWorshipper(this);
            }
        }


        if (m_myHomeStone && hasFoundFreeWorshipPlate())
        {
            Debug.DrawLine(transform.position, m_myHomeStone.transform.position, Color.magenta);
            Debug.DrawLine(m_myHomeStone.transform.position, m_plateInfo.m_worshipPlate.m_plateTransform.position, Color.magenta);
        }


	}

    void die()
    {
        if (hasFoundFreeWorshipPlate())
        {
            m_plateInfo.m_worshipPlate.m_worshipper = null;
			m_plateInfo=null;
        }
    }

    bool hasFoundFreeWorshipPlate()
    {
        return m_plateInfo != null;
    }

	public void setPlate(WorshipStone.WorshipPlate p_plate)
	{
		m_plateInfo = new PlateInfo(p_plate);
	}
}
