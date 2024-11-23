using MelonLoader;
using System;
using UnityEngine;

namespace HonkMenu
{
    [AddComponentMenu("Camera/Simple Smooth Mouse Look")]
    public class MagicCamPlugin : MonoBehaviour
    {
        public static MagicCamPlugin Instance;

        public float Speed = 0.1f;
        public float TimeSpeed = 0.008333334f;
        public bool IsEnabled;
        public bool IsGUIVisible = true;

        private Camera PrevCamera;
        private Camera _FreeCamera;
        private SimpleSmoothMouseLook _MouseLook;
        private GameObject _goose;

        public float SpeedF => Time.unscaledDeltaTime / TimeSpeed;

        public Camera FreeCamera
        {
            get
            {
                if (_FreeCamera != null)
                    return _FreeCamera;

                _FreeCamera = new GameObject("HonkMenu MAGIC CAMERA™").AddComponent<Camera>();
                _FreeCamera.tag = "MainCamera";
                _FreeCamera.enabled = false;
                _FreeCamera.nearClipPlane = 0.3f;
                _FreeCamera.farClipPlane = 4000f;
                _MouseLook = _FreeCamera.gameObject.AddComponent<SimpleSmoothMouseLook>();
                return _FreeCamera;
            }
        }

        public SimpleSmoothMouseLook MouseLook
        {
            get
            {
                if (_MouseLook == null)
                {
                    _MouseLook = FreeCamera.GetComponent<SimpleSmoothMouseLook>();
                }
                return _MouseLook;
            }
        }
        public void Awake()
        {
            Instance = this;
            _goose = GameObject.Find("Goose");
            MelonLogger.Msg("MagicCamPlugin Awake called");
            MelonLogger.Msg("MagicCamPlugin Awake called - Active in scene: " + gameObject.activeInHierarchy);
        }

        public static event Action OnUpdate;

        public void Update()
        {
            OnUpdate?.Invoke();

            if (Input.GetKeyDown(KeyCode.O))
            {
                MelonLogger.Msg("O key pressed - toggling MagicCam");

                IsEnabled = !IsEnabled;
                if (IsEnabled)
                {
                    EnableFreeCam();
                    MelonLogger.Msg("MagicCam Enabled");
                }
                else
                {
                    DisableFreeCam();
                    MelonLogger.Msg("MagicCam Disabled");
                }
            }

            if (IsEnabled)
            {
                HandleFreeCamControls();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                IsGUIVisible = !IsGUIVisible;
            }
        }

        public static void Init()
        {
            GameObject obj = new GameObject("MagicCamPlugin");
            obj.AddComponent<MagicCamPlugin>();
            MelonLogger.Msg("MagicCamPlugin initialized");
        }

        private void DisableFreeCam()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FreeCamera.enabled = false;

            if (PrevCamera != null)
            {
                PrevCamera.enabled = true;
            }
        }

        private void EnableFreeCam()
        {
            _goose = GameObject.Find("Goose");
            Time.timeScale = 0f;
            QualitySettings.lodBias = 1f;
            QualitySettings.maximumLODLevel = 0;
            PrevCamera = Camera.main;

            if (PrevCamera != null)
            {
                FreeCamera.transform.position = PrevCamera.transform.position;
                FreeCamera.transform.eulerAngles = new Vector3(0f, PrevCamera.transform.eulerAngles.y, 0f);
                MouseLook.targetDirection = PrevCamera.transform.rotation.eulerAngles;
                FreeCamera.transform.position = _goose.transform.position;
                MouseLook.mouseAbsolute = Vector2.zero;
                FreeCamera.fieldOfView = PrevCamera.fieldOfView;

                if (FreeCamera.fieldOfView < 10f)
                {
                    FreeCamera.fieldOfView = 75f;
                }

                PrevCamera.enabled = false;
            }

            FreeCamera.enabled = true;
        }

        private void HandleFreeCamControls()
        {
            MouseLook.enabled = IsEnabled;

            if (!IsEnabled) return;

            FreeCamera.enabled = true;
            Transform camTransform = FreeCamera.transform;

            if (Input.GetKeyDown(KeyCode.Alpha9) && Speed <= 0.40f)
            {
                Speed += 0.01f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && Speed >= 0f)
            {
                Speed -= 0.01f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Speed = 0f;
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Speed = 0.1f;
                Time.timeScale = 0f;
            }
            float adjustedSpeed = Speed;
            if (ModInput.GetButton("FreeCam Run"))
            {
                adjustedSpeed *= 4f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _goose.transform.position = _FreeCamera.transform.position;
                //_goose.transform.position = _gooseB.transform.position;
            }
            Vector3 movement = Vector3.zero;
            movement += camTransform.forward * ModInput.GetAxis("Vertical");
            movement += new Vector3(Mathf.Sin((camTransform.eulerAngles.y + 90f) / 180f * Mathf.PI), 0f, Mathf.Cos((camTransform.eulerAngles.y + 90f) / 180f * Mathf.PI)) * ModInput.GetAxis("Horizontal");

            if (movement != Vector3.zero)
            {
                movement.Normalize();
                camTransform.position += movement * adjustedSpeed * SpeedF;
            }

            camTransform.position += Vector3.up * ModInput.GetAxis("FreeCam Y Movement") * adjustedSpeed * SpeedF;

            Time.timeScale = Mathf.Clamp(Time.timeScale + ModInput.GetAxis("FreeCam Game Speed") * ((Time.timeScale < 0.25f) ? 0.01f : (Time.timeScale < 2f) ? 0.05f : (Time.timeScale < 8f) ? 0.5f : 1f), 0f, 100f);

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Time.timeScale = 0f;
            }
        }

        public void LateUpdate()
        {
            ModInput.LateUpdate();
        }

        public void DrawGUI()
        {
            if (IsGUIVisible && IsEnabled)
            {
                // Adjust these values as needed
                int bottomOffset = 50;  // Padding from the bottom
                int rightOffset = 0;   // Padding from the right
                int labelWidth = 250;   // Width of the label
                int labelHeight = 25;   // Height of each label

                // Calculate starting position for the bottom-right corner
                float xPos = Screen.width - labelWidth - rightOffset;
                float yPos = Screen.height - bottomOffset - labelHeight;

                // Display the position, rotation, and speed labels
                GUI.Label(new Rect(xPos, yPos, labelWidth, labelHeight), $"Speed: {Speed:F2}");
                yPos -= labelHeight;
                GUI.Label(new Rect(xPos, yPos, labelWidth, labelHeight), $"Rotation: {FreeCamera.transform.eulerAngles}");
                yPos -= labelHeight;
                GUI.Label(new Rect(xPos, yPos, labelWidth, labelHeight), $"Position: {FreeCamera.transform.position}");
            }
        }
        private void OnGUI()
        {
            if (IsEnabled)
            {
                DrawGUI();
            }
        }

    }
}
