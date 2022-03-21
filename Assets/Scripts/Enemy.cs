using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 2; //if distance less than 1 then chase
    public float chaseLength = 5; //if chased for more than 5 then return to original position
    private bool chasing;
    private bool collidingWithPlayer; //if colliding with player stop dont go any further
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    //since we cant inherit form collidable because we already inherit from mover
    //C# does not contain multi inheritance
    public ContactFilter2D filter;
    private BoxCollider2D hitbox; 
    private Collider2D[] hits = new Collider2D[10];


    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>(); //get chiled of index 0
    }

    private void FixedUpdate()
    {
        //is the player in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if(!collidingWithPlayer)
                    UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        //Collision work
        boxCollider.OverlapCollider(filter, hits); //overlap collider would need the filter and array to store hits
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue; //no hit
            }
            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //Array not cleaned up every time (need to manually clean)
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 20, Color.green, transform.position, Vector3.up * 40, 1.0f);
    }
}
