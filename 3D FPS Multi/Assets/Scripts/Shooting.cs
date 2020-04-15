using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    public float fireRate = 0.1f;
    float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
       camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer < fireRate) {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer>fireRate) {

            //reset fireTimer
            fireTimer = 0.0f;

            RaycastHit _hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            
            //if ray collides with gameobject, load hit data to _hit
            if (Physics.Raycast(ray, out _hit, 100))
            {
                Debug.Log(_hit.collider.gameObject.name);
            }
        }
    }
}
