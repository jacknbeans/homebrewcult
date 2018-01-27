using UnityEngine;

namespace Core
{
    public class Interact : MonoBehaviour
    {
        public HandPlane.HandPlaneLogic HeightPlane;
        public float OverlapSphereRadius = 1.0f;
        public float TargetRotationLookAtClient = 285.0f;
        public float TransitionTime = 0.2f;
        public float Threshold;
        public MeshRenderer HandRenderer;
        public Material ClosedHandMat;
        public Material OpenHandMat;
        const ;lkajsdf;lkj

        private Interactable _interactableObject;

        private bool _startTransition;
        private float _velocity;
        private float _startRotation;

        private Collider _collider;

        private void Start()
        {
            _startRotation = transform.eulerAngles.x;
            _collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (_startTransition)
            {
                var newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, TargetRotationLookAtClient,
                    ref _velocity, TransitionTime);
                transform.rotation =
                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newAngle);
                
                if (Mathf.Abs(TargetRotationLookAtClient - newAngle) <= Threshold)
                {
                    var switchRotation = TargetRotationLookAtClient;
                    TargetRotationLookAtClient = _startRotation;
                    _startRotation = switchRotation;

                    _startTransition = false;
                    _collider.isTrigger = false;
                }
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
                HandRenderer.material = ClosedHandMat;
                break;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_interactableObject != null)
                {
                    _interactableObject.Drop();
                    HandRenderer.material = OpenHandMat;
                    _interactableObject = null;
                }
            }
        }

        public void StartTransition()
        {
            _startTransition = true;
            _collider.isTrigger = true;
            HeightPlane.StartTransition();
        }

        private void OnCollisionEnter(Collision other)
        {
            HeightPlane.OldHeightMin = HeightPlane.HeightPlaneMin;

            foreach (var contactPoint in other.contacts)
            {
                if (contactPoint.normal.y >= 1.0f)
                {
                    HeightPlane.HeightPlaneMin = Mathf.Abs(HeightPlane.transform.InverseTransformPoint(contactPoint.point).y - HeightPlane.StartHeightY);
                    break;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            HeightPlane.HeightPlaneMin = HeightPlane.OldHeightMin;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, OverlapSphereRadius);
            Gizmos.color = Color.white;
        }
    }
}