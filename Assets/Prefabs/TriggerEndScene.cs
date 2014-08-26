using UnityEngine;
using System.Collections;

public class TriggerEndScene : MonoBehaviour {
    public GameObject m_endScene;
    public GameObject m_oldBoss;
    public GameObject m_oldPlayer;
    public GameObject m_music;
    public GameObject m_hud;
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
            m_oldBoss.GetComponent<bossFireballSpawner>().enabled = false;
            m_oldPlayer.GetComponent<Player>().enabled = false;
            m_oldBoss.SetActive(false);
            m_oldPlayer.SetActive(false);
            m_music.SetActive(false);
            m_hud.SetActive(false);
            
            GameObject[] fireballs = GameObject.FindGameObjectsWithTag("fireball");
            foreach (GameObject fball in fireballs)
            {
                Destroy(fball);
            }
            collider.enabled = false;
        }
    }


}
