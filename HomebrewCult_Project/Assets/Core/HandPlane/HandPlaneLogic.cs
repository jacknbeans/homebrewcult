using UnityEngine;
using System.Collections.Generic;

namespace Core.HandPlane
{
    public class HandPlaneLogic : MonoBehaviour
    {
        public Transform HandPlaneTransform;
        public float DropSpeed = 10.0f;
        public float HeightPlaneMin = 2.0f;
        public float TargetHeightPlaneRotation = 285.0f;
        public float TransitionTime = 0.2f;
        public float Threshold = 0.05f;

        [HideInInspector] public float OldHeightMin;
        [HideInInspector] public float StartHeightY;

        private float _startHeightPlaneRotation;

        private bool _startTransition;
        private float _velocity;

        public void StartTransition()
        {
            _startTransition = true;
        }

        private void Start()
        {
            StartHeightY = HandPlaneTransform.localPosition.y;
            _startHeightPlaneRotation = transform.eulerAngles.x;
        }

        private void Update()
        {
            if (_startTransition)
            {
                var newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, TargetHeightPlaneRotation,
                    ref _velocity, TransitionTime);
                transform.rotation =
                    Quaternion.Euler(newAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                
                if (Mathf.Abs(TargetHeightPlaneRotation - newAngle) <= Threshold)
                {
                    var switchRotation = TargetHeightPlaneRotation;
                    TargetHeightPlaneRotation = _startHeightPlaneRotation;
                    _startHeightPlaneRotation = switchRotation;

                    _startTransition = false;
                }
            }
            else
            {
                var heightY = Input.GetMouseButton(0)
                    ? HandPlaneTransform.localPosition.y - DropSpeed * Time.deltaTime
                    : HandPlaneTransform.localPosition.y + DropSpeed * Time.deltaTime;
                heightY = heightY >= StartHeightY ? StartHeightY : heightY;
                heightY = Mathf.Abs(StartHeightY - heightY) >= HeightPlaneMin ? -HeightPlaneMin : heightY;

                HandPlaneTransform.localPosition = new Vector3(HandPlaneTransform.localPosition.x,
                    heightY,
                    HandPlaneTransform.localPosition.z);
            }
        }
    }
}