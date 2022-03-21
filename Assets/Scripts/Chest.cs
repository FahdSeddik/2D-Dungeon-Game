using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinsamount = 50;
    
    protected override void OnCollect() 
    {
        if (!collected)
        {
            collected = true;
            //we have to change sprite for chest
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinsamount;
            GameManager.instance.ShowText("+" + coinsamount.ToString() + " coins!", 20, Color.yellow, transform.position, Vector3.up * 25, 0.5f);
        }
    }
}
