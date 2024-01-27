using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StrutPainter : MonoBehaviour
    {
        [SerializeField]
        private GameObject VerticalBar;

        [SerializeField]
        private float minX = -0.4985f;
        [SerializeField]
        private float maxX = 0.4985f;
        [SerializeField]
        private int numberOfBars = 9;

        // Start is called before the first frame update
        void Start()
        {
            float spacing = (maxX - minX) / numberOfBars;

            for (int i = 0; i < numberOfBars; i++)
            {
                Instantiate(
                    VerticalBar,
                    new Vector2(
                        transform.position.x,
                        minX + (i * spacing)),
                    Quaternion.Euler(new Vector3(0, 0, 90f)),
                    transform);
            }
        }
    }
}
