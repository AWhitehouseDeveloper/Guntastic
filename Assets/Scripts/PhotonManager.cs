using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        var player = PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-10f, 10f), Random.Range(-3.5f, 4f)), Quaternion.identity);
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}
