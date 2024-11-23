using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    class SpawnItems : MelonMod
    {
        private static int selectedItem;

        public static GameObject spawnItem, _goose;
        public static Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);
        public static Vector2 scrollPosition;

        public static void ItemSpawner()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;

            string[] allitems = Constants.allitems;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            for (int l = 0; l < allitems.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + allitems[l] + "</color>", buttonStyle, new GUILayoutOption[0]))
                {
                    selectedItem = l;
                    _goose = GameObject.Find("Goose");
                    spawnItem = GameObject.Find(allitems[selectedItem]);
                    spawnItem.gameObject.transform.position = _goose.transform.position;
                    // + _goose.gameObject.transform.up * 1f Goose Cannot reach
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
