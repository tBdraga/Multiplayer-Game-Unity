using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class FPSGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;


    public static FPSGameManager gameManagerInstance;

    private void Awake()
    {
        if (gameManagerInstance!=null) {
            Destroy(this.gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null) {
                int randomSpawnPoint = Random.Range(-5, 5);
                //instantiate player for all members in room
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomSpawnPoint, 0, randomSpawnPoint), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player " + newPlayer.NickName + " joined the game " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

    public void leaveRoom() {
        PhotonNetwork.LeaveRoom();
    }
}
