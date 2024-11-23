using UnityEngine;
using MelonLoader;
using System.Collections.Generic;

namespace HonkMenu
{
    public class FunStuff
    {
        public static bool jumpEnabled = false, floatingEnabled = false, colorChangeEnabled = false, ShooReadinessEnabled = false, isGooseBSkinActive = false;
        private static float floatForce = 9.8f, jumpForce = 5f, groundCheckThreshold = 0.1f;
        private static Mesh originalMesh;
        private static SkinnedMeshRenderer gooseSkin;
        private static Material originalMaterial, modifiedMaterial;
        private static Material[] originalMaterials;
        public static GameObject _goose;
        private Dictionary<string, bool> npcTrackingStatus;
        private Vector2 scrollPosition;
        private static bool isIconEnabled = false;

        public static void TogglePlayerIcon()
        {
            // Find the "twoplayer_icon" GameObject
            GameObject iconObject = GameObject.Find("boilerRoom/GROUP_Geese/Goose/Body_Armature/twoplayer_icon");

            if (iconObject != null)
            {
                // Ensure the GameObject is active before accessing its components
                if (!iconObject.activeSelf)
                {
                    iconObject.SetActive(true);
                }

                // Access the SpriteRenderer component
                SpriteRenderer spriteRenderer = iconObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Toggle the enabled state of the SpriteRenderer
                    isIconEnabled = !isIconEnabled;
                    spriteRenderer.enabled = isIconEnabled;

                    // Log the current state
                    MelonLogger.Msg(isIconEnabled ? "twoplayer_icon enabled." : "twoplayer_icon disabled.");
                }
                else
                {
                    MelonLogger.Error("SpriteRenderer component not found on twoplayer_icon.");
                }
            }
            else
            {
                MelonLogger.Error("twoplayer_icon GameObject not found.");
            }
        }

        public static string[] shooPeeps = new string[]
        {
            "gardener brain", "wimp brain", "tvshop brain", "shopkeeper brain",
            "neighbourClean brain", "neighbourMessyFixed brain", "cook brain",
            "pub man brain", "pub woman brain", "oldMan brain", "gossip2 brain", "gossip1 brain"
        };
        public static void SetShooReadinessForAllHumans(bool enable)
        {
            float shooTime = enable ? 100000f : 0f;

            foreach (string brainName in shooPeeps)
            {
                GameObject brainObject = GameObject.Find(brainName);
                if (brainObject != null)
                {
                    Brain brain = brainObject.GetComponent<Brain>();
                    if (brain != null)
                    {
                        brain.body.shooReadiness.timeAllowedToShooAgain = shooTime;
                    }
                    else
                    {
                        MelonLogger.Msg($"Brain component not found on {brainName}");
                    }
                }
                else
                {
                    MelonLogger.Msg($"GameObject {brainName} not found in the scene");
                }
            }
        }

        public string[] trackpeeps = new string[]
        {
            "gardener brain", "wimp brain", "tvshop brain", "shopkeeper brain",
            "neighbourClean brain", "neighbourMessyFixed brain", "cook brain",
            "pub man brain", "pub woman brain", "oldMan brain", "gossip2 brain", "gossip1 brain"
        };

        public FunStuff()
        {
            npcTrackingStatus = new Dictionary<string, bool>();
            foreach (string npcName in trackpeeps)
            {
                npcTrackingStatus[npcName] = false; 
            }
        }

