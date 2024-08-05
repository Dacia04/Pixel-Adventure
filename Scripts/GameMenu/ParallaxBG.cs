using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float length,startpos;
    public GameObject Cam;
    public float parallaxSpeed;


    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate() {
        float temp = Cam.transform.position.x *(1-parallaxSpeed);
        float distance = Cam.transform.position.x * parallaxSpeed;

        transform.position = new Vector3(startpos + distance,transform.position.y,transform.position.z);

        if(temp > startpos + length +10f)
            startpos += length;
        else if(temp < startpos - length)
            startpos -= length;
    }
}
