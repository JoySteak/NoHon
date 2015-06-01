using UnityEngine;
using System.Collections;

public class TreasureCheckPoint : MonoBehaviour
{
	public LayerMask m_playerLayerMask;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Physics2D.OverlapCircle(transform.position, 1.0f, m_playerLayerMask))
		{
			Debug.Log("Victory!");
		}

	}
}
