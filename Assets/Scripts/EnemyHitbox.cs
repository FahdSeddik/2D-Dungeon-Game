using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 4;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            //Create a new dmg object before sending it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position, //our current player position
                pushForce = pushForce
            };

            //send this to enemy
            coll.SendMessage("ReceiveDamage", dmg); //this will be sent to enemy and call ReceiveDamage in enemy and pass dmg
        }
    }
}
