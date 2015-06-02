using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	// Use this for initialization
	public Sprite m_switchCenter;

	//types of trap
	public float normRollChance = 0.25f;
	public float bonusRollChance = 0.5f;
	public int hpBonus = 2;
	public int hpMinus = -2;
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
		if(type < 0.25f){
			currentTrap = trapType.A;
			this.GetComponent<SpriteRenderer>().color = Color.red;
			Debug.Log("a");
		}else if(type < 0.5f){
			currentTrap = trapType.B;
			this.GetComponent<SpriteRenderer>().color = Color.blue;
			Debug.Log("b");
		}else if(type < 0.75f){
			currentTrap = trapType.C;
			this.GetComponent<SpriteRenderer>().color = Color.black;
			Debug.Log("c");
		}else{
			currentTrap = trapType.D;
			this.GetComponent<SpriteRenderer>().color = Color.white;
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

	void OnCollisionStay2D(Collision2D other){
		//check if collided object is hero
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player");
			//check if hero has interacted, i.e bool hasInteracted is true
			if(other.gameObject.GetComponent<Character>().m_interacted){

				//check type of hero
				if((int)other.gameObject.GetComponent<Character>().m_type == (int)currentTrap){
					//match, 50% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(true));
					gameObject.GetComponent<SpriteRenderer>().sprite = m_switchCenter;
					gameObject.GetComponent<BoxCollider2D>().enabled = false;
					//destroy trap
				}else{
					//no match, 25% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(false));
					gameObject.GetComponent<SpriteRenderer>().sprite = m_switchCenter;
					gameObject.GetComponent<BoxCollider2D>().enabled = false;
					//destroy trap
				}
			}
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("aaa");
		if(stream.isWriting){
			stream.SendNext((int)currentTrap);
			SetTrapType(currentTrap);
		}else{
			currentTrap = (trapType)stream.ReceiveNext();
			SetTrapType(currentTrap);
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
			hero.GetComponent<ComponentHealth>().HPModifier(hpBonus);
			Debug.Log("SUCCESS");
		}else{
			//trap open fail
			//all hero hp 10
			hero.GetComponent<ComponentHealth>().HPModifier(hpMinus);
			Debug.Log("faaaaail");
		}
	}

	void SetTrapType(trapType type){
		if(type == trapType.A){
			currentTrap = trapType.A;
			this.GetComponent<SpriteRenderer>().color = Color.red;
			Debug.Log("a");
		}else if(type == trapType.B){
			currentTrap = trapType.B;
			this.GetComponent<SpriteRenderer>().color = Color.blue;
			Debug.Log("b");
		}else if(type == trapType.C){
			currentTrap = trapType.C;
			this.GetComponent<SpriteRenderer>().color = Color.black;
			Debug.Log("c");
		}else{
			currentTrap = trapType.D;
			this.GetComponent<SpriteRenderer>().color = Color.white;
			Debug.Log("d");
		}
	}
}
