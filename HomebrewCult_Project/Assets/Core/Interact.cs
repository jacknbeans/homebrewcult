using UnityEngine;

namespace Core
{
    public class Interact : MonoBehaviour
    {
        public float DropSpeed = 10.0f;
        public Transform HeightPlane;
        public float HeightPlaneMin = -2.0f;

        private bool _startTransition;
        private float _startHeightY;
        private float _oldHeightMin;
        private BoxCollider _collider;
        private Rigidbody _rigidbody;

        private FixedJoint _otherJoint;

        private void Start()
        {
            _startHeightY = HeightPlane.position.y;
            _collider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
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

            if (_otherJoint != null && Input.GetMouseButtonUp(0))
            {
                _otherJoint.connectedBody = null;
                _otherJoint = null;
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

                    if (other.gameObject.CompareTag("Interactable") && Input.GetMouseButton(0))
                    {
                        _otherJoint = other.gameObject.GetComponent<FixedJoint>();
                        _otherJoint.connectedBody = _rigidbody;
                    }

                    break;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            HeightPlaneMin = _oldHeightMin;
        }
    }
}