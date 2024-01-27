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
        private NoteGenerator _noteGenerator;
        private InputControlsInputs _input;

        private AudioSource _playerOneAudio;
        private AudioSource _playerTwoAudio;

        private bool _leftTriggerHeld = false;
        private bool _rightTriggerHeld = false;

        private InputAction _leftAction;

        private void Start()
        {
            _players = new Tuple<PlayerController, PlayerController>(
                PlayerOne.GetComponent<PlayerController>(),
                PlayerTwo.GetComponent<PlayerController>());

            _players.Item1.MaxLeft = -MaxRightAbs;
            _players.Item1.MaxRight = -MaxLeftAbs;

            _players.Item2.MaxLeft = MaxLeftAbs;
            _players.Item2.MaxRight = MaxRightAbs;

            _playerOneAudio = PlayerOne.GetComponentInChildren<AudioSource>();
            _playerTwoAudio = PlayerTwo.GetComponentInChildren<AudioSource>();

            _noteGenerator = GetComponentInChildren<NoteGenerator>();

            _noteGenerator.MaxX = MaxRightAbs;
            _noteGenerator.MinX = MaxLeftAbs;

            _input = GetComponent<InputControlsInputs>();
        }

        private void Update()
        {
            //if (_leftTriggerHeld && !_playerOneAudio.isPlaying)
            //{
            //    _playerOneAudio.Play();
            //}
        }

        void FixedUpdate()
        {

            _players.Item1.Move(_input.moveLeftStick);
            _players.Item2.Move(_input.moveRightStick);
        }

        public void OnLeftTrigger(InputValue value)
        {
            if (value.isPressed)
            {
                _playerOneAudio.Play();
            }

            _players.Item1.Trigger();
        }

        public void OnRightTrigger(InputValue value)
        {
            if (value.isPressed)
            {
                _playerTwoAudio.Play();
            }

            _players.Item2.Trigger();
        }

        //public void OnLeftTriggerHold(InputValue value)
        //{
        //    _leftTriggerHeld = value.isPressed;

        //    if (value.isPressed && !_playerOneAudio.isPlaying)
        //    {
        //        _playerOneAudio.Play();
        //    }
        //    else if (!value.isPressed) _playerOneAudio.Stop();
        //}
    }
}
