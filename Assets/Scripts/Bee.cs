/*
References

https://forum.unity.com/threads/creating-a-random-direction-vector.220427/
https://stuartspixelgames.com/2019/02/19/how-to-change-sprites-colour-or-transparency-unity-c/
https://stackoverflow.com/questions/59022682/using-unity-scripting-enemy-ai-follow-player


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeeState
{
    Searching,
    atHive,
    Fleeing,
    Dancing,
    Gathering
}

public class Bee : MonoBehaviour
{

    public BeeState currState = BeeState.Searching;

    Rigidbody2D beeRB;
    BoxCollider2D beeCollider;
    SpriteRenderer beeSprite;

    public float beeSpeed = 2f;
    private float range = 4f;
    private float energy = 200f;
    private float energyRate = 0.3f;
    public float nectar = 0f;
    private Vector2 movementDirection;

    private float d1;
    private float d2 = 5f;
    private int id;

    public GameObject[] flower;
   // flower = GameObject.FindGameObjectsWithTag("Flower");
    GameObject bird;
    GameObject hive;
    Transform target;
    Transform target1;

    // Start is called before the first frame update
    void Start()
    {
        beeRB = GetComponent<Rigidbody2D>();
        beeRB.velocity = new Vector2(0,0);
        //flower = GameObject.FindGameObjectsWithTag("Flower");
        bird = GameObject.FindGameObjectWithTag("Bird");
        hive = GameObject.FindGameObjectWithTag("Hive");
        target = GameObject.FindGameObjectsWithTag("Flower")[0].GetComponent<Transform>();
        target1 = GameObject.FindGameObjectWithTag("Hive").GetComponent<Transform>();
        movementDirection = Random.insideUnitCircle.normalized;

       // beeSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
        {
            case (BeeState.Searching):
                Searching();
                break;
            case (BeeState.Gathering):
                Gathering();
                break;
            case (BeeState.Fleeing):
                Fleeing();
                break;
        }

        if(IsFlowerInRange(range))
        {
            currState = BeeState.Gathering;
        }
        else if(!IsFlowerInRange(range))
        {
            currState = BeeState.Searching;
        }

        if(IsBirdInRange(range))
        {
            currState = BeeState.Fleeing;
        }
        else if(IsBirdInRange(range))
        {
            currState = BeeState.Searching;
        }
    }

    private bool IsFlowerInRange(float range)
    {
        int i = 0;

        foreach(GameObject flower in flower)
        {
            if(flower != null)
            {
                this.d1 = Vector2.Distance(transform.position, flower.transform.position);
                if(this.d1 < this.d2)
                {
                    if(Vector2.Distance(transform.position, flower.transform.position) <= range){
                        this.d2 = this.d1;
                        id = i;
                        return true;
                    }
                }
            }
        }

        this.d1 =0;
        this.d2 =range;
        return false;
    }

    private bool IsBirdInRange(float range)
    {
        return Vector2.Distance(transform.position, bird.transform.position) <= range;
    }


    void Searching()
    {
        energyRate -= 0.1f;
        beeRB.velocity = movementDirection*beeSpeed;
    }

    void Gathering()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), beeSpeed * Time.deltaTime); 
    }

    void Fleeing()
    {
        energyRate -= 0.3f;
        print("Fleeing");
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target1.position.x, transform.position.y), beeSpeed * Time.deltaTime);
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
            currState = BeeState.Searching;
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

    


}
