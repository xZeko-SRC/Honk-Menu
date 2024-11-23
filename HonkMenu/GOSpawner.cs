using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class GOSpawner : MelonMod
    {
        private static int selectedItem;

        public static GameObject spawnItem, _goose;
        public static Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);
        public static Vector2 scrollPosition;

        public static void GOSpawners()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 13;
            GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 15;
            string[] allitems = Constants.allitems;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);

            for (int l = 0; l < allitems.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + allitems[l] + "</color>", buttonStyle, new GUILayoutOption[0]))
                {
                    selectedItem = l;
                    _goose = GameObject.Find("Goose");

                    if (_goose == null)
                    {
                        MelonLogger.Msg("Goose not found!");
                        return;
                    }

                    spawnItem = GameObject.Find(allitems[selectedItem]);

                    if (spawnItem == null)
                    {
                        MelonLogger.Msg($"Spawn item '{allitems[selectedItem]}' not found!");
                        return;
                    }

                    MelonLogger.Msg($"Goose Forward Direction: {_goose.transform.forward}");

                    Vector3 spawnPosition = _goose.transform.position + (_goose.transform.forward * 1f);
                    spawnPosition.y = _goose.transform.position.y;

                    GameObject newClone = GameObject.Instantiate(spawnItem, spawnPosition, _goose.transform.rotation);

                    newClone.transform.localScale = spawnItem.transform.localScale;

                    newClone.name = "Clone_" + selectedItem;

                    MelonLogger.Msg($"Instantiated {newClone.name} at position: {newClone.transform.position} with scale: {newClone.transform.localScale}");
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
