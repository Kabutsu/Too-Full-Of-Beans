using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class NoteController : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private SpriteRenderer _spriteRenderer;
        private Camera _mainCamera;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mainCamera = Camera.main;
        }

        void Update()
        {
            if (!IsSpriteVisible() && transform.position.y > 0)
            {
                Destroy(gameObject);
            }

            transform.Translate(_speed * Time.deltaTime * Vector3.up);
        }

        public void Pop(Constants.Scores score)
        {
            Debug.Log(Helpers.GetName(score));
            Destroy(gameObject);
        }

        private bool IsSpriteVisible()
        {
            // Check if the sprite is within the camera's view frustum
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
            Bounds bounds = _spriteRenderer.bounds;
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
    }
}
