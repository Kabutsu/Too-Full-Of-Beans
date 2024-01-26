using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed = 600f;
        public float SpeedChangeRate = 15f;

        private Rigidbody2D _rigidBody;

        private float _speed;
    
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Trigger()
        {
            Debug.Log("Trigger!");
        }

        public void Move(Vector2 byAmount)
        {
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
