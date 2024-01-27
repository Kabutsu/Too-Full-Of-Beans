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
        private GameObject LeftTrumpet;
        [SerializeField]
        private GameObject RightTrumpet;

        [SerializeField]
        private float MaxLeftAbs = 1.5f;
        [SerializeField]
        private float MaxRightAbs = 7.5f;

        private Tuple<PlayerController, PlayerController> _players;
        private NoteGenerator _noteGenerator;
        private InputControlsInputs _input;

        private void Start()
        {
            Application.targetFrameRate = -1;

            _players = new Tuple<PlayerController, PlayerController>(
                PlayerOne.GetComponent<PlayerController>(),
                PlayerTwo.GetComponent<PlayerController>());

            _players.Item1.MaxLeft = -MaxRightAbs;
            _players.Item1.MaxRight = -MaxLeftAbs;
            _players.Item1.TrumpetController = LeftTrumpet.GetComponent<TrumpetController>();

            _players.Item2.MaxLeft = MaxLeftAbs;
            _players.Item2.MaxRight = MaxRightAbs;
            _players.Item2.TrumpetController = RightTrumpet.GetComponent<TrumpetController>();

            _noteGenerator = GetComponentInChildren<NoteGenerator>();

            _noteGenerator.MaxX = MaxRightAbs;
            _noteGenerator.MinX = MaxLeftAbs;

            _input = GetComponent<InputControlsInputs>();
        }

        void FixedUpdate()
        {

            _players.Item1.Move(_input.moveLeftStick);
            _players.Item2.Move(_input.moveRightStick);
        }

        public void OnLeftTrigger(InputValue value)
        {
            _players.Item1.Trigger(value.isPressed);
        }

        public void OnRightTrigger(InputValue value)
        {
            _players.Item2.Trigger(value.isPressed);
        }
    }
}
