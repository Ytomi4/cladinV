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

       public static void SetUpInputDevices() {
           try {
                LeftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                RightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                LeftHandButton = new Button(LeftHand);
                RightHandButton = new Button(RightHand);
           } 
           catch (Exception e) {
                Debug.Log(e);
           }
       }

       public static void UpdateInputValues() {
           LeftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 lPos);
           LeftHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion lRot);
           LeftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 lP2DAxis);
           RightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rPos);
           RightHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rRot);
           RightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rP2DAxis);

           LeftHandPos = lPos; LeftHandRot = lRot; LeftHandPrimary2DAxis = lP2DAxis;
           RightHandPos = rPos; RightHandRot = rRot; RightHandPrimary2DAxis = rP2DAxis;

           LeftHandButton.UpdateButtonStates();
           RightHandButton.UpdateButtonStates();
       }

    }

}
