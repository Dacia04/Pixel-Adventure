using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButtletManager : MonoBehaviour
{
    public GameObject BulletPiece1;
    public GameObject BulletPiece2;

    public float Speed;
    public float MoveDir;
    public float ForceMagnitude;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    GameObject piece1, piece2;
  
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();       
        _rigidbody2D.velocity= new Vector2(MoveDir * Speed,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().playerCollsion.IsHit = false;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Map")  || collision.gameObject.CompareTag("Platform"))
        {
            
            Destroy(gameObject);
            piece1 = Instantiate(BulletPiece1, transform.position,Quaternion.identity);
            piece2 = Instantiate(BulletPiece2, transform.position, Quaternion.identity);
            Vector3 forceDirection1,forceDirection2;
            if(MoveDir == -1 )
            {
                forceDirection1 = Quaternion.AngleAxis(Random.Range(75f,86f), Vector3.forward) * Vector3.right;
                forceDirection2 = Quaternion.AngleAxis(Random.Range(275f,290f), Vector3.forward) * Vector3.right;
            }
            else
            {
                forceDirection1 = Quaternion.AngleAxis(Random.Range(96f,115f), Vector3.forward) * Vector3.right;
                forceDirection2 = Quaternion.AngleAxis(Random.Range(250f,265f), Vector3.forward) * Vector3.right;
            }
            
            piece1.GetComponent<Rigidbody2D>().AddForce(forceDirection1 * ForceMagnitude, ForceMode2D.Impulse);
            piece2.GetComponent<Rigidbody2D>().AddForce(forceDirection2 * ForceMagnitude, ForceMode2D.Impulse);
            Destroy(piece1, 2f);
            Destroy(piece2, 2f);

          
        }
        
        
    }
}
