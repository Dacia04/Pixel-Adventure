


using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
public class PlayerData: ScriptableObject
{
    public int Health; 
    public int Energy;
    public float TimeImmortal;
    
    [Header("Move State")]
    public float MovementVeclocity;

    [Header("Jump State")]
    public float JumpVelocity;
    public float TimeJump;
    public int AmountOfJump=1;

    [Header("Wall Climb State")]
    public float ClimbVelocity;
    public float TimeClimb;
}