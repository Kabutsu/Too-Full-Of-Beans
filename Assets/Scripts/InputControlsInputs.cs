using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class InputControlsInputs : MonoBehaviour
    {
        public Vector2 moveLeftStick;
        public Vector2 moveRightStick;

        public void OnMoveLeftStick(InputValue value)
        {
            moveLeftStick = value.Get<Vector2>();
        }

        public void OnMoveRightStick(InputValue value)
        {
            moveRightStick = value.Get<Vector2>();
        }
    }
}
