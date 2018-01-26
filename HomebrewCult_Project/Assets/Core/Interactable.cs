using UnityEngine;

namespace Core
{
    public class Interactable : MonoBehaviour
    {
        private void OnMouseDown()
        {
            var ignoreSelf = LayerMask.NameToLayer("IgnoreSelf");
            gameObject.layer = ignoreSelf;
        }

        private void OnMouseDrag()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            string[] layers = {"Default"};
            var layerMask = LayerMask.GetMask(layers);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.magenta);
                transform.position = hit.point;
            }
        }

        private void OnMouseUp()
        {
            var defaultLayer = LayerMask.NameToLayer("Default");
            gameObject.layer = defaultLayer;
        }
    }
}