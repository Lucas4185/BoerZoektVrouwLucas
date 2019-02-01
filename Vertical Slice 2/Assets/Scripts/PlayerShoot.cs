﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public float speed = 3f;

    public int MaxAmmo = 90;
    private int ammo = 30;
    public int currentAmmo = 30;

    public GameObject bulletPrefab;
    public float fireDelay = 2f;

    float nextFire = 0;
    Vector3 direction;

    private void Awake()
    {
        var ammoCollisionSystem = GetComponent<CollisionListener>();
        ammoCollisionSystem.ammoCollected += GetAmmo;
    }

    private void Update()
    {
        direction = Input.mousePosition;
        direction.z = 0;
        direction = Camera.main.ScreenToWorldPoint(direction);
        direction = direction - transform.position;
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentAmmo > 0) {

            currentAmmo--;

            nextFire = Time.time + fireDelay;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
        }

        Reload();
    }

    private void GetAmmo()
    {
        MaxAmmo += 15;
    }

    private void Reload()
    {
        if (Input.GetKeyDown("r")) {
            if (currentAmmo < ammo && MaxAmmo > 0) {

                if (MaxAmmo - (ammo - currentAmmo) < 0) {
                    int missingBullets = ((ammo - currentAmmo) - MaxAmmo);
                    currentAmmo = ammo;
                    currentAmmo -= missingBullets;
                    MaxAmmo = 0;
                } else {
                    MaxAmmo -= (ammo - currentAmmo);
                    currentAmmo = ammo;
                }
            }
        }
    }
}