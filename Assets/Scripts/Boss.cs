using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
		//boss moves left and right randomly?
		//shoots a bullet from in front in fixed intervals
		//the y value of the bullet spawn coords go up and down
	}

	void OnCollisionEnter2D(Collision2D other){
		//if hit by bullet, get damaged
		//if hp = 0, dies, spawns treasure at death coords
	}
}
