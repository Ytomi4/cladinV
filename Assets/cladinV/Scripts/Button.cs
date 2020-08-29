using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace cladinV {
    public class Button {
        private InputDevice _device;
        private static bool[] _lastButtonStates = new bool[2];
        static readonly Dictionary<InputFeatureUsage<bool>, bool> _getLastButtonState = new Dictionary<InputFeatureUsage<bool>, bool> {
            {CommonUsages.triggerButton, _lastButtonStates[0]},
            {CommonUsages.gripButton, _lastButtonStates[1]}
        };

        public Button(InputDevice device) {
            _device = device;
        }

       public bool GetButton(InputFeatureUsage<bool> button) {
           _device.TryGetFeatureValue(button, out bool value);
           return value;
       }

       public bool GetButtonDown(InputFeatureUsage<bool> button) {
           try {
               bool lastState = _getLastButtonState[button];
               bool currentState = GetButton(button);
               if (currentState == true && currentState != lastState) {
                   return true;
               } else {
                   return false;
               }
           } 
           catch (Exception e){
               Debug.Log(e);
               return false;
           }
       }

       public void UpdateButtonStates() {
           var availableButtons = _getLastButtonState.Keys.ToArray();
           int length = availableButtons.Length;
           for (int i = 0; i < length; i++) {
               _lastButtonStates[i] = GetButton(availableButtons[i]);
           }
       }
    }
}