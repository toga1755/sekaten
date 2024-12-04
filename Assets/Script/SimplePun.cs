using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using AnimalControl = Supercyan.AnimalPeopleSample.SimpleSampleCharacterControl;
using FreeControl = Supercyan.FreeSample.SimpleSampleCharacterControl;
using Photon.Pun.Demo.PunBasics;

public class SimplePun : MonoBehaviourPunCallbacks
{

	// Use this for initialization
	void Start () {
		//旧バージョンでは引数必須でしたが、PUN2では不要です。
		PhotonNetwork.ConnectUsingSettings();
	}

	void OnGUI()
	{
		//ログインの状態を画面上に出力
		GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
	}


	//ルームに入室前に呼び出される
	public override void OnConnectedToMaster() {
		// "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
		PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
	}

	//ルームに入室後に呼び出される
	public override void OnJoinedRoom(){
		// プレイヤーキャラクターのプレハブを指定
		// リストの中からランダムに１つ選ぶ
		// string[] player_characters = new string[] {"FreeSample_male_1_SimpleMovement", "animal_people_wolf_1_SimpleMovement"};
		// string player_character = player_characters[Random.Range(0, player_characters.Length)];
		
		//キャラクターを生成
		GameObject monster = PhotonNetwork.Instantiate("FreeSample_male_1_SimpleMovement", new Vector3(38, 8, 38), Quaternion.identity, 0);
		
		if (monster == null) {
			Debug.LogError("Failed to instantiate player character.");
			return;
		}

		//自分だけが操作できるようにスクリプトを有効にする
		// var animalControlComponent = monster.GetComponent<AnimalControl>();
		var freeControlComponent = monster.GetComponent<FreeControl>();
		var cameraWorksComponent = monster.GetComponent<CameraWorks>();

		freeControlComponent.enabled = true;
		cameraWorksComponent.enabled = true;
	}
}

