using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState
{
    Flying,
    Chasing,
}


public class Bird : MonoBehaviour
{
    GameObject bee;
    public BirdState currState = BirdState.Flying;

    Transform target;
    Rigidbody2D birdRb;
    Collider2D birdCollider;

    public float birdSpeed = 2f;
    public float range =  3f;
    private  Vector2 movementDirection;

    void Start()
    {
        birdRb = GetComponent<Rigidbody2D>();
        birdRb.velocity = new Vector2(0,0);
        bee =  GameObject.FindGameObjectWithTag("Bee");
        target = GameObject.FindGameObjectWithTag("Bee") .GetComponent<Transform>();
        movementDirection = Random.insideUnitCircle.normalized;

    }

    void Update()
    {
        switch (currState)
        {
            case (BirdState.Flying):
                Flying();
                break;
            case (BirdState.Chasing):
                Chasing();
                break;
            
        }

        if(isBeeInRange(range))
        {
            currState = BirdState.Chasing;
        }
        else if(!isBeeInRange(range))
        {
            currState = BirdState.Flying;
        }
        
    }

    private bool isBeeInRange(float range)
    {
        return Vector2.Distance(transform.position, bee.transform.position) <= range;

    }

    void Flying()
    {
        birdRb.velocity = movementDirection*birdSpeed;
    }

    void Chasing()
    {
        print("Chasing State");

        if(transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            birdRb.velocity = new Vector2(-birdSpeed, 0f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), birdSpeed*Time.deltaTime);
        }
        else if(transform.position.x < target.position.x)
        {
            transform.localScale = new Vector2(1, 1);
            birdRb.velocity = new Vector2(birdSpeed, 0f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), birdSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "XBorder")
        {
            movementDirection.x *= -1f;
        }

        if(collision.tag == "YBorder")
        {
            movementDirection.y *= -1f;
        }
        
        if(collision.tag == "Bee")
        {
            currState = BirdState.Flying;
        }
    }
}
