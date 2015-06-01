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

	// Use this for initialization
	void Start(){
		poolManager = GameObject.Find("PoolManager");
		m_position = this.transform.position;
		m_rotation = this.transform.rotation;

	}
	
	// Update is called once per frame
	void Update(){
		if(bulletTimer <= 0){
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

		//if hp = 0, dies, spawns treasure at death coords
	}

	void OnCollisionEnter2D(Collision2D other){
		//if hit by bullet, get damaged
		if(other.gameObject.tag == "Bullet"){
			//minus health of boss
		}

	}

	void dropLoot(){
		//instantiate treasure at death coords
		Instantiate(treasure, m_position,m_rotation);
	}
}
