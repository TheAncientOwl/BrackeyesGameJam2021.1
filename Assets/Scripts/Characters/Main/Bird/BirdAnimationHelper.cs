using UnityEngine;

namespace Characters.Main.Bird
{
    public class BirdAnimationHelper
    {
        private static readonly int s_FLY = Animator.StringToHash("Fly");
        private static readonly int s_IDLE = Animator.StringToHash("Idle");
        private static readonly int s_Walk = Animator.StringToHash("Walk");

        public void Set(Animator animator, BirdState state, float horizontalDirection)
        {
            switch (state)
            {
                case BirdState.InAir:
                    {
                        animator.SetBool(s_FLY, true);
                        animator.SetBool(s_Walk, false);
                        animator.SetBool(s_IDLE, false);
                        break;
                    }
                case BirdState.Grounded:
                    {
                        animator.SetBool(s_FLY, false);
                        if (horizontalDirection == 0f)
                        {
                            animator.SetBool(s_IDLE, true);
                            animator.SetBool(s_Walk, false);
                        }
                        else
                        {
                            animator.SetBool(s_Walk, true);
                            animator.SetBool(s_IDLE, false);
                        }
                        break;
                    }
            }
        }
    }
}