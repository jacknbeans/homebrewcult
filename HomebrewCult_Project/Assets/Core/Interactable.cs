﻿using UnityEngine;

namespace Core
{
    public class Interactable : MonoBehaviour
    {
        private Transform _hand;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool isHeld;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Carry(Transform parentTransform)
        {
            _hand = parentTransform;
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
            isHeld = true;
        }

        public void Drop()
        {
            _hand = null;
            _rigidbody.isKinematic = false;
            _collider.isTrigger = false;
            isHeld = false;
        }

        private void Update()
        {
            if (_hand != null)
            {
                transform.position = _hand.position;
            }
        }

        public bool IsHeld()
        {
            return isHeld;
        }
    }
}