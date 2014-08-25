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
        else if (p_coll.gameObject.tag == "Grass")
        {
            GrasCut grasscut = p_coll.gameObject.GetComponent<GrasCut>();
            if (grasscut) grasscut.cut();
        }
    }

    void OnNPCCollide(Collider p_coll)
    {
        NPCBrain brain = p_coll.GetComponent<NPCBrain>();
        brain.setHurt();
        Vector3 fVec = Vector3.Normalize(p_coll.transform.position-transform.parent.position);
        fVec = new Vector3(fVec.x,0.0f,fVec.z);
        p_coll.gameObject.rigidbody.AddForce(fVec * 100.0f);
    }
}
