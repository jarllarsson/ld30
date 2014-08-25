using UnityEngine;
using System.Collections;

public class NPCAttackArea : MonoBehaviour {

    public HealthBar m_playerHealth;


    // Use this for initialization
    void Start()
    {
        GameObject hpBarObj = GameObject.FindGameObjectWithTag("HPBar");
        if (hpBarObj) m_playerHealth = hpBarObj.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
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
            OnPlayerCollide(p_coll);
        }
        else if (p_coll.gameObject.tag == "Grass")
        {
            GrasCut grasscut = p_coll.gameObject.GetComponent<GrasCut>();
            if (grasscut) grasscut.cut();
        }
    }

    void OnPlayerCollide(Collider p_coll)
    {
        if (m_playerHealth && p_coll)
        {
            m_playerHealth.decreaseHp();
            Vector3 pos = transform.position;
            if (transform.parent) pos = transform.parent.position;
            Vector3 fVec = Vector3.Normalize(p_coll.transform.position - pos);
            fVec = new Vector3(fVec.x, 0.0f, fVec.z);
            p_coll.gameObject.rigidbody.AddForce(fVec * 300.0f);
        }
    }
}
