using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonkMenu
{
    public static class ModInput
    {
        public static IDictionary<string, Func<bool>> ButtonMap = new Dictionary<string, Func<bool>>();
        public static IDictionary<string, Func<float>> AxisMap = new Dictionary<string, Func<float>>();
        private static IDictionary<string, bool> _ButtonsPrev = new Dictionary<string, bool>();

        internal static void LateUpdate()
        {
            foreach (KeyValuePair<string, Func<bool>> keyValuePair in ButtonMap)
            {
                _ButtonsPrev[keyValuePair.Key] = keyValuePair.Value != null && keyValuePair.Value();
            }
        }

        public static bool GetButton(string buttonName)
        {
            return ButtonMap.TryGetValue(buttonName, out Func<bool> func) && func != null && func();
        }

        public static bool GetButtonDown(string button)
        {
            return _ButtonsPrev.TryGetValue(button, out bool wasPressed) && !wasPressed && GetButton(button);
        }

        public static float GetAxis(string axis)
        {
            return GetAxisRaw(axis);
        }

        public static float GetAxisRaw(string axis)
        {
            return AxisMap.TryGetValue(axis, out Func<float> func) && func != null ? func() : 0f;
        }

        public static float AxisOr(float a, float b)
        {
            return Mathf.Abs(a) > 0.1f ? a : b;
        }

        public static float Deadzone(float input, float deadzone)
        {
            return Mathf.Abs(input) < deadzone ? 0f : input;
        }

        private static void _InitButtonMap(string id, params string[] names)
        {
            Func<bool> value = () => Input.GetButton(id);
            foreach (string name in names)
            {
                ButtonMap[name] = value;
            }
        }

        static ModInput()
        {
            AxisMap["Horizontal"] = () => AxisOr(Input.GetKey(KeyCode.A) ? -1f : (Input.GetKey(KeyCode.D) ? 1f : 0f), Deadzone(Input.GetAxis("Joy1Axis1"), 0.17f) * 0.3f);
            AxisMap["Vertical"] = () => AxisOr(Input.GetKey(KeyCode.S) ? -1f : (Input.GetKey(KeyCode.W) ? 1f : 0f), Deadzone(-Input.GetAxis("Joy1Axis2"), 0.17f) * 0.3f);
            AxisMap["FPV Camera X"] = () => AxisOr(Input.GetAxis("Mouse X") * 0.5f, Deadzone(Input.GetAxis("Joy1Axis4"), 0.17f) * 0.5f);
            AxisMap["FPV Camera Y"] = () => AxisOr(Input.GetAxis("Mouse Y") * 0.5f, Deadzone(-Input.GetAxis("Joy1Axis5"), 0.17f) * 0.5f);
            AxisMap["Mouse X"] = () => Input.GetAxis("Mouse X");
            AxisMap["Mouse Y"] = () => Input.GetAxis("Mouse Y");

            ButtonMap["SwitchCameraMode"] = () => Input.GetKeyDown(KeyCode.Alpha8);
            ButtonMap["DisableFPSMode"] = () => Input.GetKeyDown(KeyCode.Alpha9);
            ButtonMap["Sprint"] = () => Input.GetKey(KeyCode.LeftShift);
        }
    }
}
