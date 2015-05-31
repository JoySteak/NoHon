using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	// Use this for initialization

	//types of trap
	public float normRollChance = 0.25f;
	public float bonusRollChance = 0.5f;
	public enum trapType{
		A = 0,
		B,
		C,
		D
	};

	public trapType currentTrap = trapType.A;

	void Start(){
		//assign a type to this trap
		//trapType currentTrap;
		float type = Random.value;
		if(type < 0.25){
			currentTrap = trapType.A;
			Debug.Log("a");
		}else if(type < 0.5){
			currentTrap = trapType.B;
			Debug.Log("b");
		}else if(type < 0.75){
			currentTrap = trapType.C;
			Debug.Log("c");
		}else{
			currentTrap = trapType.D;
			Debug.Log("d");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if hero interacts with trap
		//check type of hero against type of trap
		//if match, 50% success rate
		//else, 25% success rate
		//if success, current hero +10hp (max 100hp), trap destroyed
		//else all heroes -10hp
	
	}

	void OnCollisionEnter2D(Collision2D other){
		//check if collided object is hero
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player");
			//check if hero has interacted, i.e bool hasInteracted is true
			if(other.gameObject.GetComponent<Character>().m_interacted){
				//check type of hero
				if((int)other.gameObject.GetComponent<Character>().m_type == (int)currentTrap){
					//match, 50% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(true));
					Destroy(this.gameObject);
					//destroy trap
				}else{
					//no match, 25% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(false));
					Destroy(this.gameObject);
					//destroy trap
				}
			}
		}
	}

	bool AttemptOpenTrap(bool typeCheck){
		float rollAttempt = Random.value;
		if(typeCheck){
			//if match, 50% success
			if(rollAttempt > 1-bonusRollChance){
				return true;
			}else{
				return false;
			}
		}else{
			//no match, 25% success
			if(rollAttempt > 1- normRollChance){
				return true;
			}else{
				return false;
			}
		}
	}

	void DamageOrReward(GameObject hero, bool successCheck){
		if(successCheck){
			//if trap open success
			//current hero hp +10
			Debug.Log("SUCCESS");
		}else{
			//trap open fail
			//all hero hp 10
			Debug.Log("faaaaail");
		}
	}
}
