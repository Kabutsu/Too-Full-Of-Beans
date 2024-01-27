using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed = 600f;
        public float SpeedChangeRate = 15f;

        public float MaxLeft { get; set; }
        public float MaxRight { get; set; }

        private Rigidbody2D _rigidBody;

        private float _speed;

        private List<Collider2D> _collisions = new();
    
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            _collisions.Add(collision);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            _collisions.Remove(collision);
        }

        public void Trigger()
        {
            if (_collisions.Count > 0)
            {
                for(int i = 0; i < _collisions.Count; i++)
                {
                    var collider = _collisions[i];

                    collider.gameObject
                        .GetComponent<NoteController>()
                        .Pop(collider.CalculateScore(transform));
                }
            }
        }

        public void Move(Vector2 byAmount)
        {
            if ((byAmount.x < 0 && transform.position.x <= MaxLeft)
                || (byAmount.x > 0 && transform.position.x >= MaxRight))
            {
                return;
            }

            float targetSpeed = byAmount == Vector2.zero
                ? 0.0f
                : MoveSpeed;

            float currentHorizontalSpeed = _rigidBody.velocity.magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = byAmount.magnitude;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // set player's velocity
            _rigidBody.AddForce(byAmount * _speed);
        }
    }
}