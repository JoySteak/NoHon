using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapManagerScript : MonoBehaviour {
	public static TrapManagerScript current;
	public List<GameObject> m_trapPool;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start(){
		m_trapPool = new List<GameObject>();
		GameObject[] tmpTraps = GameObject.FindGameObjectsWithTag("Trap");
		for (int i = 0; i < tmpTraps.Length; i++) {
			m_trapPool.Add(tmpTraps[i]);
		}
	}
	
	// Update is called once per frame
	void Update(){
	
	}
}
