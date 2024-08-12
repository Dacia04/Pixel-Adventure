using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    private SaveLoadManager saveLoad;
    private Animator _animator;

    private void Start() {
        saveLoad = GameObject.FindWithTag("saveload").GetComponent<SaveLoadManager>();
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Out",true);
            saveLoad.SaveGameUncompleted();
        }
    }
}
