using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	float constant = 0.01f;
	[SerializeField]
	GameObject target = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//when player dies, target=null, stop following
		if(target==null) return;
		
		//calculate new camera position
		float cx = (1-constant)*this.transform.position.x+constant*target.transform.position.x;
		float cy = (1-constant)*this.transform.position.y+constant*target.transform.position.y;
		float cz = this.transform.position.z;
		this.transform.position = new Vector3(cx,cy,cz);
	}
}
