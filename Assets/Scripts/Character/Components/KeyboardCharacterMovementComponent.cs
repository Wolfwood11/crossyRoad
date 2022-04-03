using UnityEngine;

namespace Character.Components
{
    public class KeyboardCharacterMovementComponent : CharacterMovementComponent
    {
        protected override bool IsForwardInput()
        {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }

        protected override bool IsLeftInput()
        {
            return Input.GetKeyDown(KeyCode.LeftArrow);
        }

        protected override bool IsRightInput()
        {
            return Input.GetKeyDown(KeyCode.RightArrow);
        }
    }
}