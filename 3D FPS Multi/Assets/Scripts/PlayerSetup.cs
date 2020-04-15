using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    GameObject cameraBase;

    [SerializeField]
    TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        cameraBase = GameObject.Find("CameraBase");

        if (photonView.IsMine) {
            transform.GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerController>().enabled = false;
        }

        setPlayerUI();
    }

    void setPlayerUI() {

        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }

    }
}
