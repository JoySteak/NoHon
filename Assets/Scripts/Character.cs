using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : Photon.MonoBehaviour
{
	public float m_maxSpeed = 5.0f;
	
	// Flip Indicator
	private bool m_facingRight = true;
	
	// Jump Usage
	bool m_grounded = false;
	public Transform m_groundCheck;
	float m_groundRadius = 0.2f;
	public LayerMask m_whatIsGround;
	public float m_jumpForce = 800.0f;

	// Interation With Trap
	public bool m_interacted = false;
	private float m_interactedDelay = 0.0f;

    // Shoot Delay
    private float m_shotRate = 0.3f;
    private float m_shotDelay = 0.0f;

	// Treasure
	private GameObject m_tmpTreasureRef = null;
	private bool m_haveTreasure = false;

	
	// Available Type
	public enum CharacterType
	{
		A = 0,
		B,
		C,
		D
	};
	public CharacterType m_type = CharacterType.A;

    // Looting Feature
    public LayerMask m_whatIsLootable;


	private Animator m_anim;

//	Character()
//	{
//		m_type = CharacterType.A;
//	}
//
//	Character(CharacterType type)
//	{
//		m_type = type;
//	}
	
	// Use this for initialization
	void Start()
	{
		m_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update()
	{
        m_shotDelay += Time.deltaTime;
		m_interactedDelay += Time.deltaTime;

		// Shoot the bullet
		if (Input.GetKey (KeyCode.X) && m_shotDelay > m_shotRate)
		{
			ShootBullet ();
            m_shotDelay = 0.0f;
		}

        PlayerPickUp();
		JumpMovement();

		if (Input.GetKey (KeyCode.E)) {
			TrapInteraction();
		}

		if (m_interactedDelay > 2.0f) {
			m_interacted = false;
			m_interactedDelay = 0.0f;
		}
	}
	
	void FixedUpdate()
	{
		if(photonView.isMine)
			HorizontalMovement();
	}

	public void ShootBullet()
	{
		List<GameObject> tmpPool = TrapManagerScript.current.m_trapPool;
		if(tmpPool[tmpPool.Count - 1].GetComponent<BoxCollider2D>().enabled)
			return;

		GameObject bulletReference = PoolManager.current.GetBullet ();

		bulletReference.transform.position = transform.position;

        bulletReference.GetComponent<BulletScript>().tmpPlayerRef = this;

		// Set the bullet flying direction
		bulletReference.GetComponent<BulletScript> ().m_facingRight = m_facingRight;

        bulletReference.SetActive(true);
	}

	public void TrapInteraction()
	{
		m_interacted = true;
	}

	public void PlayerPickUp()
	{
        Debug.Log("PlayerPickUp Function Call");
        // Check if loot key has been pressed and player is grounded in order to loot
		if (Input.GetKeyDown (KeyCode.Z) && m_grounded)
		{
            Debug.Log ("Z - Pressed, Check for looting");
            // Check if player collided with nearby lootable item
            Collider2D lootObtained = Physics2D.OverlapCircle(transform.position, 0.5f, m_whatIsLootable);

            if(lootObtained)
            {
				if(lootObtained.gameObject.activeSelf)
				{
	                Debug.Log ("Loot Obtained");
	                lootObtained.gameObject.SetActive(false);
					lootObtained.transform.parent = transform;
					m_tmpTreasureRef = lootObtained.gameObject;
					m_haveTreasure = true;
				}
            }
		}
	}

	public void DropTreasure()
	{
		if(m_tmpTreasureRef == null)
			return;

		m_tmpTreasureRef.SetActive (true);
		m_tmpTreasureRef.transform.parent = null;
		m_haveTreasure = false;
	}

	void HorizontalMovement()
	{
		m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_whatIsGround);
		m_anim.SetBool ("Ground", m_grounded);
		
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * m_maxSpeed, rigidbody2D.velocity.y);

		// Animation Run
		m_anim.SetFloat("Speed", Mathf.Abs (move));
		

		// If -> Key is press move is > 0.0f and if player is facing left Flip() it
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
		// Check if grounded and Space is press to Jump
		if (m_grounded && Input.GetKeyDown (KeyCode.Space)) {
			rigidbody2D.AddForce (new Vector2 (0.0f, m_jumpForce));
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet" && other.GetComponent<BulletScript>().tmpPlayerRef != this)
        {
            ComponentHealth tmpHealthCompo = gameObject.GetComponent<ComponentHealth>();
            
            tmpHealthCompo.HPModifier(-1);
        }
        
		if (other.name == "TreasureCheckPoint" && m_haveTreasure) {
			Debug.Log("Victory");
		}
    }
}