using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomManager : PosMoveEnemy
{
    protected override void Start() {
        base.Start();
        _animator.SetBool("Run",true);
    }
}
