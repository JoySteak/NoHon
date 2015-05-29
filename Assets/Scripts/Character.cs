using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public float m_maxSpeed = 5.0f;
	
	// Flip Indicator
	private bool m_facingRight = true;
	
	// Jump Usage
	bool m_grounded = false;
	public Transform m_groundCheck;
	float m_groundRadius = 0.2f;
	public LayerMask m_whatIsGround;
	public float m_jumpForce = 700.0f;
	public bool m_interacted = false;
	
	// Available Type
	public enum CharacterType
	{
		A = 0,
		B,
		C,
		D
	};
	public CharacterType m_type = CharacterType.A;

	Character()
	{
		m_type = CharacterType.A;
	}

	Character(CharacterType type)
	{
		m_type = type;
	}
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		JumpMovement();
	}
	
	void FixedUpdate()
	{
		HorizontalMovement();
	}
	
	void HorizontalMovement()
	{
		m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);
		
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * m_maxSpeed, rigidbody2D.velocity.y);
		
		
		if(move > 0.0f && !m_facingRight)
		{
			Flip();
		}
		else if(move < 0.0f && m_facingRight)
		{
			Flip();
		}
	}
	
	void JumpMovement()
	{
		if(m_grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody2D.AddForce(new Vector2(0.0f, m_jumpForce));
		}
	}
	
	void Flip()
	{
		m_facingRight = !m_facingRight;
		
		// Set the flip scale
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}