using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject enterGamePanel;
    public GameObject connectionStatusPanel;
    public GameObject lobbyPanel;

    // Start is called before the first frame update
    void Start()
    {
        enterGamePanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        //all player are notifies to load the same scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void connectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            //connect to photon services
            PhotonNetwork.ConnectUsingSettings();

            //display connecting loading screen
            connectionStatusPanel.SetActive(true);
            enterGamePanel.SetActive(false);
        }
    }

    public void joinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    public void createNewRoom()
    {
        string roomName = "Room " + Random.Range(0, 100);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + "Connected to server");
        //activate lobby panel
        lobbyPanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Join random room failed:" + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name);
        //load level
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name + ". Max Player count: " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
}
