using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFS
{
    public class Shoot : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform spawnPoint;
        public float shotCooldown = 0.4f;
        public float shotSpeed = 16f;
        private float lastShot;

        private void Start()
        {
            lastShot = Time.time - shotCooldown;
        }
        
        private void Update()
        {
            if (Input.GetButton("Fire1") && lastShot + shotCooldown <= Time.time)
            {
                lastShot = Time.time;
                GameObject go = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
                go.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * shotSpeed;

            }
        }
    }
}