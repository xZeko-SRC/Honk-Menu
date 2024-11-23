using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    class CloneSpawn : MelonMod
    {
        private static int selectedItem;

        public static GameObject spawnItem, _goose;
        public static Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);
        public static Vector2 scrollPosition, scrollPosition1;

        public static void CloneSpawner()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 13;
            GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 15;
            GUILayout.Label("Clone People and Goose", textStyle);
            string[] clonepeeps = new string[] { "Goose", "gardener", "wimp", "tvshop", "shopkeeper", "neighbourClean", "neighbourMessyFixed", "cook", "pub man", "pub woman", "oldMan", "gossip2", "gossip1" };
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            for (int l = 0; l < clonepeeps.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + clonepeeps[l] + "</color>", buttonStyle, new GUILayoutOption[0]))
                {
                    selectedItem = l;
                    _goose = GameObject.Find("Goose");
                    spawnItem = GameObject.Find(clonepeeps[selectedItem]);
                    //spawnItem.gameObject.transform.position = _goose.transform.position;
                    // + _goose.gameObject.transform.up * 1f Goose Cannot reach
                    spawnItem = GameObject.Instantiate(spawnItem.gameObject,
                        _goose.gameObject.transform.position + (_goose.gameObject.transform.forward * 1f),
                        _goose.gameObject.transform.rotation, _goose.gameObject.transform.parent);
                    //spawnItem.gameObject.name = cloned;
                }
            }
            GUILayout.EndScrollView();

            GUILayout.Label("------------------------------------------------------");
            GUILayout.Label("Destroy People and Goose", textStyle);
            string[] destroypeeps = new string[] { "Goose(Clone)", "gardener(Clone)", "wimp(Clone)", "tvshop(Clone)", "shopkeeper(Clone)", "neighbourClean(Clone)", "neighbourMessyFixed(Clone)", "cook(Clone)", "pub man(Clone)", "pub woman(Clone)", "oldMan(Clone)", "gossip2(Clone)", "gossip1(Clone)" };
            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, new GUILayoutOption[0]);
            for (int l = 0; l < destroypeeps.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + clonepeeps[l] + "</color>", buttonStyle, new GUILayoutOption[0]))
                {
                    selectedItem = l;
                    spawnItem = GameObject.Find(destroypeeps[selectedItem]);
                    GameObject.Destroy(spawnItem);
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
