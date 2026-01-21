using UnityEngine;

namespace Move
{
    public class MovableSprite
    {
        private bool isRight, isDown, isUp, isLeft;

        public void SetDirection(bool right, bool left, bool down, bool up)
        {
            isRight = right;
            isLeft = left;
            isDown = down;
            isUp = up;
        }

        public void SetAnimator(Animator animator)
        {
            animator.SetBool("isDown", isDown);
            animator.SetBool("isRight", isRight);
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isUp", isUp);
        }
    }
}
