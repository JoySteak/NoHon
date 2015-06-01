using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	public bool m_facingRight = false;
	public float m_maxSpeed = 5.0f;

    public Character tmpPlayerRef = null;
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
        if(gameObject.activeSelf)
		    BulletMovement ();

        if(transform.position.x > 20.0f || transform.position.x < -20.0f)
        {
            gameObject.SetActive(false);
            tmpPlayerRef = null;
        }
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
