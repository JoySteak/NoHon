using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){

		//check if parent has hp
		//if parent is destroyed?
		//instantiate new treasure on the same spot OR no longer is child of parent
	
	}

	void OnTriggerEnter2D(Collider2D other){

		//if no parent
		//if other is a player
		//treasure becomes child of player, held on top of player's head
	}
}
