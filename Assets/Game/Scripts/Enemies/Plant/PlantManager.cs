using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : ImmobileEnemy
{
    public GameObject ButtletPrefab;

    protected override void Start() {
        _isAttack = true;
        base.Start();
        
    }


    // in Animation Attack
    public void Attack()
    {      
        Instantiate(ButtletPrefab,transform.position, Quaternion.identity);
    }
}
