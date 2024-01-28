using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Menu
{
    public class MenuPlayerController : MonoBehaviour
    {
        public GameObject Bean;

        [SerializeField]
        private float TimeBetweenSpawns = 0.125f;

        private Vector3 BeanSpawnCoordinates = new(-0.1f, 1.9f, -1f);

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ThrowBeans());
        }

        private IEnumerator ThrowBeans()
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 4f));

                int beansToSpawn = Random.Range(1, 6);

                while (beansToSpawn > 0)
                {
                    Instantiate(Bean, BeanSpawnCoordinates, transform.rotation);
                    beansToSpawn--;

                    yield return new WaitForSeconds(TimeBetweenSpawns / 1.5f);

                    yield return new WaitForSeconds(TimeBetweenSpawns);
                }
            }
        }
    }
}
