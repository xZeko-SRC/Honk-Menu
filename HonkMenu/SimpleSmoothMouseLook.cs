using UnityEngine;

namespace HonkMenu
{
    public class SimpleSmoothMouseLook : MonoBehaviour
    {
        public Vector3 targetDirection;
        public Vector2 mouseAbsolute;
        private Vector2 smoothMouse;

        public Vector2 sensitivity = new Vector2(2f, 2f);
        public Vector2 smoothing = new Vector2(3f, 3f);

        private float yaw;
        private float pitch;

        private void Start()
        {
            targetDirection = transform.localRotation.eulerAngles;

            yaw = targetDirection.y;
            pitch = targetDirection.x;
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 mouseInput = new Vector2(ModInput.GetAxisRaw("FPV Camera X"), ModInput.GetAxisRaw("FPV Camera Y"));

            mouseInput = Vector2.Scale(mouseInput, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseInput.x, 1f / smoothing.x);
            smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseInput.y, 1f / smoothing.y);

            mouseAbsolute += smoothMouse;

            mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, -90f, 90f);

            transform.localRotation = Quaternion.Euler(-mouseAbsolute.y, mouseAbsolute.x, 0f);
        }
    }
}