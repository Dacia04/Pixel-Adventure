using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishManager : EnemyBase
{
    #region Enemy Chase
    public float SpeedChase;
    private float SpeedReturn;

    protected Vector2 _originalPosition;
    protected Vector2 _target;

    protected bool _isFacingRightSky = false;

    public Vector2 OriginalPosition { get => _originalPosition; }
    public Vector2 Target { get => _target; set => _target = value; }

    public Vector2 randomRange;
    public float distanceLimit;
    public float ForceMagnitude;
    public GameObject LeafLeft;
    public GameObject LeafRight;
    #endregion

    #region Pos Move Enemy
    public float SpeedGround;
    public GameObject PosLeft;
    public GameObject PosRight; 
    private int _moveDir;
    private bool _isFacingRightGround = false;
    #endregion

    private bool IdleState;
    private bool RunState;
    
    private float distance;
    protected override void Start()
    {
        base.Start();
        _originalPosition = transform.position;
        _target = _originalPosition;
        IdleState = true;
        RunState = false;
        _moveDir=-1;

        SpeedReturn = SpeedChase;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(IdleState)
        {
            AnimationUpdateSky();
            transform.position = Vector2.MoveTowards(transform.position, _target, SpeedChase * Time.deltaTime);
            distance = Vector2.Distance(_originalPosition,transform.position);
            if((Vector2)transform.position == _target)
            {
                SpeedChase = SpeedReturn;
                _target = CreateNewPosRecent(randomRange.x,randomRange.y);
            }

            if(distance >= distanceLimit)
            {
                SpeedChase = SpeedReturn * 3.5f;
                _target = _originalPosition;
            }
        }
        else
        {
            AnimationUpdateGround();
            _rigidbody2D.velocity = new Vector2(SpeedGround * _moveDir, _rigidbody2D.velocity.y);

        }
        
        _animator.SetBool("Idle",IdleState);
        _animator.SetBool("Run",RunState);
    }

    private Vector2 CreateNewPosRecent(float x,float y)
    {
        float X = transform.position.x + Random.Range(x,y);
        float Y = transform.position.y + Random.Range(x,y);
        return new Vector2(X,Y);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            if(_player.collisionDetect.IsEnemyBottom && !_requireNewAttack)
            {
                DecreaseHealth(1);
                _animator.SetTrigger("Hit");
                _requireNewAttack = true;

                if(IdleState)
                    GetHit();
            }
        }
        if(other.gameObject.CompareTag("Map"))
        {
            if(IdleState)
            {
                _target = CreateNewPosRecent(0,randomRange.y);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject == PosLeft)
        {
            _moveDir = 1;
        }
        if (collision.gameObject == PosRight)
        {
            _moveDir = -1;
        }

    }

    private void GetHit()
    {
        _rigidbody2D.gravityScale = 10f;
        IdleState = false;
        RunState = true;

        Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        _isFacingRightGround = true;
        _moveDir = -1;

        LeafLeft.SetActive(true);
        LeafRight.SetActive(true);

        Vector3 forceDirection1 = Quaternion.AngleAxis(Random.Range(100f,170f), Vector3.forward) * Vector3.right;
        Vector3 forceDirection2 = Quaternion.AngleAxis(Random.Range(10f,7f), Vector3.forward) * Vector3.right;
        LeafLeft.GetComponent<Rigidbody2D>().AddForce(forceDirection1 * ForceMagnitude, ForceMode2D.Impulse);
        LeafRight.GetComponent<Rigidbody2D>().AddForce(forceDirection2 * ForceMagnitude, ForceMode2D.Impulse);
        Destroy(LeafLeft,3f);
        Destroy(LeafRight,3f);
    }

    private void AnimationUpdateSky()
    {
        if (!_isFacingRightSky && (transform.position.x - _target.x) > 0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRightSky = !_isFacingRightSky;
        }
        else if (_isFacingRightSky && (transform.position.x - _target.x) < -0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRightSky = !_isFacingRightSky;
        }
    }

    private void AnimationUpdateGround()
    {
        if (!_isFacingRightGround && _rigidbody2D.velocity.x > 0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRightGround = !_isFacingRightGround;
        }
        else if (_isFacingRightGround && _rigidbody2D.velocity.x < -0.1f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRightGround = !_isFacingRightGround;
        }
    }

}
