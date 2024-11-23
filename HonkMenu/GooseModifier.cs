using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    public class GooseModifier : MelonMod
    {
        public static GameObject _goose, _gooseB;

        public static Vector2 scrollPosition, scrollPosition1;
        private static int selectedItem;
        public static GameObject spawnItem;
        public static void ResizingBig()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 13;
            GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 14;
            GUILayout.Label("Bigger Size", textStyle);
            string[] clonepeeps = new string[] { "Goose", "gardener", "wimp", "tvshop", "shopkeeper", "neighbourClean", "neighbourMessyFixed", "cook", "pub man", "pub woman", "oldMan", "gossip2", "gossip1" };
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            for (int l = 0; l < clonepeeps.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + clonepeeps[l] + "</color>", buttonStyle))
                {
                    selectedItem = l;
                    _goose = GameObject.Find("Goose");
                    spawnItem = GameObject.Find(clonepeeps[selectedItem]);
                    spawnItem.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                }
            }
            GUILayout.EndScrollView();
        }
        public static void ResizingSmall()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 13;
            GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 14;
            GUILayout.Label("------------------------------------------", textStyle);
            GUILayout.Label("Smaller Size", textStyle);
            string[] clonepeeps = new string[] { "Goose", "gardener", "wimp", "tvshop", "shopkeeper", "neighbourClean", "neighbourMessyFixed", "cook", "pub man", "pub woman", "oldMan", "gossip2", "gossip1" };
            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, buttonStyle);
            for (int l = 0; l < clonepeeps.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + clonepeeps[l] + "</color>", buttonStyle))
                {
                    selectedItem = l;
                    _goose = GameObject.Find("Goose");
                    spawnItem = GameObject.Find(clonepeeps[selectedItem]);
                    spawnItem.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                }
            }
            GUILayout.EndScrollView();
        }
        public static void GoosetoGeese()
        {
            _goose = GameObject.Find("Goose");
            _gooseB = GameObject.Find("GooseB");
            _goose.transform.position = _gooseB.transform.position;

        }
        public static void GeesetoGoose()
        {
            _goose = GameObject.Find("Goose");
            _gooseB = GameObject.Find("GooseB");
            _gooseB.transform.position = _goose.transform.position;
        }
    }
}
