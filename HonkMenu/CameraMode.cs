using System.Collections;
using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class CameraMode : MonoBehaviour
    {
        public float moveSpeed = 1.1f, sprintSpeed = 1.9f, lookSensitivity = 3.0f;
        private float pitch = 0f;
        private Rigidbody gooseRigidbody;
        private GooseNeckEvents gooseNeck;
        private Camera fpsCamera;
        private GameObject goose, gooseMesh;
        private Transform headTransform;
        private bool isFPSMode = false;


        void Start()
        {
            StartCoroutine(InitializeGoose());
        }

        private IEnumerator InitializeGoose()
        {
            while (goose == null)
            {
                goose = GameObject.Find("Goose");

                if (goose != null)
                {
                    MelonLogger.Msg("Goose found!");
                    gooseMesh = goose.transform.Find("Goosemesh")?.gameObject;
                    headTransform = goose.transform.Find("Body_Armature/head-control");

                    if (fpsCamera == null)
                    {
                        GameObject cameraObject = new GameObject("FPSCamera");
                        fpsCamera = cameraObject.AddComponent<Camera>();
                        fpsCamera.enabled = false;
                    }

                    if (headTransform != null)
                    {
                        fpsCamera.transform.SetParent(headTransform);
                        fpsCamera.transform.localPosition = new Vector3(7, 0, 0);
                        //fpsCamera.transform.localPosition = Vector3.zero;
                        fpsCamera.transform.localRotation = Quaternion.identity;
                    }

                    MelonLogger.Msg("FPS Camera initialized successfully.");
                }

                yield return new WaitForSeconds(1f);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                EnableFPSMode();
                MelonLogger.Msg("FPS Mode Enabled");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                DisableFPSMode();
                MelonLogger.Msg("FPS Mode Disabled");
            }

            if (isFPSMode)
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                HandleMovement();
                HandleMouseLook();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void EnableFPSMode()
        {
            if (goose == null || fpsCamera == null) return;

            isFPSMode = true;

            HideGooseBody();

            if (gooseMesh != null)
                gooseMesh.SetActive(false);

            fpsCamera.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void DisableFPSMode()
        {
            if (goose == null || fpsCamera == null || gooseMesh == null) return;

            isFPSMode = false;

            ShowGooseBody();

            fpsCamera.enabled = false;
            gooseMesh.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void HandleMovement()
        {
            if (goose == null) return;

            if (gooseRigidbody == null)
            {
                gooseRigidbody = goose.GetComponent<Rigidbody>();
                if (gooseRigidbody == null)
                {
                    MelonLogger.Error("Goose Rigidbody not found!");
                    return;
                }
            }

            if (gooseNeck == null)
            {
                gooseNeck = goose.GetComponent<GooseNeckEvents>();
            }

            bool isCrouched = (gooseNeck != null && !gooseNeck.cleared);

            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");

            float currentSpeed = (Input.GetKey(KeyCode.LeftShift) || isCrouched) ? sprintSpeed : moveSpeed;

            Vector3 moveDirection = fpsCamera.transform.forward * moveVertical + fpsCamera.transform.right * moveHorizontal;
            moveDirection.y = 0; 

            Vector3 newPosition = gooseRigidbody.position + moveDirection * currentSpeed * Time.deltaTime;

            gooseRigidbody.MovePosition(newPosition);
        }

        private void HandleMouseLook()
        {
            if (goose == null || fpsCamera == null) return;

            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            goose.transform.Rotate(0, mouseX, 0);

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -15f, 15f);

            //pitch = 15f;
            fpsCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            fpsCamera.transform.rotation = Quaternion.Euler(pitch, goose.transform.eulerAngles.y, 0);


        }

        private void HideGooseBody()
        {
            if (gooseMesh != null)
            {
                foreach (Transform child in gooseMesh.transform)
                {
                    if (child.gameObject.name != "HeldItem")
                    {
                        SkinnedMeshRenderer renderer = child.GetComponent<SkinnedMeshRenderer>();
                        if (renderer != null)
                        {
                            renderer.enabled = false;
                        }
                    }
                }
            }
        }

        private void ShowGooseBody()
        {
            if (gooseMesh != null)
            {
                foreach (Transform child in gooseMesh.transform)
                {
                    SkinnedMeshRenderer renderer = child.GetComponent<SkinnedMeshRenderer>();
                    if (renderer != null)
                    {
                        renderer.enabled = true;
                    }
                }
            }
        }

    }
}
