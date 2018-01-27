using UnityEngine;

namespace Core
{
    public class Interact : MonoBehaviour
    {
        public float ScrollSpeed = 20.0f;
        public Transform HeightPlane;

        private void Update()
        {
            HeightPlane.position = new Vector3(HeightPlane.position.x, HeightPlane.position.y + Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ScrollSpeed, HeightPlane.position.z);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                transform.position = hit.point;
            }
        }
    }
}