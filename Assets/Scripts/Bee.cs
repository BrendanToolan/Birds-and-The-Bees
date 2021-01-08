/*
References

https://forum.unity.com/threads/creating-a-random-direction-vector.220427/
https://stuartspixelgames.com/2019/02/19/how-to-change-sprites-colour-or-transparency-unity-c/

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{

    Rigidbody2D beeRB;
    BoxCollider2D beeCollider;
    SpriteRenderer beeSprite;

    public float beeSpeed = 2f;
    private float range = 1f;
    private float energy = 200f;
    private float energyRate = 0.3f;
    public float nectar = 0f;
    private Vector2 movementDirection;

    public GameObject flower;


    // Start is called before the first frame update
    void Start()
    {
        beeRB = GetComponent<Rigidbody2D>();
        beeRB.velocity = new Vector2(0,0);
        movementDirection = Random.insideUnitCircle.normalized;
        beeSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag =="XBorder")
        {
            movementDirection.x *= -1f;
        }

        if(collision.tag =="Flower")
        {
            nectar++;
            print("Nectar Collected: " + nectar);
        }

        if(collision.tag =="YBorder")
        {
            movementDirection.y *= -1f;
        }

        if(collision.tag =="Bird")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
      Searching();
      Energy();  
    }

    private void Energy()
    {
        if(energy <= 120)
        {
            beeSprite.color = new Color(255, 255, 0, 1);
        }
        else if(energy <= 60)
        {
            beeSprite.color = new Color(1, 0, 0, 1);
        }
       

    }


    private void Searching()
    {

        energyRate -= 0.1f;
        beeRB.velocity = movementDirection * beeSpeed;

        if(Vector2.Distance(transform.position, flower.transform.position) <= range)
        {
            transform.position = Vector2.MoveTowards(transform.position, flower.transform.position, beeSpeed*Time.deltaTime);
            print("bee within range");
        }

    }

    private void GoBackToHive()
    {

    }


}
