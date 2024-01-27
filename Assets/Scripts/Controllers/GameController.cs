using System;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerOne;
        [SerializeField]
        private GameObject PlayerTwo;

        [SerializeField]
        private float MaxLeftAbs = 1.0f;
        [SerializeField]
        private float MaxRightAbs = 8.0f;

        private Tuple<PlayerController, PlayerController> _players;
        private InputControlsInputs _input;

        private void Start()
        {
            _players = new Tuple<PlayerController, PlayerController>(
                PlayerOne.GetComponent<PlayerController>(),
                PlayerTwo.GetComponent<PlayerController>());

            _players.Item1.MaxLeft = -MaxRightAbs;
            _players.Item1.MaxRight = -MaxLeftAbs;

            _players.Item2.MaxLeft = MaxLeftAbs;
            _players.Item2.MaxRight = MaxRightAbs;

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
