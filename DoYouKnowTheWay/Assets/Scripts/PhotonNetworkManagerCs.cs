﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
///  Network manager

public class PhotonNetworkManagerCs : Photon.MonoBehaviour
{
    ///Used to store refrence to player prefab 
    [SerializeField]
    private GameObject[] Player;

    ///Used to store refrence to spawnPoints postion as a prefab 
    [SerializeField]
    private GameObject[] spawnPoints;

    ///Used to store refrence to debug connection info 
    [SerializeField]
    private Text networkUpdateText;

    [SerializeField]
    private Text RoomNameText;
    private PhotonView myphotonView;


    [SerializeField]
    private GameObject connectingCanvas;

    /// used to store current version 
    const string version = "0.1";

    ///options for multiplayer room 
    RoomOptions myroom;
    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.ConnectUsingSettings(version);
       myphotonView = GetComponent<PhotonView>();
        myroom = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        //all players will load the same scene
        //PhotonNetwork.automaticallySyncScene = true;
    }

    public bool CheckMyRoom()
    {
        if (PhotonNetwork.countOfPlayers == myroom.MaxPlayers)
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
           // Debug.Log("false");
            return false;
        }
    }

    // creates lobby 
    public void CreateMyLobby ()
    {
        PhotonNetwork.CreateRoom(RoomNameText.text.ToString(), myroom, null);
    }
    // joins a lobby 
    public void JoinMylobby ()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRoom(RoomNameText.text.ToString());
        }
        else
        {

        }
       
    }
 
    public virtual void OnJoinedLobby()
    {
        //Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }
      public virtual void OnConnectedToMaster()
      {
        //Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
      }
    public void OnJoinedRoom()
    {
        //PhotonNetwork.Instantiate(Player[spawnCount].name, spawnPoints[spawnCount].transform.position, spawnPoints[spawnCount].transform.rotation, 0);
        PhotonNetwork.Instantiate(Player[PhotonNetwork.player.ID - 1].name, spawnPoints[PhotonNetwork.player.ID - 1].transform.position, Player[PhotonNetwork.player.ID - 1].transform.rotation, 0);
        //Debug.Log("JOINED ROOM");
       // spawnCount++;
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }
    public void DisconnectGame()
    {
        PhotonNetwork.Disconnect();
    }
    // Update is called once per frame
    void Update ()
    {
        if (!PhotonNetwork.connected)
        {
           // Debug.Log("connecting");
        }
        //if connected photon button = useful
       /* else if (PhotonNetwork.connected)
        {
            if (PhotonNetwork.playerList.Length == myroom.maxPlayers)
            {
                connectingCanvas.SetActive(false);
                PhotonNetwork.room.IsOpen = false;
                Debug.Log("connected");
            }

        }*/
        networkUpdateText.text ="Stat:" + PhotonNetwork.connectionStateDetailed.ToString () +
            " Ping: " + PhotonNetwork.GetPing()+"ms"; 
	}

}
