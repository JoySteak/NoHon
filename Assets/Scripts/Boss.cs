using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public float bulletInterval = 0f;
	float bulletTimer = 0f;
	GameObject poolManager;
	GameObject bullet;
	public float amplitude;
	public float period;
	public GameObject treasure;
	Vector2 m_position;
	Quaternion m_rotation;
	public int receiveDamage = -1;
	bool startShooting = false;


	// Use this for initialization
	void Start(){
		poolManager = GameObject.Find("PoolManager");
		m_position = this.transform.position;
		m_rotation = this.transform.rotation;

	}
	
	// Update is called once per frame
	void Update(){
		if(GameObject.Find("Trap")==null){
			startShooting = true;
		}
		if(bulletTimer <= 0 && startShooting){
			bulletTimer = bulletInterval;
			//shoot bullet
			bullet = poolManager.GetComponent<PoolManager>().GetBullet();
			float theta = Time.timeSinceLevelLoad / period;
			float distance = amplitude * Mathf.Sin(theta);
			bullet.transform.position = new Vector2(this.transform.position.x -1.5f,this.transform.position.y*distance-4.5f);
			bullet.GetComponent<BulletScript>().m_facingRight = false;
			bullet.SetActive(true);
		}else{
			bulletTimer -= Time.deltaTime; 
		}

		//shoots a bullet from in front in fixed intervals
		//the y value of the bullet spawn coords go up and down
		//var health = GetComponent<ComponentHealth>().CurrHP;
		/*if(health <= 1){
			dropLoot();
		}*/

		//if hp = 0, dies, spawns treasure at death coords
	}

	void OnTriggerEnter2D(Collider2D other){
		//if hit by bullet, get damaged

		if(other.tag == "Bullet"){
			ComponentHealth tmpHealthCompo = gameObject.GetComponent<ComponentHealth>();
			
			tmpHealthCompo.HPModifier(receiveDamage);
		}

	}

	public void DropTreasure()
	{
		treasure.SetActive (true);
		treasure.transform.parent = null;
	}
}
