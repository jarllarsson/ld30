using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
    public AudioSource m_audio;
    public bool m_auto = false;
	// Use this for initialization
	void OnEnable() 
    {
        if (m_auto)
            playSound();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void playSound()
    {
        if (!m_audio.isPlaying)
            m_audio.Play();
    }
}
