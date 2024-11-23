using UnityEngine;
using MelonLoader;

namespace HonkMenu
{
    class ItemTask : MelonMod
    {
        private static int selectedItem;
        public static Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);
        public static Vector2 scrollPosition;

        public static void CompleteTasks()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
            string[] taskstr = GooseTask.itemTasks;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            for (int l = 0; l < taskstr.Length; l++)
            {
                if (GUILayout.Button("<color=#72d4d3>" + taskstr[l] + "</color>", buttonStyle))
                {
                    selectedItem = l;
                    SwitchEventManager.TriggerEvent("award-" + taskstr[selectedItem]);
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
