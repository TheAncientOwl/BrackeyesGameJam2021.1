using UnityEngine;

public abstract class AnimatorHashes
{
    public static readonly int GO = Animator.StringToHash("go");
    public static readonly int IDLE = Animator.StringToHash("Idle");
    public static readonly int SHOOT = Animator.StringToHash("Shoot");
    public static readonly int WALK = Animator.StringToHash("Walk");
    public static readonly int JUMP = Animator.StringToHash("Jump");
    public static readonly int FALL = Animator.StringToHash("Fall");
    public static readonly int FLY = Animator.StringToHash("Fly");
    public static readonly int LEFT_PUNCH = Animator.StringToHash("LeftPunch");
    public static readonly int RIGHT_PUNCH = Animator.StringToHash("RightPunch");
}
