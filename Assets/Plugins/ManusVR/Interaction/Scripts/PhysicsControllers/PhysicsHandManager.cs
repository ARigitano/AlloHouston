﻿// Copyright (c) 2018 ManusVR
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ManusVR.Scripts.Factory;
using UnityEngine;

namespace Assets.ManusVR.Scripts.PhysicalInteraction
{
    public class PhysicsHandManager : HandManager {

        public HandManager TargetManager;

        [SerializeField]
        private PhysicsHandSettings _physicsHandSettings = null;
        [SerializeField]
        private PhysicsWristSettings _physicsWristSettings = null;
        [SerializeField]
        private PhysicsFingerSettings _physicsFingerSettings = null;

        public Action<PhysicsObject, device_type_t> OnGrabbedItem;
        [Header("Handcontrollers")] public List<PhysicsHand> HandControllers = new List<PhysicsHand>();


        public override IEnumerator InitializeHand(device_type_t deviceType)
        {
            yield return new WaitForFixedUpdate();

            Hand targetHand = TargetManager.GetHandController(deviceType);
            Transform target = targetHand.WristTransform;

            GameObject parent = deviceType == device_type_t.GLOVE_LEFT ? RootBoneLeft.gameObject : RootBoneRight.gameObject;
            
            // Initialize visual body
            SphereCollider collider = target.gameObject.AddComponent<SphereCollider>();
            collider.center = deviceType == device_type_t.GLOVE_LEFT ? new Vector3(-0.096f, 0.07f, 0.015f) : new Vector3(0.096f, -0.07f, -0.015f);
            collider.radius = 0.04f;
            collider.isTrigger = true;
            target.gameObject.AddComponent<TriggerBinder>();

            // Initialize the controller
            var hand = HandFactory.GetHand(parent, HandType.Physics, TargetManager.HandData, this, deviceType);  
            var controller = hand as PhysicsHand;
            controller.PhysicsHandSettings = _physicsHandSettings;
            controller.PhysicsWristSettings = _physicsWristSettings;
            controller.PhysicsFingerSettings = _physicsFingerSettings;


            controller.Target = targetHand;
            HandControllers.Add(controller);
            
            controller.Grabber = CreateObjectGrabber(parent, deviceType, target);

        }

        private ObjectGrabber CreateObjectGrabber(GameObject parent, device_type_t deviceType, Transform target)
        {
            var grabber = parent.AddComponent<ObjectGrabber>();
            
            grabber.DeviceType = deviceType;
            grabber.TriggerBinder = target.GetComponent<TriggerBinder>();
            
            grabber.HandData = TargetManager.HandData;
            
            grabber.OnItemGrabbed += GrabbedItem;
            return grabber;
        }

        private void GrabbedItem(GameObject gameObject, device_type_t type)
        {
            if (OnGrabbedItem == null) return;
            PhysicsObject physicsObject = null;
            PhysicsManager.Instance.GetPhysicsObject(gameObject, out physicsObject);
            OnGrabbedItem(physicsObject, type);
        }
    }
}
