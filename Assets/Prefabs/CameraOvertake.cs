using UnityEngine;
using System.Collections;

public class CameraOvertake : MonoBehaviour {
    public Camera m_cam;
    public AudioListener m_listener;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void cameraOvertake()
    {
        AudioListener mainListener = Camera.main.gameObject.GetComponent<AudioListener>();
        if (mainListener) mainListener.enabled = false;
        Camera.main.enabled = false;
        m_cam.enabled = true;
        m_listener.enabled = true;
    }
}
