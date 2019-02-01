﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{

    public event Action ammoCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "ammo")
            {
             Destroy(collision.gameObject);
            if (ammoCollected != null)
              ammoCollected();
            }            
    }
}
