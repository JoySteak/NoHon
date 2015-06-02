using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapManagerScript : MonoBehaviour {
	public static TrapManagerScript current;
	public List<GameObject> m_trapPool = new List<GameObject>();

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
	
	}
}
