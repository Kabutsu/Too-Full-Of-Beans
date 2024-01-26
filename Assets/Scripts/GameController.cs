using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerOne;
        [SerializeField]
        private GameObject PlayerTwo;

        private Tuple<PlayerController, PlayerController> _players;
        private InputControlsInputs _input;

        private void Start()
        {
            _players = new Tuple<PlayerController, PlayerController>(
                PlayerOne.GetComponent<PlayerController>(),
                PlayerTwo.GetComponent<PlayerController>());

            _input = GetComponent<InputControlsInputs>();
        }

        void FixedUpdate()
        {
            _players.Item1.Move(_input.moveLeftStick);
            _players.Item2.Move(_input.moveRightStick);
        }

        public void OnLeftTrigger(InputValue value)
        {
            _players.Item1.Trigger();
        }

        public void OnRightTrigger(InputValue value)
        {
            _players.Item2.Trigger();
        }
    }
}
