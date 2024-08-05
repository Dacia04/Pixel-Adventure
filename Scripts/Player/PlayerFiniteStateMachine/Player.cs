using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player: MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerFallingState FallState { get; private set; }

    [SerializeField]
    public PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim {  get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb2D { get; private set; }
    public BoxCollider2D Collider { get; private set;}
    public AudioSource audioSource {get;private set;}
    public AudioSystem audioSystem {get;private set;}
    #endregion

    #region Other variables
    public CollisionDetect collisionDetect { get; private set; }
    public Movement movement { get; private set; }

    public PlayerCollision playerCollsion;

    public int EnergyLeft;
    public int CurrentHealth;
    public bool IsHit;
    public bool Immortal;
    private float TimeStartImmortal;

    private PlayerGameplay _input;
    public Knockback Knockback;
    #endregion

    #region State Variable
    // private bool IdleBool;    
    // private bool MoveBool;
    // private bool JumpBool;
    // private bool DoubleJumpBool;
    // private bool WallClimbBool;
    // private bool HitBool;
    // private bool FallBool;

    // public bool IdleBoolState {get => IdleBool;}
    // public bool MoveBoolState {get => MoveBool;}
    // public bool JumpBoolState {get => JumpBool;}
    // public bool DoubleJumpBoolState {get => DoubleJumpBool;}
    // public bool WallClimbBoolState {get => WallClimbBool;}
    // public bool HitBoolState {get => HitBool;}
    // public bool FallBoolState {get => FallBool;}

    private string IdleString= "Idle";
    private string MoveString ="Move";
    private string JumpString ="Jump";
    private string DoubleJumpString="DoubleJump";
    private string WallClimbString="WallClimb";
    private string HitString="Hit";
    private string FallString="Fall";
    #endregion

    #region Unity CallBack Function
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, IdleString);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, MoveString);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, JumpString);
        DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, DoubleJumpString);
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, WallClimbString);
        HitState = new PlayerHitState(this, StateMachine, playerData, HitString);
        FallState = new PlayerFallingState(this, StateMachine, playerData, FallString);

        collisionDetect = GetComponentInChildren<CollisionDetect>();
        movement = GetComponentInChildren<Movement>();

        _input = new PlayerGameplay();
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();    
        InputHandler = GetComponent<PlayerInputHandler>();
        playerCollsion = GetComponent<PlayerCollision>();
        Rb2D = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        Knockback = GetComponent<Knockback>();
        audioSource = GetComponent<AudioSource>();
        audioSystem = GameObject.FindWithTag("Audio").GetComponent<AudioSystem>();

        StateMachine.Initialize(IdleState);

        EnergyLeft = playerData.Energy;
        CurrentHealth = playerData.Health;

        Immortal = false;
        
    }

    private void Update()
    {
        collisionDetect.LogicUpdate();
        movement.LogicUpdate();

        StateMachine.CurrentState.LogicUpdate();

        

        Knockback.LogicUpdate();

        IsHit = playerCollsion.IsHit;

        CheckImmortal();
        Death();

        MoveWall();
        WallTop();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicUpdate();
    }
    #endregion


    private void MoveWall()
    {
        float NormX = InputHandler.NormInputX;
        bool IsFacingRight = movement.IsFacingRight;
        bool IsWallRight = collisionDetect.IsWallRight;

        
        if(NormX > 0 && IsFacingRight && IsWallRight)
        {
            Rb2D.velocity = new Vector2(0f,Rb2D.velocity.y);
        }

        if(NormX < 0 && !IsFacingRight && IsWallRight) 
        {
            Rb2D.velocity = new Vector2(0f, Rb2D.velocity.y);
        }
    }

    private void WallTop()
    {
        bool IsWallTop = collisionDetect.IsWallTop;
        bool IsWallRight = collisionDetect.IsWallRight;
        bool IsWallLeft = collisionDetect.IsWallLeft;
        if(IsWallTop && (!IsWallLeft && !IsWallRight))
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x,0f);
        }
    }


    //death
    private void Death()
    {
        if(CurrentHealth <=0)
        {
            audioSystem.PlayDeathAudio(audioSource);
            Anim.SetTrigger("Disappear");
            movement.SetVelocityZero();      
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void RePlayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Immortal
    private void CheckImmortal()
    {
        if(Immortal && Time.time >= (playerData.TimeImmortal + TimeStartImmortal))
        {
            
            Immortal = false;
        }
    }



    //control input
    public void EnableInput()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    public void DisableInput()
    {
        GetComponent<PlayerInput>().enabled = false;
    }


    //control health
    public void DecreaseHealth(int damage)
    {
        if (!Immortal)
        {
            CurrentHealth -= damage;
            Immortal = true;
            TimeStartImmortal = Time.time;
        }

    }
    public void IncreaseHealth(int heal)
    {
        CurrentHealth += heal;
    }

    public void DecreaseAllHealth()
    {
        CurrentHealth = 0;
    }


    //onther function
    public void FinishAnimationHit()
    {
        HitState.AnimationFinish = true;
    }

    // public void UpdateState()
    // {
    //     string temp = StateMachine.CurrentState._animBoolName;
    //     if( temp == IdleString)
    //     {
    //         SetIdleState();
    //     }
    //     else if(temp == MoveString)
    //     {
    //         SetMoveState();
    //     }
    //     else if(temp == JumpString)
    //     {
    //         SetJumpState();
    //     }
    //     else if(temp == DoubleJumpString)
    //     {
    //         SetDoubleJumpState();
    //     }
    //     else if(temp == WallClimbString)
    //     {
    //         SetWallClimbState();
    //     }
    //     else if(temp == HitString)
    //     {
    //         SetHitState();
    //     }
    //     else if(temp == FallString)
    //     {
    //         SetFallState();
    //     }
    // }
    // public void SetIdleState()
    // {
    //     IdleBool = true;
    //     MoveBool = false;
    //     JumpBool = false;
    //     DoubleJumpBool = false;
    //     WallClimbBool = false;
    //     HitBool = false;
    //     FallBool = false;
    // }
    // public void SetMoveState()
    // {
    //     IdleBool = false;
    //     MoveBool = true;
    //     JumpBool = false;
    //     DoubleJumpBool = false;
    //     WallClimbBool = false;
    //     HitBool = false;
    //     FallBool = false;
    // }
    // public void SetJumpState()
    // {
    //     IdleBool = false;
    //     MoveBool = false;
    //     JumpBool = true;
    //     DoubleJumpBool = false;
    //     WallClimbBool = false;
    //     HitBool = false;
    //     FallBool = false;
    // }
    // public void SetDoubleJumpState()
    // {
    //     IdleBool = false;
    //     MoveBool = false;
    //     JumpBool = false;
    //     DoubleJumpBool = true;
    //     WallClimbBool = false;
    //     HitBool = false;
    //     FallBool = false;
    // }
    // public void SetWallClimbState()
    // {
    //     IdleBool = false;
    //     MoveBool = false;
    //     JumpBool = false;
    //     DoubleJumpBool = false;
    //     WallClimbBool = true;
    //     HitBool = false;
    //     FallBool = false;
    // }
    // public void SetHitState()
    // {
    //     IdleBool = false;
    //     MoveBool = false;
    //     JumpBool = false;
    //     DoubleJumpBool = false;
    //     WallClimbBool = false;
    //     HitBool = true;
    //     FallBool = false;
    // }
    // public void SetFallState()
    // {
    //     IdleBool = false;
    //     MoveBool = false;
    //     JumpBool = false;
    //     DoubleJumpBool = false;
    //     WallClimbBool = false;
    //     HitBool = false;
    //     FallBool = true;
    // }
}