using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; //contact filter what it should hit
    private BoxCollider2D boxCollider; //the casted box to check collision
    private Collider2D[] hits = new Collider2D[10]; //objects hit max 10
    
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void Update()
    {
        //Collision work
        boxCollider.OverlapCollider(filter, hits); //overlap collider would need the filter and array to store hits
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue; //no hit
            }
            OnCollide(hits[i]);

            //Array not cleaned up every time (need to manually clean)
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("On collide was not implemented in " + this.name);
    }
}
