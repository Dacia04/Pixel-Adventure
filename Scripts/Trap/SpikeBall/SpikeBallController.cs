using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    public GameObject ChainPrefab;
    public GameObject SpikeBallPrefab;
    public int chainLength = 10;
    public float rotationTrap;
    public float velocity;
    public float timeCount;

    [Range(0f,1f)]
    public float segmentDistance;


    private GameObject[] _chainSegments;
    private GameObject _spikeBall;
    private Rigidbody2D _rigidbody2D;
    private bool start = true;

    void Start()
    {
        _chainSegments = new GameObject[chainLength];
        int i = 0;
        for (i = 0; i < chainLength; i++)
        {
            _chainSegments[i] = Instantiate(ChainPrefab, new Vector3(transform.position.x, transform.position.y - i * segmentDistance, 1), Quaternion.identity);
            _chainSegments[i].transform.parent = transform;
        }
        _spikeBall = Instantiate(SpikeBallPrefab,new Vector3(transform.position.x,transform.position.y - i * segmentDistance,0),Quaternion.identity);
        _spikeBall.transform.parent = transform;


        _rigidbody2D = GetComponent<Rigidbody2D>();

        StartCoroutine(ChangeVelovity());
        
    }

    void Update()
    {
        
        _rigidbody2D.angularVelocity = velocity;
    }

    private IEnumerator ChangeVelovity()
    {
        while(true)
        {
            velocity = -velocity;
            if (start)
            {
                yield return new WaitForSeconds(timeCount / 2);
                start = false;
            }
            else
                yield return new WaitForSeconds(timeCount);
            
        }
    }



}
