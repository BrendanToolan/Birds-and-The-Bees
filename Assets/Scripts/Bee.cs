/*
References

https://forum.unity.com/threads/creating-a-random-direction-vector.220427/


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{

    Rigidbody2D beeRB;
    BoxCollider2D beeCollider;

    public float beeSpeed = 3f;
    private float range = 6f;
    private float energy = 200f;
    private float energyRate = 0.3f;
    public float nectar = 0f;
    private Vector2 movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        beeRB = GetComponent<Rigidbody2D>();
        beeRB.velocity = new Vector2(0,0);
        movementDirection = Random.insideUnitCircle.normalized;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Bird")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
      beeRB.velocity = movementDirection * beeSpeed;   
    }

    void Searching()
    {
        energyRate -= 0.1f;
    }


}
