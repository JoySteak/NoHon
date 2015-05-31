using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	public bool m_facingRight = true;
	public float m_maxSpeed = 5.0f;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	void BulletMovement()
	{
		if (m_facingRight)
        {
			rigidbody2D.velocity = new Vector2 (m_maxSpeed, rigidbody2D.velocity.y);
		}
        else
        {
			rigidbody2D.velocity = new Vector2 (-m_maxSpeed, rigidbody2D.velocity.y);
		}
	}
}