        public void DrawTrackingButtons()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
            //scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200), GUILayout.Width(200));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            foreach (string npcName in trackpeeps)
            {
                bool currentStatus = npcTrackingStatus[npcName];
                if (GUILayout.Button($"<color={(currentStatus ? "#FFFF00" : "#72d4d3")}>{npcName}</color>", buttonStyle))
                {
                    npcTrackingStatus[npcName] = !currentStatus;
                }
            }

            GUILayout.EndScrollView();
        }

        public void DrawAllHumansESP()
        {
            _goose = GameObject.Find("Goose");
            if (_goose == null) return;

            foreach (string npcName in trackpeeps)
            {
                if (npcTrackingStatus.TryGetValue(npcName, out bool isTracking) && isTracking)
                {
                    GameObject npc = GameObject.Find(npcName);
                    if (npc != null)
                    {
                        DrawIndividualESP(npc);
                    }
                }
            }
        }

        private void DrawIndividualESP(GameObject npc)
        {
            if (_goose == null || npc == null) return;

            Vector3 npcTorsoPos = npc.transform.position + Vector3.up * 1.0f;
            Vector3 npcHeadPos = npc.transform.position + Vector3.up * 2.0f;
            Vector3 gooseHeadPos = _goose.transform.position + Vector3.up * 0.3f;

            Vector3 screenTorsoPos = Camera.main.WorldToScreenPoint(npcTorsoPos);
            Vector3 screenHeadPos = Camera.main.WorldToScreenPoint(npcHeadPos);
            Vector3 screenGooseHeadPos = Camera.main.WorldToScreenPoint(gooseHeadPos);

            if (screenTorsoPos.z > 0)
            {
                Color originalColor = GUI.color;
                GUI.color = Color.red;

                float boxHeight = screenHeadPos.y - screenTorsoPos.y;
                float boxWidth = boxHeight / 2;
                float boxX = screenTorsoPos.x - (boxWidth / 2);
                float boxY = Screen.height - screenTorsoPos.y - (boxHeight / 2);

                HMain.Render.DrawBox(boxX, boxY, boxWidth, boxHeight, Color.red, 2f);

                HMain.Render.DrawLine(new Vector2(screenGooseHeadPos.x, Screen.height - screenGooseHeadPos.y),
                                new Vector2(screenTorsoPos.x, Screen.height - screenTorsoPos.y),
                                Color.red, 1f);

                float distance = Vector3.Distance(_goose.transform.position, npc.transform.position);
                GUI.Label(new Rect(boxX, boxY - 20, boxWidth, 20), $"{distance:F2}m",
                    new GUIStyle(GUI.skin.label) { fontSize = 15, alignment = TextAnchor.UpperCenter, normal = { textColor = Color.white } });

                GUI.color = originalColor;
            }
        }

        public static void TriggerJump()
        {
            if (!jumpEnabled || !IsGrounded()) return;

            GameObject goose = GameObject.Find("Goose");
            if (goose != null)
            {
                Rigidbody gooseRigidbody = goose.GetComponent<Rigidbody>();
                if (gooseRigidbody != null)
                {
                    gooseRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }

        private static bool IsGrounded()
        {
            GameObject goose = GameObject.Find("Goose");
            if (goose != null)
            {
                Rigidbody gooseRigidbody = goose.GetComponent<Rigidbody>();
                if (gooseRigidbody != null)
                {
                    return Mathf.Abs(gooseRigidbody.velocity.y) < groundCheckThreshold;
                }
            }
            return false;
        }


        public static void EnableFloating()
        {
            floatingEnabled = true;
        }

        public static void DisableFloating()
        {
            floatingEnabled = false;
        }

        public static void ApplyFloating()
        {
            if (floatingEnabled)
            {
                GameObject goose = GameObject.Find("Goose");
                if (goose != null)
                {
                    Rigidbody gooseRigidbody = goose.GetComponent<Rigidbody>();
                    if (gooseRigidbody != null)
                    {
                        gooseRigidbody.AddForce(Vector3.up * floatForce - Physics.gravity, ForceMode.Acceleration);
                    }
                }
            }
        }
        public static void EnableColorChange()
        {
            colorChangeEnabled = true;
            ApplyColorChange();
        }

        public static void DisableColorChange()
        {
            colorChangeEnabled = false;
            ApplyColorChange();
        }

        private static void ApplyColorChange()
        {
            GameObject goose = GameObject.Find("Goose");
            if (goose != null)
            {
                if (gooseSkin == null)
                {
                    gooseSkin = goose.GetComponentInChildren<SkinnedMeshRenderer>();
                    if (gooseSkin != null)
                    {
                        originalMaterial = gooseSkin.material;
                    }
                }

                if (gooseSkin != null)
                {
                    if (colorChangeEnabled)
                    {

                        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
                        modifiedMaterial = new Material(Shader.Find("VertexLit"));
                        modifiedMaterial.SetColor("_Color", randomColor);
                        modifiedMaterial.SetFloat("_EmissionColor", randomColor.r * 10f);
                        modifiedMaterial.EnableKeyword("_EMISSION");
                        gooseSkin.material = modifiedMaterial;
                    }
                    else
                    {
                        gooseSkin.material = originalMaterial;
                    }
                }
            }
        }
        public static void ToggleGooseSkin()
        {
            GameObject groupGeese = GameObject.Find("boilerRoom/GROUP_Geese");
            if (groupGeese != null)
            {
                GameObject goose = groupGeese.transform.Find("Goose")?.gameObject;
                GameObject gooseB = groupGeese.transform.Find("GooseB")?.gameObject;

                if (goose != null && gooseB != null)
                {
                    SkinnedMeshRenderer gooseRenderer = goose.GetComponentInChildren<SkinnedMeshRenderer>();
                    SkinnedMeshRenderer gooseBRenderer = gooseB.GetComponentInChildren<SkinnedMeshRenderer>();

                    if (gooseRenderer != null && gooseBRenderer != null)
                    {
                        if (isGooseBSkinActive)
                        {
                            gooseRenderer.sharedMesh = originalMesh;
                            gooseRenderer.materials = originalMaterials;
                            Debug.Log("Switched back to the original Goose skin.");
                        }
                        else
                        {
                            if (originalMesh == null && originalMaterials == null)
                            {
                                originalMesh = gooseRenderer.sharedMesh;
                                originalMaterials = gooseRenderer.materials;
                            }

                            gooseB.SetActive(true);
                            gooseRenderer.sharedMesh = gooseBRenderer.sharedMesh;
                            gooseRenderer.materials = gooseBRenderer.materials;
                            Debug.Log("Switched to the GooseB skin.");

                            gooseB.SetActive(false);
                        }

                        isGooseBSkinActive = !isGooseBSkinActive;
                    }
                    else
                    {
                        Debug.LogWarning("Could not find SkinnedMeshRenderer on Goose or GooseB.");
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find Goose or GooseB in boilerRoom/GROUP_Geese.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find boilerRoom/GROUP_Geese.");
            }
        }

    }
}

