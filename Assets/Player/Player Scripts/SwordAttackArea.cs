using UnityEngine;
using System.Collections;

public class SwordAttackArea : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
        if (p_coll.gameObject.tag == "NPC")
        {
            OnNPCCollide(p_coll);
        }
    }

    void OnNPCCollide(Collider p_coll)
    {
        Destroy(p_coll.gameObject);
    }
}
