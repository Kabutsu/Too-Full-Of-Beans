using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class TrumpetController : MonoBehaviour
    {
        public Vector2 FireRumble = new(0.5f, 3f);

        [SerializeField]
        private GameObject Bean;

        [SerializeField]
        private float TimeBetweenSpawns = 0.125f;
        private int BeansToSpawn = 0;
        private bool IsSpawning = false;

        public void FireBeans(int numberOfBeans)
        {
            BeansToSpawn += numberOfBeans;

            if (!IsSpawning)
            {
                StartCoroutine(SpawnBeans());
            }
        }

        private IEnumerator SpawnBeans()
        {
            IsSpawning = true;

            while (BeansToSpawn > 0)
            {
                Instantiate(Bean, transform.position, transform.rotation);
                BeansToSpawn--;

                Gamepad.current.SetMotorSpeeds(FireRumble.x, FireRumble.y);
                yield return new WaitForSeconds(TimeBetweenSpawns / 1.5f);
                Gamepad.current.ResetHaptics();

                yield return new WaitForSeconds(TimeBetweenSpawns);
            }

            IsSpawning = false;
        }
    }
}
