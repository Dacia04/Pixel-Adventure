
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AngPigController : PosMoveEnemy
{
    public float TimeReset;

    private bool _nomalState;
    private bool _angryState;

    private float _currentSpeed;
    private float _angrySpeed;

    protected override void Start()
    {
        base.Start();

        _nomalState = true;
        _angryState = false;

        _currentSpeed = Speed;
        _angrySpeed = Speed * 2;

        StartCoroutine(ResetState());
    }

    protected override void Update()
    {
        base.Update();
        AnimationUpdate();        
    }

    protected override void AnimationUpdate()
    {
        base.AnimationUpdate();

        if (_currentHealth <= (MaxHealth / 2))
        {
            _nomalState = false;
            _angryState = true;
            Speed =_angrySpeed;
        }
        _animator.SetBool("Nomal", _nomalState);
        _animator.SetBool("Angry", _angryState);
    }

    private IEnumerator ResetState()
    {
        while (true)
        {
            if (_angryState)
            {
                yield return new WaitForSeconds(TimeReset);
                _angryState = false;
                _nomalState = true;
                _currentHealth = MaxHealth;
                Speed = _currentSpeed;
            }
            yield return null;
        }
    }

}