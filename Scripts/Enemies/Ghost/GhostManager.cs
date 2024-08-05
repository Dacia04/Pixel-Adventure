using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class GhostManager : ChaseEnemy
{
    public float TimeChase;
    public float TimeReset;
    public float ChaseDistance;
    public Vector2 RandomRange;

    public float TimeResetTeleport;


    private bool _chase = false;
    public bool IsChase {get => _chase; set=> _chase =value;}
    private bool appear;
    private bool disappear;
    private Vector2 boxColliderSizeDefault;
    private Vector2 boxColliderSizeNew;
    private float TimeStart;

    private bool CanTeleport=true;

    private GameObject player;
    private SpriteRenderer _spriteRenderer;

    private float distance;
    protected override void Start()
    {
        base.Start();

        boxColliderSizeDefault = new Vector2(_boxCollider2D.size.x,_boxCollider2D.size.y);
        boxColliderSizeNew = new Vector2(_boxCollider2D.size.x/100,_boxCollider2D.size.y/100);

        player = GameObject.FindWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetDisappearState();
        StartCoroutine(ResetTeleport());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        AnimationUpdate();
        ChaseUpdate();
        UpdateBoxCollider();
    }

    protected override void AnimationUpdate()
    {
        base.AnimationUpdate();

        _animator.SetBool("IdleChase",_chase);
        _animator.SetBool("Appear",appear);
        _animator.SetBool("Disappear",disappear);
    }

    private void ChaseUpdate()
    {
        distance = Vector2.Distance(transform.position,player.transform.position);
        if(!_chase)
        {
            SetDisappearState();
            _target = _originalPosition;
        }
        else
        {
            if(distance <= ChaseDistance)
            {
                SetAppearState();
            }
            else
            {
                if(CanTeleport)
                {
                    SetDisappearState();
                    float x,y;
                    if(Random.Range(0,1)==0)
                    {
                        x = player.transform.position.x - Random.Range(RandomRange.x,RandomRange.y);
                    }
                    else
                    {
                        x = player.transform.position.x + Random.Range(RandomRange.x,RandomRange.y);
                    }
                    
                    if(Random.Range(0,1)==0)
                    {
                        y = player.transform.position.y - Random.Range(RandomRange.x,RandomRange.y);
                    }
                    else
                    {
                        y = player.transform.position.y + Random.Range(RandomRange.x,RandomRange.y);
                    }
                    Vector2 newPosition =  new Vector2(x,y);
                    transform.position = newPosition;
                    CanTeleport = false;
                }
                
            }
        }
    }

    private IEnumerator ResetTeleport()
    {
        while(true)
        {
            if(CanTeleport == false)
            {
                yield return new WaitForSeconds(TimeResetTeleport);
                CanTeleport = true;
            }
            yield return null;
        }
    }

    private void changeSizeBoxCollider()
    {
        _boxCollider2D.size = boxColliderSizeNew;
    }

    private void ResetSizeBoxCollider()
    {
        _boxCollider2D.size = boxColliderSizeDefault;
    }

    private void UpdateBoxCollider()
    {
        if(Time.time >= (TimeStart + TimeReset))
        {
            ResetSizeBoxCollider();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            if(_player.collisionDetect.IsEnemyBottom && !_requireNewAttack)
            {
                DecreaseHealth(1);
                _animator.SetTrigger("Hit");
                _requireNewAttack = true;           
            }
            TimeStart = Time.time;
            changeSizeBoxCollider();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _requireNewAttack = false;
        }
    }

    private void SetAppearState()
    {
        appear = true;
        disappear = false;
        _chase = true;     
        SetColorAlphaDefault();
        EnableBoxCollider();

    } 

    private void SetDisappearState()
    {
        appear = false;
        disappear = true;
        _chase = false;     
        DisableBoxCollider();
    }

    public void SetColorAlphaDefault()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r,_spriteRenderer.color.g,_spriteRenderer.color.b,1f);
    }

    public void SetColorrAlphaZero()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r,_spriteRenderer.color.g,_spriteRenderer.color.b,0f);
    }

    public void EnableBoxCollider()
    {
        _boxCollider2D.enabled = true;
    }

    public void DisableBoxCollider()
    {
        _boxCollider2D.enabled = false;
    }
}
