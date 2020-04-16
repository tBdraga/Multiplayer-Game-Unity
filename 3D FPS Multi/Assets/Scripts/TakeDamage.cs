using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Image healthBar;

    private float playerHealth;
    public float startHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = startHealth;

        healthBar.fillAmount = playerHealth/startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void takeDamage(float _damage) {
        playerHealth -= _damage;
        Debug.Log(playerHealth);

        healthBar.fillAmount = playerHealth / startHealth;

        if (playerHealth<=0f) {
            //die();
            StartCoroutine(respawn());
        }
    }

    void die() {
        if (photonView.IsMine) {
            FPSGameManager.gameManagerInstance.leaveRoom();
        }
    }

    IEnumerator respawn() {

        GameObject respawnText = GameObject.Find("RespawnText");

        float respawnTime = 8.0f;

        while (respawnTime > 0.0f) {

            yield return new WaitForSeconds(1.0f);
            respawnTime -= 1.0f;

            //while respawning movement not allowed
            transform.GetComponent<PlayerController>().enabled = false;
            //gameObject.SetActive(false);

            if (photonView.IsMine) {
                respawnText.GetComponent<Text>().text = "Respawning ...";
            }
            else
            {
                respawnText.GetComponent<Text>().text = "Opponent killed!";
            }

        }

        respawnText.GetComponent<Text>().text = "";

        if (photonView.IsMine) {
            int randomSpawnPoint = Random.Range(-5, 5);
            transform.position = new Vector3(randomSpawnPoint, 0, randomSpawnPoint);
            //gameObject.SetActive(true);
            transform.GetComponent<PlayerController>().enabled = true;

            photonView.RPC("regainHealth", RpcTarget.AllBuffered);
        }
        
    }

    [PunRPC]
    public void regainHealth() {
        playerHealth = startHealth;

        healthBar.fillAmount = playerHealth / startHealth;
    }
}
