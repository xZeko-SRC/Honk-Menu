using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class HMain : MelonMod
    {
        //-------------------------------------------------------------------- RandoBell, magic cam position 0,0,0. 
        private GameObject _goose, _bell, _timmy, _boot;
        //private GameObject lineObject, boxObject;
        public static int initializedScene;
        //float _deltaTime = 0.0f;
        public float smoothSpeed = 0.125f, _gooseMass;
        public GUIStyle _guiStyle = null;
        public Vector2 sensitivity = new Vector2(2.0f, 2.0f), smoothing = new Vector2(5.0f, 5.0f), clampInDegrees = new Vector2(100f, 100f);
        public static Vector3 _goosePos;
        private HMenu menu;
        public static bool _noclip = false;
        public bool _advancedOutput, _gooseInfo;
       //private bool isShooerEnabled = true, isSwapActive = false;
        private bool setSpeed = false;
        private CameraMode cameraMode;
        private FunStuff funStuff;
        private FunStuff funStuffInstance = new FunStuff();
        private RandoBellDrawer randoBellDrawer;
        //private Goose gooseInstance;
        //private GooseShooer gooseShooerInstance;

        public override void OnInitializeMelon()
        {
            menu = new GameObject("HMenu").AddComponent<HMenu>();
            MelonLogger.Msg("Mod loaded");
            randoBellDrawer = new RandoBellDrawer();
            GameObject obj = new GameObject("MagicCamPlugin");
            obj.AddComponent<MagicCamPlugin>();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 1)
            {
                GameSettings.currentSettings.allowCheats = true;
                GameSettings.currentSettings.teleportMustBePrimed = true;
            }
            if (buildIndex == 2)
            {
                    _goose = GameObject.Find("boilerRoom/GROUP_Geese/Goose");

                    if (_goose == null)
                    {
                        MelonLogger.Error("GooseObject not found!");
                    }
                    else
                    {
                        if (_goose.GetComponent<Transform>() != null)
                        {
                            _goose.AddComponent<ShowCollision>();
                            MelonLogger.Msg("ShowCollision component added to GooseObject");

                            _gooseMass = _goose.GetComponent<Rigidbody>().mass;

                            GameObject magicCamObject = new GameObject("MagicCamPluginObject");
                            MagicCamPlugin magicCamPlugin = magicCamObject.AddComponent<MagicCamPlugin>();
                            MagicCamPlugin.Instance = magicCamPlugin;

                            GameObject cameraModeObject = new GameObject("CameraModeObject");
                            cameraMode = cameraModeObject.AddComponent<CameraMode>();
                            GameObject.DontDestroyOnLoad(cameraModeObject);
                            MelonLogger.Msg("CameraMode initialized");
                            
                            funStuff = new FunStuff();
                            if (funStuff != null)
                            {
                                //funStuff.Start(); // Call FunStuff's Start method
                                MelonLogger.Msg("FunStuff started in OnSceneWasLoaded.");
                            }
                            else
                            {
                                MelonLogger.Msg("Error: FunStuff is null in OnSceneWasLoaded.");
                            }

                        }
                        else
                        {
                            MelonLogger.Error("GooseObject does not have a Transform component!");
                        }
                    }
                    this._bell = GameObject.Find("goldenBell");
                    _gooseMass = _goose.GetComponent<Rigidbody>().mass;
            }
            MelonLogger.Msg("OnSceneWasInitialized: " + buildIndex.ToString() + " | " + sceneName);
        }
        private void DrawGardenerESP()
        {
            GameObject gardener = GameObject.Find("gardener brain");
            if (_goose == null || gardener == null)
            {
                MelonLogger.Msg("Goose or Gardener object not found for ESP drawing.");
                return;
            }

            // Position box at the gardener's torso
            Vector3 gardenerTorsoPos = gardener.transform.position + Vector3.up * 1.0f;
            Vector3 gardenerHeadPos = gardener.transform.position + Vector3.up * 2.0f;
            Vector3 gooseHeadPos = _goose.transform.position + Vector3.up * 0.25f;  // Adjust for goose head height

            // Convert positions to screen space
            Vector3 screenTorsoPos = Camera.main.WorldToScreenPoint(gardenerTorsoPos);
            Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(gardenerHeadPos);
            Vector3 screenGooseHeadPos = Camera.main.WorldToScreenPoint(gooseHeadPos);

            if (screenTorsoPos.z > 0)  // Ensure gardener is in front of the camera
            {
                Color originalColor = GUI.color;
                GUI.color = Color.red;

                float boxHeight = screenHeadPos.y - screenTorsoPos.y;
                float boxWidth = boxHeight / 2;
                float boxX = screenTorsoPos.x - (boxWidth / 2);
                float boxY = Screen.height - screenTorsoPos.y - (boxHeight / 2);

                // Draw 2D box around the gardener’s torso
                Render.DrawBox(boxX, boxY, boxWidth, boxHeight, Color.red, 2f);

                // Draw line from goose head to center of gardener's torso box
                Render.DrawLine(new Vector2(screenGooseHeadPos.x, Screen.height - screenGooseHeadPos.y),
                                new Vector2(screenTorsoPos.x, Screen.height - screenTorsoPos.y),
                                Color.red, 1f);

                GUI.color = originalColor;
            }
            else
            {
                MelonLogger.Msg("Gardener is not visible on screen.");
            }
        }
        public static class Render
        {
            private static Texture2D lineTex;

            public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
            {
                if (!lineTex) lineTex = new Texture2D(1, 1);
                Color originalColor = GUI.color;
                GUI.color = color;

                Matrix4x4 matrix = GUI.matrix;
                float angle = Vector3.Angle(pointB - pointA, Vector2.right);
                if (pointA.y > pointB.y) angle = -angle;

                GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), pointA);
                GUIUtility.RotateAroundPivot(angle, pointA);
                GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
                GUI.matrix = matrix;
                GUI.color = originalColor;
            }

            public static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
            {
                Color originalColor = GUI.color;
                GUI.color = color;

                DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);  // Top
                DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);  // Left
                DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);  // Right
                DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);  // Bottom

                GUI.color = originalColor;
            }
        }
        //Behaviour.enabled
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                _gooseInfo = !_gooseInfo;
            }

            funStuffInstance?.DrawAllHumansESP();
            FunStuff.ApplyFloating();
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                randoBellDrawer.RandoBell();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            {
                MelonLogger.Msg("L pushed");
                randoBellDrawer.ToggleMarker();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
            {
                setSpeed = !setSpeed;
            }
            if (setSpeed)
            {
                Goose goose = GameObject.Find("Goose").GetComponent<Goose>();
                if (goose != null && goose.mover != null)
                {
                    goose.mover.currentSpeed = 5;
                }
            }
            // --------------------------------------------------------------------------------------------------------------------------
            if (FunStuff.jumpEnabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
            {
                FunStuff.TriggerJump();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E) || (Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5) && Input.GetKeyDown(KeyCode.JoystickButton2)))
            {
                _goose.transform.position += _goose.transform.forward * 3f;
                MelonLogger.Msg("Blinked Forward Boss!");
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                _goose = GameObject.Find("Goose");
                _bell = GameObject.Find("goldenBell");
                _bell.transform.position = _goose.transform.position;
                MelonLogger.Msg("Teleport the Bell to Goose Boss!");
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
            {
                _goose = GameObject.Find("Goose");
                _boot = GameObject.Find("boot");
                _boot.transform.position = this._goose.transform.position;
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
            {
                _goose = GameObject.Find("Goose");
                _timmy = GameObject.Find("MiniPerson Variant - child");
                _timmy.transform.position = this._goose.transform.position;
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            {
                _goose.transform.position = _goosePos;
                MelonLogger.Msg("Loaded the Goose Boss!");
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            {
                _goosePos = _goose.transform.position;
                MelonLogger.Msg("Saved the Goose Boss!");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                menu.ToggleMenu();
                MelonLogger.Msg("MMMMMMMMMM");
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N) || (Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5) && Input.GetKeyDown(KeyCode.JoystickButton3)))
            {
                _noclip = !_noclip;
            }
            if (_noclip)
            {
                if (_goose != null)
                {
                    Rigidbody gooseRb = _goose.GetComponent<Rigidbody>();
                    if (gooseRb != null)
                    {
                        gooseRb.isKinematic = true;
                        gooseRb.useGravity = false;
                        gooseRb.mass = 0f;
                    }
                    else
                    {
                        MelonLogger.Msg("Rigidbody component is null on goose during noclip.");
                    }

                    Collider[] components = _goose.GetComponents<Collider>();
                    foreach (Collider collider in components)
                    {
                        if (collider != null)
                        {
                            collider.enabled = false;
                        }
                    }

                    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
                {
                    _goose.transform.position += new Vector3(0f, 0.05f, 0f);
                }
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.JoystickButton3))
                {
                    _goose.transform.position += new Vector3(0f, -0.05f, 0f);
                }
                if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0f)
                {
                    _goose.transform.position += _goose.transform.forward * 0.05f;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0f)
                {
                    _goose.transform.position -= _goose.transform.forward * 0.05f;
                }
                if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0f)
                {
                    _goose.transform.position += _goose.transform.right * 0.05f;
                }
                if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0f)
                {
                    _goose.transform.position -= _goose.transform.right * 0.05f;
                }
                }
                else
                {
                    MelonLogger.Msg("Goose object is null in noclip logic.");
                }
            }
            else
            {
                if (_goose != null)
                {
                    Rigidbody gooseRb = _goose.GetComponent<Rigidbody>();
                    if (gooseRb != null)
                    {
                        gooseRb.isKinematic = false;
                        gooseRb.useGravity = true;
                        gooseRb.mass = _gooseMass;
                    }

                    Collider[] components2 = _goose.GetComponents<Collider>();
                    foreach (Collider collider2 in components2)
                    {
                        if (collider2 != null)
                        {
                            collider2.enabled = true;
                        }
                    }
                }
            }
        }

        public override void OnGUI()
        {
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
            {
                richText = true,
                alignment = TextAnchor.UpperRight,
                fontSize = 25
            };

            GUIStyle versionStyle = new GUIStyle(GUI.skin.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleRight,
                fontSize = 18,
                normal = { textColor = Color.black }
            };

            GUILayout.BeginArea(new Rect(Screen.width - 225, 10, 200, 50));
            GUILayout.Label("<color=#72d4d3>Honk</color> <color=#FFFF00>Menu</color>", titleStyle);
            GUILayout.Space(-10);
            GUILayout.Label("v1", versionStyle);
            GUILayout.EndArea();

            GameObject gooseObject = GameObject.Find("Goose");
            if (gooseObject != null)
            {
                Goose gooseComponent = gooseObject.GetComponent<Goose>();
                if (gooseComponent != null && gooseComponent.enabled)
                {
                    menu.OnGUI();
                }
            }

            if (_goose != null && !_gooseInfo)
            {
                float bottomOffset = 120;
                float spacing = 20;

                GUI.Label(new Rect(20, Screen.height - bottomOffset, 2000, 200), $"Position: {_goose.transform.position}");
                GUI.Label(new Rect(20, Screen.height - (bottomOffset - spacing), 2000, 200), $"Rotation: {_goose.transform.rotation}");
                GUI.Label(new Rect(20, Screen.height - (bottomOffset - 2 * spacing), 2000, 200), $"Velocity: {_goose.GetComponent<Rigidbody>().velocity}");
            }
        }

    }
}
