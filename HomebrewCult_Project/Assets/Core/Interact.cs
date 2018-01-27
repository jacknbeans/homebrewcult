using UnityEngine;

namespace Core
{
    public class Interact : MonoBehaviour
    {
        public float DropSpeed = 10.0f;
        public Transform HeightPlane;
        public float HeightPlaneMin = -2.0f;
        public float OverlapSphereRadius = 1.0f;

        private bool _startTransition;
        private float _startHeightY;
        private float _oldHeightMin;
        private Interactable _interactableObject;

        private void Start()
        {
            _startHeightY = HeightPlane.position.y;
        }

        private void Update()
        {
            if (_startTransition)
            {
                _startTransition = false;
            }
            else
            {
                var heightY = Input.GetMouseButton(0)
                    ? HeightPlane.position.y - DropSpeed * Time.deltaTime
                    : HeightPlane.position.y + DropSpeed * Time.deltaTime;
                heightY = heightY >= _startHeightY ? _startHeightY : heightY;
                heightY = heightY <= HeightPlaneMin ? HeightPlaneMin : heightY;

                HeightPlane.localPosition = new Vector3(HeightPlane.position.x,
                    heightY,
                    HeightPlane.position.z);
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            string[] layers = {"HandHeightPlane", "Default"};
            var layerMask = LayerMask.GetMask(layers);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask,
                QueryTriggerInteraction.Collide))
            {
                transform.position = hit.point;
            }

            layers = new[] {"InteractableObjects"};
            layerMask = LayerMask.GetMask(layers);
            var touchingInteractableObjects = Input.GetMouseButton(0) && _interactableObject == null
                ? Physics.OverlapSphere(transform.position, OverlapSphereRadius, layerMask)
                : new Collider[] { };

            foreach (var interactableObject in touchingInteractableObjects)
            {
                _interactableObject = interactableObject.GetComponent<Interactable>();
                if (_interactableObject == null)
                {
                    Debug.LogError("No Interactable script found on object in InteractableObjects layer!");
                    continue;
                }

                _interactableObject.Carry(transform);
                break;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_interactableObject != null)
                {
                    _interactableObject.Drop();
                    _interactableObject = null;
                }
            }
        }

        public void StartTransition()
        {
            _startTransition = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            _oldHeightMin = HeightPlaneMin;

            foreach (var contactPoint in other.contacts)
            {
                if (contactPoint.normal.y >= 1.0f)
                {
                    HeightPlaneMin = contactPoint.point.y;
                    break;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            HeightPlaneMin = _oldHeightMin;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, OverlapSphereRadius);
            Gizmos.color = Color.white;
        }
    }
}