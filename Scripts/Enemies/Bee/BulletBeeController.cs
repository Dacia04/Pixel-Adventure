using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBeeController : MonoBehaviour
{
    public GameObject BulletPiece1;
    public GameObject BulletPiece2;

    public float Speed;
    public float ForceMagnitude;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    GameObject piece1, piece2;
  
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();       
        _rigidbody2D.AddForce(Vector2.down * Speed, ForceMode2D.Impulse);
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
            Vector3 forceDirection1 = Quaternion.AngleAxis(Random.Range(100f,170f), Vector3.forward) * Vector3.right;
            Vector3 forceDirection2 = Quaternion.AngleAxis(Random.Range(10f,7f), Vector3.forward) * Vector3.right;
            piece1.GetComponent<Rigidbody2D>().AddForce(forceDirection1 * ForceMagnitude, ForceMode2D.Impulse);
            piece2.GetComponent<Rigidbody2D>().AddForce(forceDirection2 * ForceMagnitude, ForceMode2D.Impulse);
            Destroy(piece1, 2f);
            Destroy(piece2, 2f);

          
        }
        
        
    }
}
