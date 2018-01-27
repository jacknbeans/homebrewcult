using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class LookAtBase : MonoBehaviour, IPointerEnterHandler
    {
        public Transform CameraRig;
        public float CameraAngle;
        public float TransitionTime = 0.3f;
        public LookAtBase OtherLookAt;

        protected bool StartTransition;

        private float _velocity;
        private float _threshold = 0.07f;

        private void Update()
        {
            if (StartTransition)
            {
                var newAngle =
                    Mathf.SmoothDamp(CameraRig.eulerAngles.x, CameraAngle, ref _velocity, TransitionTime);
                CameraRig.rotation = Quaternion.Euler(newAngle, CameraRig.eulerAngles.y, CameraRig.eulerAngles.z);
                if (Mathf.Abs(CameraAngle - CameraRig.eulerAngles.x) <= _threshold)
                {
                    StartTransition = false;
                    OtherLookAt.Enable();
                    gameObject.SetActive(false);
                }
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            CameraRig.rotation = Quaternion.Euler(CameraAngle, CameraRig.eulerAngles.y, CameraRig.eulerAngles.z);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartTransition = true;
        }
    }
}