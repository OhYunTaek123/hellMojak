using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {
        byte n = 10;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom("Ã¹¹øÂ°", roomOptions);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreatedRoomFailed," + returnCode + " , " + message);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SceneManager.LoadScene("manCha");
    }
    void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void Start()
    {
        CreateRoom();
    }
}
