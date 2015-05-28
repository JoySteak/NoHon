using UnityEngine;
using System.Collections;

public class CameraSize : MonoBehaviour {

	// Use this for initialization
	public static CameraSize current;
	
	public float m_height = 0.0f;
	public float m_width = 0.0f;
	
	void Awake() {
		current = this;
		
		Camera cam = Camera.main;
		m_height = 2.0f * cam.orthographicSize;
		m_width = m_height * cam.aspect;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
