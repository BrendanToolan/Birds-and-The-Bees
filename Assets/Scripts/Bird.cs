﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D birdRB;
    BoxCollider2D birdCollider;

    public float speed = 3f;
    private float range = 6f;
    private float energy = 200f;
    private float energyRate = 0.3f;
    private Vector2 movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        birdRB = GetComponent<Rigidbody2D>();
        birdRB.velocity = new Vector2(0,0);
        movementDirection = Random.insideUnitCircle.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        birdRB.velocity = movementDirection*speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            movementDirection *= -1f;
        }
    }
}
