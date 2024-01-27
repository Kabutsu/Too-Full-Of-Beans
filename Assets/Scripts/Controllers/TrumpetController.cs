using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class TrumpetController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Bean;

        [SerializeField]
        private float TimeBetweenSpawns = 0.1f;
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
                yield return new WaitForSeconds(TimeBetweenSpawns);
            }

            IsSpawning = false;
        }
    }
}
