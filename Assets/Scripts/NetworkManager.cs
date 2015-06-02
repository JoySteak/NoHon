using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	public static NetworkManager current;
	
	private string roomName = "Test Photon";
	private bool isVisible = true;
	private bool isOpen = true;
	private int maxPlayers = 2;
	public bool isRoomMaster = false;
	public GameObject player;
	public List<Vector3> list;
	
	void Awake()
	{
		current = this;
	}
	
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.sendRate = 30;
		PhotonNetwork.sendRateOnSerialize = 20;
		//list = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUILayout.Label("Status - " + PhotonNetwork.connectionStateDetailed.ToString() + System.Environment.NewLine + " Ping - " + PhotonNetwork.GetPing());
	}
	
	// Joined Lobby find or randomly join a room
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom (roomName, isVisible, isOpen, maxPlayers);
		isRoomMaster = true;
	}
	
	void OnJoinedRoom() {
		
		PhotonNetwork.Instantiate(player.name,new Vector3(9.4f,13.77f,0.0f),Quaternion.identity,0);
		if(isRoomMaster){
			PhotonNetwork.Instantiate("TrapManager",Vector3.zero,Quaternion.identity,0);
			for(int i =0; i<list.Count;i++){
				GameObject tmpTrap = PhotonNetwork.Instantiate("TrapSwitchRight",list[i],Quaternion.identity,0);
			}
		}
	}
	

	
	//	void SpawnZombie() {
	//		for (int i = 0; i < 5; i++) {
	//			float randomX = Random.Range(-11.0f, 11.0f);
	//			float randomY = Random.Range(-5.0f, 5.0f);
	//			GameObject zombieObject = PhotonNetwork.Instantiate ("Zombie", new Vector3(randomX, randomY, 0.0f), Quaternion.identity, 0);
	//		}
	//	}
	//
	//	void SpawnHuman() {
	//		for (int i = 0; i < 5; i++) {
	//			float randomX = Random.Range(-11.0f, 11.0f);
	//			float randomY = Random.Range(-5.0f, 5.0f);
	//			GameObject zombieObject = PhotonNetwork.Instantiate ("Human", new Vector3(randomX, randomY, 0.0f), Quaternion.identity, 0);
	//		}
	//	}
}
