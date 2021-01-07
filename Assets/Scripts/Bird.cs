using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D birdRB;
    BoxCollider2D birdCollider;

    public float speed = 4f;
    private float range = 6f;
    private float energy = 200f;
    private float energyRate = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        birdRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
