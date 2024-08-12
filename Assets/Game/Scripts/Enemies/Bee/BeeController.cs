using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : ImmobileEnemy
{
    public GameObject ButtletPrefab;


    // in Animation Attack
    public void Attack()
    {      
        Instantiate(ButtletPrefab,transform.position, Quaternion.identity);
    }


}
