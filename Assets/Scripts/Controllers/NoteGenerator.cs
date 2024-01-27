using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class NoteGenerator : MonoBehaviour
    {
        public float MaxX { get; set; }
        public float MinX { get; set; }

        [SerializeField]
        private GameObject NoteObject;

        private float y;

        void Start()
        {
            y = transform.position.y;
        }

        void Update()
        {
            if(Random.Range(0f, 1f) >= 0.995f)
            {
                Generate(Random.Range(MinX, MaxX));
            }
        }

        public void Generate(float x)
        {
            Instantiate(
                NoteObject,
                new Vector2(x, y),
                transform.rotation);

            Instantiate(
                NoteObject,
                new Vector2(-x, y),
                transform.rotation);
        }
    }
}
