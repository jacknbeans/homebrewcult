using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.SwitchCamera
{
    public class LookAtClient : LookAtBase, IPointerEnterHandler
    {
        public Color FlashingColor = Color.yellow;
        public float StartDelay = 5.0f;
        public float FlashingSpeed = 2.0f;
        public ClientManager TheClientManager;

        private Image _image;
        private Color _initialImageColor;

        private float _timer;
        private float _pingPong;

        private bool _firstTime = true;

        private void Start()
        {
            _image = GetComponent<Image>();
            _initialImageColor = _image.color;
        }

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

            if (_firstTime)
            {
                _timer += Time.deltaTime;

                if (_timer >= StartDelay)
                {
                    _pingPong += Time.deltaTime;
                    var value = Mathf.PingPong(_pingPong * FlashingSpeed, 1.0f);
                    _image.color = Color.Lerp(_initialImageColor, FlashingColor, value);
                }
            }
        }

        public new void OnPointerEnter(PointerEventData eventData)
        {
            StartTransition = true;
            HandRef.StartTransition();

            if (_firstTime)
            {
                _firstTime = false;
                TheClientManager.StartDialogue();
                _image.color = _initialImageColor;
            }
        }
    }
}