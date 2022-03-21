using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage 
{
    //Container for damage like if we are firing a fire arrow 
    //contain who sent it damage velocity etc
    //ie damage structure
    public Vector3 origin; 
    public int damageAmount; //reduce hitpoints by damage amount
    public float pushForce; //push the receiver using push force

}
