using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    Text ammoDisplay;
    Text ammoWarning;

    GameObject fireSoundGameObject;
    GameObject reloadSoundGameObject;

    AudioSource fireSound;
    AudioSource reloadSound;

    public float fireRate = 0.1f;
    float fireTimer;

    float clipBullets = 12;
    float magazineBullets = 42;

    // Start is called before the first frame update
    void Start()
    {
        //find ui objects
        ammoDisplay = GameObject.Find("Ammo").GetComponent<Text>();
        ammoWarning = GameObject.Find("AmmoWarning").GetComponent<Text>();

        //showcase ammo
        ammoDisplay.text = "" + clipBullets + " /" + magazineBullets;

        //find sound gameobjects
        fireSoundGameObject = GameObject.Find("FireSound");
        reloadSoundGameObject = GameObject.Find("ReloadSound");

        //initialize sounds from audio Source
        fireSound = fireSoundGameObject.GetComponent<AudioSource>();
        reloadSound = reloadSoundGameObject.GetComponent<AudioSource>();

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        displayLowAmmoWarning();

        if (fireTimer < fireRate) {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer>fireRate) {

            if (clipBullets > 0) {

                //play fire sound
                fireSound.Play();

                //update clip number
                clipBullets -= 1;


                //reset fireTimer
                fireTimer = 0.0f;

                RaycastHit _hit;
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                //if ray collides with gameobject, load hit data to _hit
                if (Physics.Raycast(ray, out _hit, 100))
                {
                    Debug.Log(_hit.collider.gameObject.name);

                    if (_hit.collider.gameObject.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                    {
                        _hit.collider.gameObject.GetComponent<PhotonView>().RPC("takeDamage", RpcTarget.AllBuffered, 10f);
                    }
                }
            }
        }

        reloadWeapon();

        //display remaining ammo
        ammoDisplay.text = "" + clipBullets + " /" + magazineBullets;
    }

    void displayLowAmmoWarning() {
        if (clipBullets == 0)
        {
            ammoWarning.text = "Clip empty! Reload!";
        }
    }

    void reloadWeapon() {
        if (Input.GetKeyDown(KeyCode.R) && clipBullets < 12)
        {

            if (magazineBullets > 0)
            {
                //play reload sound
                reloadSound.Play();

                if (magazineBullets - (12 - clipBullets) > 0)
                {
                    clipBullets += (12 - clipBullets);
                    magazineBullets -= (12 - clipBullets);
                }
                else if (magazineBullets - (12 - clipBullets) < 0)
                {
                    clipBullets += magazineBullets;
                    magazineBullets = 0;
                }

                //clear ammo warning
                ammoWarning.text = "";
            }
        }
    }
}
