using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.1f, 2.2f, 2.3f, 2.4f, 2.5f, 2.6f };

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float coolDown = 0.25f;
    private float lastSwing;

   
    protected override void Start()
    {
        base.Start(); //keep base for collider
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    protected override void Update()
    {
        base.Update(); //need for collision 

        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time - lastSwing> coolDown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }

    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;


            //Create a new damage object, then  we'll send it to the 'Fighter' we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position, //our current player position
                pushForce = pushForce[weaponLevel]
            };

            //send this to enemy
            coll.SendMessage("ReceiveDamage", dmg); //this will be sent to enemy and call ReceiveDamage in enemy and pass dmg
            

        }

    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //change stats %%
    }


    public void SetWeaponLevel(int l)
    {
        weaponLevel = l;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

}
