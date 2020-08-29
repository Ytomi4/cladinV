using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace cladinV {
    public class Button {
        private InputDevice _device;
        private static InputFeatureUsage<bool>[] _availableButtons = {
            CommonUsages.triggerButton,
            CommonUsages.gripButton
        };
        private static bool[] _lastButtonStates = new bool[_availableButtons.Length];

        public Button(InputDevice device) {
            _device = device;
        }

       public bool GetButton(InputFeatureUsage<bool> button) {
           _device.TryGetFeatureValue(button, out bool value);
           return value;
       }

       public bool GetButtonDown(InputFeatureUsage<bool> button) {
           try {
               bool lastState = GetLastButtonState(button);
               bool currentState = GetButton(button);
               if (currentState == true && currentState != lastState) {
                   return true;
               } else {
                   return false;
               }
           } 
           catch {
               return false;
           }
       }

       public bool GetLastButtonState(InputFeatureUsage<bool> button) {
           int index = Array.IndexOf(_availableButtons, button);
           return _lastButtonStates[index];
       }

       public void UpdateLastButtonStates() {
           int length = _availableButtons.Length;
           for (int i = 0; i < length; i++) {
                _lastButtonStates[i] = GetButton(_availableButtons[i]);
           }
       }
    }
}