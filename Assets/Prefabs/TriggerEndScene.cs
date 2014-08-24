using UnityEngine;
using System.Collections;

public class TriggerEndScene : MonoBehaviour {
    public GameObject m_endScene;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider p_coll)
    {
        OnAllCollide(p_coll);
    }

    void OnTriggerStay(Collider p_coll)
    {
        OnAllCollide(p_coll);
    }

    void OnAllCollide(Collider p_coll)
    {
        if (p_coll.gameObject.tag == "Player")
        {
            m_endScene.SetActive(true);
            collider.enabled = false;
        }
    }


}
