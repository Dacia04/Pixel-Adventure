using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    private Light2D LightMap;

    // Start is called before the first frame update
    void Start()
    {
        LightMap = GameObject.FindWithTag("LightMap").GetComponent<Light2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {

            LightMap.intensity =0f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LightMap.intensity =0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ra");
            LightMap.intensity =1f;
        }
    }
}
