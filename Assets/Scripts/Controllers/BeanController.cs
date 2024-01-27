using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BeanController : MonoBehaviour
    {
        [SerializeField]
        private float MoveTime = 0.25f;

        public void Creep(float byAmount)
        {
            StartCoroutine(MoveUp(byAmount));
        }

        private IEnumerator MoveUp(float byAmount)
        {
            Vector2 targetPosition = (Vector2)transform.position + (Vector2.up * byAmount);

            float startTime = Time.time;

            while (Time.time - startTime < MoveTime)
            {
                float t = (Time.time - startTime) / MoveTime;
                transform.position = Vector2.Lerp(transform.position, targetPosition, t);
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}
