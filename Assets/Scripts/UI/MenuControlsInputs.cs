using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControlsInputs : MonoBehaviour
{
    public void OnMoveLeftStick(InputValue value)
    {
        FindObjectOfType<MainMenuController>().SelectButton((ControllerDirection)value.Get<Vector2>().x);
    }

    public void OnConfirm(InputValue value)
    {
        FindObjectOfType<MainMenuController>().StartGame();
    }
}
