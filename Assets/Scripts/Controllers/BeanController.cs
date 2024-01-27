using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BeanController : MonoBehaviour
    {
        private Rigidbody2D _rigidBody;
        private Collider2D _collider;

        [SerializeField]
        private int yForce = 400;
        [SerializeField]
        private int xForce = 350;

        [SerializeField]
        private float minMultiplier = 0.85f;
        [SerializeField]
        private float maxMultiplier = 1.15f;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            _collider.isTrigger = true;

            if (transform.position.x > 0) xForce = -xForce;

            float multiplier = Random.Range(
                minMultiplier,
                maxMultiplier);

            _rigidBody.AddForce(new Vector2(xForce, yForce) * multiplier);

            float angle = Random.Range(180, 355) * Mathf.Rad2Deg;
            Vector2 torqueVector = multiplier * xForce * new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle));

            _rigidBody.AddTorque(torqueVector.magnitude);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.Divider))
            {
                _collider.isTrigger = false;
            }
        }
    }
}
