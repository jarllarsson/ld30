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
    public WaypointWalker m_wayPointer;

    public float m_searchTime = 15.0f;
    private float m_searchTimeTick = 0.0f;

    private WayPoint m_worshipTargetJob = null;
    private bool m_isWorshipping = false;


	// Use this for initialization
	void Start () 
    {
        m_searchTimeTick = Random.Range(0.0f, m_searchTime / 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Search for worship plates
        m_searchTimeTick += Time.deltaTime;
        if (m_searchTimeTick>=m_searchTime)
        {
            m_searchTimeTick = 0.0f;
            if (m_myHomeStone && !hasFoundFreeWorshipPlate())
            {
                if (m_myHomeStone.registerWorshipper(this))
                {
                    WayPoint wp = new WayPoint(m_plateInfo.m_worshipPlate.m_plateTransform.position);
                    m_worshipTargetJob = wp;
                    m_wayPointer.m_waypoints.Push(wp);
                }
            }
        }

        // If we have a worship plate as target,
        // see if we have reached it, and if so, set status to "worshipping"
        // NOTE, might wanna change to some simple FSM here later.
        if (m_worshipTargetJob!=null &&
            m_worshipTargetJob.isClose(transform.position))
        {
            m_isWorshipping = true;
            m_worshipTargetJob = null;
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

    public bool isWorshipping()
    {
        return m_isWorshipping;
    }

	public void setPlate(WorshipStone.WorshipPlate p_plate)
	{
		m_plateInfo = new PlateInfo(p_plate);
	}
}
