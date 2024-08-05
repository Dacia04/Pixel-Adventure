using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float timeLoop;
    public bool onState;
    public GameObject BoxDamage;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        onState = false;

        StartCoroutine(StateCoroutine());
    }

    void Update()
    {

        if (onState)
        {
            Onstate();
        }
        else
        {
            OffState();
        }

    }
    private void Onstate()
    {
        _animator.SetBool("On", true);
        BoxDamage.SetActive(true);
    }

    private void OffState()
    {
        _animator.SetBool("On", false);
        BoxDamage.SetActive(false);
    }


    private IEnumerator StateCoroutine()
    {
        while (true)
        {
            onState = !onState;
            yield return new WaitForSeconds(timeLoop);
        }
    }
}
