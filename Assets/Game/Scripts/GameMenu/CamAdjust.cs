using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAdjust : MonoBehaviour
{
    public float speedCam;


    private Vector3 p;
    private void Start() {
        p = transform.position;
    }

    private void Update() {
        Vector2 pos = new Vector3(transform.position.x + speedCam,p.y,p.z);
        transform.position = pos;
    }
}
