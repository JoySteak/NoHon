using UnityEngine;
using System.Collections;

public class VerWallReposition : MonoBehaviour {

	public int m_wallPosition = 0;
	
	private bool m_repositionDone = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_repositionDone && CameraSize.current.m_width != 0.0f) {
			Vector3 tmpPosition = gameObject.transform.position;
			switch(m_wallPosition) {
			case 0: tmpPosition.x = -(CameraSize.current.m_width / 2.0f);
				break;
			case 1: tmpPosition.x = (CameraSize.current.m_width / 2.0f);
				break;
			}
			gameObject.transform.position = tmpPosition;
			
			m_repositionDone = true;
		}
	}
}
