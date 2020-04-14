using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject cameraBase;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
