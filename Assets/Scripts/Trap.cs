using UnityEngine;
using System.Collections;

public class Trap : Photon.MonoBehaviour {

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
		if (photonView.isMine) {
			float type = Random.value;
			if (type < 0.25f) {
				currentTrap = trapType.A;
				this.GetComponent<SpriteRenderer> ().color = Color.red;
				Debug.Log ("a");
			} else if (type < 0.5f) {
				currentTrap = trapType.B;
				this.GetComponent<SpriteRenderer> ().color = Color.blue;
				Debug.Log ("b");
			} else if (type < 0.75f) {
				currentTrap = trapType.C;
				this.GetComponent<SpriteRenderer> ().color = Color.black;
				Debug.Log ("c");
			} else {
				currentTrap = trapType.D;
				this.GetComponent<SpriteRenderer> ().color = Color.white;
				Debug.Log ("d");
			}

			photonView.RPC("SetTrapType", PhotonTargets.AllBuffered, new object[]{(int)currentTrap});
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
					//destroy trap
					photonView.RPC("RemoteDisableCollider", PhotonTargets.AllBufferedViaServer, null);
					//match, 50% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(true));
				}else{
					//destroy trap
					photonView.RPC("RemoteDisableCollider", PhotonTargets.AllBufferedViaServer, null);
					//no match, 25% success rate
					DamageOrReward(other.gameObject,AttemptOpenTrap(false));
				}
			}
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
//		Debug.Log("aaa");
//		if(stream.isWriting){
//			stream.SendNext((int)currentTrap);
//			SetTrapType(currentTrap);
//		}else{
//			currentTrap = (trapType)stream.ReceiveNext();
//			SetTrapType(currentTrap);
//		}
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
			hero.GetComponent<Character>().photonView.RPC("RemoteDeductHP", PhotonTargets.AllViaServer, new object[]{hpBonus});
//			hero.GetComponent<ComponentHealth>().HPModifier(hpBonus);
			Debug.Log("SUCCESS");
		}else{
			//trap open fail
			//all hero hp 10
			hero.GetComponent<Character>().photonView.RPC("RemoteDeductHP", PhotonTargets.AllViaServer, new object[]{hpMinus});
//			hero.GetComponent<ComponentHealth>().HPModifier(hpMinus);
			Debug.Log("faaaaail");
		}
	}

	[RPC]
	void SetTrapType(int type){
		switch ((trapType) type) {
		case trapType.A:
			currentTrap = trapType.A;
			this.GetComponent<SpriteRenderer> ().color = Color.red;
			break;
		case trapType.B:
			currentTrap = trapType.B;
			this.GetComponent<SpriteRenderer> ().color = Color.blue;
			break;
		case trapType.C:
			currentTrap = trapType.C;
			this.GetComponent<SpriteRenderer> ().color = Color.black;
			break;
		case trapType.D:
			currentTrap = trapType.D;
			this.GetComponent<SpriteRenderer> ().color = Color.white;
			break;
		}

	}

	[RPC]
	void RemoteDisableCollider()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = m_switchCenter;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
}
