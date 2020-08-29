using System;
using UnityEngine;
using UnityEngine.XR;

namespace cladinV {
    public static class HMDInput 
    {
       public static InputDevice LeftHand { get; private set;}
       public static Vector3 LeftHandPos { get; private set;}
       public static Quaternion LeftHandRot { get; private set;}
       public static Vector2 LeftHandPrimary2DAxis { get; private set;}
       public static Button LeftHandButton { get; private set;}

       public static InputDevice RightHand { get; private set;}
       public static Vector3 RightHandPos { get; private set;}
       public static Quaternion RightHandRot { get; private set;}
       public static Vector2 RightHandPrimary2DAxis {get; private set;}
       public static Button RightHandButton { get; private set;}

       public static void EnableDeviceObserver() {
            SetUpInputDevices();
            InputDevices.deviceConnected += (inputDevice) => {
                SetUpInputDevices();
                Debug.Log($"Connected : {inputDevice.name}");
            };
            InputDevices.deviceDisconnected += (inputDevice) => {
                Debug.Log($"Disconnected : {inputDevice.name}");
            };
       }

       public static void SetUpInputDevices() {
            //If there is no device at the specified endpoint, 
            //InputDevices.GetDeviceAtXRNode returns an InputDevice on which a call to InputDevice.IsValid returns false.
            LeftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            RightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

            LeftHandButton = new Button(LeftHand);
            RightHandButton = new Button(RightHand);
       }

       public static void UpdateInputValues() {
           if (LeftHand.isValid == true) {
                LeftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 lPos);
                LeftHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion lRot);
                LeftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 lP2DAxis);

                LeftHandPos = lPos; LeftHandRot = lRot; LeftHandPrimary2DAxis = lP2DAxis;
           } else {
                LeftHandPos = Vector3.zero; LeftHandRot = Quaternion.identity; LeftHandPrimary2DAxis = Vector2.zero;
           }
           if (RightHand.isValid == true) {
                RightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rPos);
                RightHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rRot);
                RightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rP2DAxis);

                RightHandPos = rPos; RightHandRot = rRot; RightHandPrimary2DAxis = rP2DAxis;
           } else {
                RightHandPos = Vector3.zero; RightHandRot = Quaternion.identity; RightHandPrimary2DAxis = Vector2.zero;
           }
       }

       public static bool GetButtonDown(InputDevice device, InputFeatureUsage<bool> button) {
            if (device == LeftHand && LeftHand.isValid == true) {
                return LeftHandButton.GetButtonDown(button);
            } else if (device == RightHand && RightHand.isValid == true) {
                return RightHandButton.GetButtonDown(button);
            } else {
                return false;
            }
       }

       public static void UpdateLastButtonStates() {
            LeftHandButton.UpdateLastButtonStates();
            RightHandButton.UpdateLastButtonStates();
       }
    }

}
