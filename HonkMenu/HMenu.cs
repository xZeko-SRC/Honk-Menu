using UnityEngine;
using MelonLoader;
using HonkMenu;

public class HMenu : MonoBehaviour
{
    private Rect _main, _speedrun, _goose, _spawn, _create, _fun, _stele, _mtele, _atele, _ftele, _keys, _expe, _gospawn, _task, _clone, _credit;
    public static bool CreditVisible, KeysVisible, VisualVisible, GooseVisible, TaskVisible, GOVisible, SpawnVisible, CloneVisible, TeleMainVisible, RenderVisible, ExVisible, WorldVisible, SpeedVisible, AreasVisible, FunVisible;
    public bool Visible = true;
    public GameObject goose;
    private FunStuff funStuffInstance = new FunStuff();

    public void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Visible = !Visible;
        }
    }

    private void Update()
    {
        ToggleMenu();
    }
    public void OnGUI()
    {
        if (!Visible)
        {
            return;
        }
        funStuffInstance?.DrawAllHumansESP();

        //_main = GUILayout.Window(0, _main, new GUI.WindowFunction(Draw), "Honk Menu", GUILayout.Width(250f), GUILayout.Height(150f));
        _main = GUILayout.Window(0, _main, new GUI.WindowFunction(Draw), "", GUILayout.Width(250f), GUILayout.Height(150f)); // Empty string for window title

        if (VisualVisible)
        {
            _speedrun = GUILayout.Window(1, _speedrun, new GUI.WindowFunction(DrawSpeedrun), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (CreditVisible)
        {
            _credit = GUILayout.Window(15, _credit, new GUI.WindowFunction(DrawCredits), "", GUILayout.Width(250f), GUILayout.Height(400f));
        }
        if (GooseVisible)
        {
            _goose = GUILayout.Window(2, _goose, new GUI.WindowFunction(DrawGoose), "", GUILayout.Width(250f), GUILayout.Height(500f));
        }
        if (SpawnVisible)
        {
            _spawn = GUILayout.Window(3, _spawn, new GUI.WindowFunction(DrawSpawn), "", GUILayout.Width(250f), GUILayout.Height(500f));
        }
        if (RenderVisible)
        {
            _create = GUILayout.Window(4, _create, new GUI.WindowFunction(DrawCreate), "",   GUILayout.Width(250f), GUILayout.Height(550f));
        }
        if (WorldVisible)
        {
            _fun = GUILayout.Window(5, _fun, new GUI.WindowFunction(DrawFun), "", GUILayout.Width(250f), GUILayout.Height(200f));
        }
        if (SpeedVisible)
        {
            _stele = GUILayout.Window(6, _stele, new GUI.WindowFunction(DrawSpeedTele), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (TeleMainVisible)
        {
            _mtele = GUILayout.Window(7, _mtele, new GUI.WindowFunction(DrawMainTele), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (AreasVisible)
        {
            _atele = GUILayout.Window(8, _atele, new GUI.WindowFunction(DrawAreaTele), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (FunVisible)
        {
            _ftele = GUILayout.Window(9, _ftele, new GUI.WindowFunction(DrawFunTele), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (KeysVisible)
        {
            _keys = GUILayout.Window(10, _keys, new GUI.WindowFunction(DrawKeys), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (ExVisible)
        {
            _expe = GUILayout.Window(11, _expe, new GUI.WindowFunction(Experiment), "", GUILayout.Width(250f), GUILayout.Height(150f));
        }
        if (GOVisible)
        {
            _gospawn = GUILayout.Window(12, _gospawn, new GUI.WindowFunction(GoSpawn), "", GUILayout.Width(250f), GUILayout.Height(450f));
        }
        if (TaskVisible)
        {
            _task = GUILayout.Window(13, _task, new GUI.WindowFunction(TaskItem), "", GUILayout.Width(250f), GUILayout.Height(500f));
        }
        if (CloneVisible)
        {
            _clone = GUILayout.Window(14, _clone, new GUI.WindowFunction(Cloning), "", GUILayout.Width(250f), GUILayout.Height(450f));
        }
    }

    public void DrawCredits(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;
        GUILayout.Label("<color=#72d4d3>Credits</color> <color=#FFFF00>Go</color> <color=#702963>To</color>", titleStyle);
        GUILayout.Label("BradCubed (If you still go by that name) for creating the first mod for Goose and getting myself involved and taking over the mod", textStyle);
        GUILayout.Label("DevChagrins for additional help and code and the start of Multiplayer Goose before it was a thing!", textStyle);
        GUILayout.Label("SRC Goose Discord, friendly bunch! Sorry I have not been around as much", textStyle);
        GUILayout.Label("And to others, sorry if I have missed you, its been a long time..", textStyle);
        GUI.DragWindow();
    }
    public void Draw(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20; 
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 12;

        GUILayout.Label("<color=#72d4d3>Honk</color> <color=#FFFF00>Menu</color>", titleStyle);

        GUILayout.Label("   Open and Close Mod Menu: M", textStyle);
        GUILayout.Label("Toggle collision detector and info: Rightshift", textStyle);
        GUILayout.Space(+10f);

        if (GUILayout.Button("<color=#72d4d3>Info + Shortcut Keys</color>", buttonStyle))
        {
            _keys.x = _main.width + 20f;
            //_keys.y = _create.width + 80f;
            KeysVisible = !KeysVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Fun Stuff</color>", buttonStyle))
        {
            _fun.x = _main.width + 20f;
            //_keys.y = _create.width + 80f;
            WorldVisible = !WorldVisible;
        }
        GUILayout.Label("--------------------------------------------------------");
        //if (GUILayout.Button("<color=#72d4d3>Cheats</color>", buttonStyle))
        //{
            //_credit.x = _main.width + 70f;
            //CreditVisible = !CreditVisible;
        //}
        if (GUILayout.Button("<color=#72d4d3>Speedrun</color>", buttonStyle))
        {
            _speedrun.x = _keys.width + 280f;
            VisualVisible = !VisualVisible;
        }

        if (GUILayout.Button("<color=#72d4d3>Goose/NPC</color>", buttonStyle))
        {
            _goose.x = _speedrun.width + 530f;
            GooseVisible = !GooseVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Complete Tasks</color>", buttonStyle))
        {
            _task.x = _create.width + 680f;
            //_expe.y = _spawn.width + 80f;
            TaskVisible = !TaskVisible;
        }
        //GUILayout.Label("<color=#ff0000>THIS WILL UNLOCK STEAM ACHIEVEMENTS</color>", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Spawn Items</color>", buttonStyle))
        {
            _spawn.x = _goose.width + 270f;
            _spawn.y = _goose.width + 110f;
            SpawnVisible = !SpawnVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Clone People/Goose</color>", buttonStyle))
        {
            _clone.x = _main.width + 120f;
            CloneVisible = !CloneVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Teleport</color>", buttonStyle))
        {
            _mtele.x = _keys.width + 240f;
            _mtele.y = _main.width + 200f;
            TeleMainVisible = !TeleMainVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Render/Create</color>", buttonStyle))
        {
            _create.x = _mtele.width + 620f;
            _create.y = _goose.width + 110f;
            RenderVisible = !RenderVisible;
        }
        GUILayout.Label("--------------------------------------------------------");
        if (GUILayout.Button("<color=#72d4d3>Experimental</color>", buttonStyle))
        {
            _expe.x = _create.width + 680f;
            //_expe.y = _spawn.width + 80f;
            ExVisible = !ExVisible;
        }


        //GUILayout.Space(10f);
        GUILayout.Label("--------------------------------------------------------");

        GUILayout.Label("Click below for", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Honk Menu Github Page</color>", buttonStyle))
        {
            Application.OpenURL("https://github.com/xZeko-SRC/Honk-Menu");
        }
        GUILayout.Label("                Created by xZeKo"); 
        GUILayout.BeginHorizontal();
        float buttonWidth = (_main.width - 50) / 3;  // Adjusting for spacing

        if (GUILayout.Button("<color=#72d4d3>Discord</color>", buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(30)))
        {
            Application.OpenURL("https://discord.gg/mh47vVq");
        }
        if (GUILayout.Button("<color=#72d4d3>Random!</color>", buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(30)))
        {
            Application.OpenURL("https://github.com/xZeko-SRC/UGGRandomizer");
        }
        if (GUILayout.Button("<color=#72d4d3>Credits</color>", buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(30)))
        {
            _credit.x = _main.width + 70f;
            CreditVisible = !CreditVisible;
            Debug.Log("Right clicked!");
        }
        GUILayout.EndHorizontal();
        GUI.DragWindow();
    }

    public void DrawSpawn(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 12;
        GUILayout.Label("<color=#72d4d3>Spawn</color> <color=#FFFF00>Items</color>", titleStyle);
        GUILayout.Label("Teleport any item to you", textStyle);
        SpawnItems.ItemSpawner();
        GUI.DragWindow();
    }

    public void DrawFun(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 12;
        GUILayout.Label("<color=#72d4d3>Fun</color> <color=#FFFF00>Stuff</color>", titleStyle);
        GUILayout.Space(+5f);
        if (GUILayout.Button(FunStuff.jumpEnabled ? "<color=#FFFF00>Disable Jump</color>" : "<color=#72d4d3>Enable Jump</color>", buttonStyle))
        {
            FunStuff.jumpEnabled = !FunStuff.jumpEnabled;
        }
        if (GUILayout.Button(FunStuff.floatingEnabled ? "<color=#FFFF00>Disable Floating</color>" : "<color=#72d4d3>Enable Floating</color>", buttonStyle))
        {
            if (FunStuff.floatingEnabled)
                FunStuff.DisableFloating();
            else
                FunStuff.EnableFloating();
        }
        if (GUILayout.Button(FunStuff.colorChangeEnabled ? "<color=#FFFF00>Disable Color Change</color>" : "<color=#72d4d3>Enable Color Change</color>", buttonStyle))
        {
            if (FunStuff.colorChangeEnabled)
                FunStuff.DisableColorChange();
            else
                FunStuff.EnableColorChange();
        }
        if (GUILayout.Button(HMain._noclip ? "<color=#FFFF00>Disable Noclip</color>" : "<color=#72d4d3>Enable Noclip</color>", buttonStyle))
        {
            HMain._noclip = !HMain._noclip;
        }
        if (GUILayout.Button(FunStuff.ShooReadinessEnabled ? "<color=#FFFF00>Enable Shoo</color>" : "<color=#72d4d3>Disable Shoo</color>", buttonStyle))
        {
            FunStuff.ShooReadinessEnabled = !FunStuff.ShooReadinessEnabled;
            FunStuff.SetShooReadinessForAllHumans(FunStuff.ShooReadinessEnabled);
        }
        if (GUILayout.Button(FunStuff.isGooseBSkinActive ? "<color=#FFFF00>Enable P1 Goose</color>" : "<color=#72d4d3>Enable P2 Goose</color>", buttonStyle))
        {
            FunStuff.ToggleGooseSkin();
        }
        GUI.DragWindow();
    }


    public void DrawSpeedrun(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;
        GUILayout.Label("<color=#72d4d3>Speedrun</color> <color=#FFFF00>Tools</color>", titleStyle);
        GUILayout.Space(5f);
        GUILayout.Label("To remove/restore all AI humans: K", textStyle);
        GUILayout.Space(5f);
        if (GUILayout.Button("<color=#72d4d3>Speedrun Teleports</color>", buttonStyle))
        {
            _stele.x = _main.width + 20f;
            SpeedVisible = !SpeedVisible;
        }
        GUILayout.Label("------------------------------------------------------", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Save Location</color>", buttonStyle))
        {
            SpeedrunItems.SaveLocation();
        }
        GUILayout.Space(5f);
        if (GUILayout.Button("<color=#72d4d3>Load Location</color>", buttonStyle))
        {
            SpeedrunItems.LoadLocation();
        }
        GUILayout.Space(5f);
        GUILayout.Label("------------------------------------------------------", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Spawn Bell</color>", buttonStyle))
        {
            SpeedrunItems.SpawnBell();
        }
        GUILayout.Space(5f);

        if (GUILayout.Button("<color=#72d4d3>Spawn Boot</color>", buttonStyle))
        {
            SpeedrunItems.SpawnBoot();
        }
        GUILayout.Space(5f);

        if (GUILayout.Button("<color=#72d4d3>Spawn Timmy</color>", buttonStyle))
        {
            SpeedrunItems.SpawnChild();
        }
        GUI.DragWindow();
    }
    public void DrawGoose(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;
        GUILayout.Label("<color=#72d4d3>Goose/NPC</color> <color=#FFFF00>Modifiers:</color>", titleStyle);
        //GUILayout.Label("Goose/NPC Modifiers:");
        GUILayout.Space(+5f);
        GUILayout.Label("Speed Up: Plus Button (+)", textStyle);
        GUILayout.Label("Speed Down: Minus Button (-)", textStyle);
        GUILayout.Label("------------------------------------------------------", textStyle);
        GooseModifier.ResizingBig();
        GooseModifier.ResizingSmall();

        GUI.DragWindow();
    }
    public static bool gooseclip = false;
    public void DrawSpeedTele(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUILayout.Label("<color=#72d4d3>Clip/Trick</color> <color=#FFFF00>Teleports</color>", titleStyle);
        GUILayout.Space(+5f);

        if (GUILayout.Button("<color=#72d4d3>Geesus Clip</color>", buttonStyle))
        {
            TeleportGoose.GeesusClip();
        }
        if (GUILayout.Button("<color=#72d4d3>Pub Clip</color>", buttonStyle))
        {
            TeleportGoose.PubClip();
        }
        if (GUILayout.Button("<color=#72d4d3>Blink Clip</color>", buttonStyle))
        {
            TeleportGoose.BlinkClip();
        }
        if (GUILayout.Button("<color=#72d4d3>Tower Climb</color>", buttonStyle))
        {
            TeleportGoose.TowerClimb();
        }
        if (GUILayout.Button("<color=#72d4d3>Bridge Clip</color>", buttonStyle))
        {
            TeleportGoose.BridgeClip();
        }
        if (GUILayout.Button("<color=#72d4d3>House Clip</color>", buttonStyle))
        {
            TeleportGoose.HouseClip();
        }

        GUI.DragWindow();
    }

    public void DrawCreate(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;

        GUILayout.Label("<color=#72d4d3>Render +</color> <color=#FFFF00>Create</color>", titleStyle);
        GUILayout.Label("Toggle Tracking for NPCs", textStyle);
        funStuffInstance?.DrawTrackingButtons();
        funStuffInstance?.DrawAllHumansESP();
        GUILayout.Label("--------------------------------------------------------");
        if (GUILayout.Button("<color=#72d4d3>Render Wireframe</color>", buttonStyle))
        {
            CreateItems.WireFrame();
        }
        GUILayout.Label("------------------------------------------------------", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Create a Cube</color>", buttonStyle))
        {
            CreateItems.CreateCube();
        }
        if (GUILayout.Button("<color=#72d4d3>Create a Capsule</color>", buttonStyle))
        {
            CreateItems.CreateCapsule();
        }
        if (GUILayout.Button("<color=#72d4d3>Create a Sphere</color>", buttonStyle))
        {
            CreateItems.CreateSphere();
        }
        if (GUILayout.Button("<color=#72d4d3>Create a Cylinder</color>", buttonStyle))
        {
            CreateItems.CreateCylinder();
        }
        GUILayout.Label("------------------------------------------------------", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Remove a Cube</color>", buttonStyle))
        {
            CreateItems.DestroyCube();
        }
        if (GUILayout.Button("<color=#72d4d3>Remove a Capsule</color>", buttonStyle))
        {
            CreateItems.DestroyCapsule();
        }
        if (GUILayout.Button("<color=#72d4d3>Remove a Sphere</color>", buttonStyle))
        {
            CreateItems.DestroySphere();
        }
        if (GUILayout.Button("<color=#72d4d3>Remove a Cylinder</color>", buttonStyle))
        {
            CreateItems.DestroyCylinder();
        }

        GUI.DragWindow();
    }


    public void DrawMainTele(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;
        GUILayout.Label("<color=#72d4d3>Tele</color><color=#FFFF00>ports</color>", titleStyle);
        GUILayout.Label("Teleport to certain places on the map.", textStyle);
        GUILayout.Space(+5f);
        //GUILayout.Label("Blink: Ctrl + E or LB + RB + X", textStyle);
        GUILayout.Space(+10f);
        if (GUILayout.Button("<color=#72d4d3>Speedrun Teleports</color>", buttonStyle))
        {
            _stele.x = _main.width + 20f;
            SpeedVisible = !SpeedVisible;
        }
        GUILayout.Space(+5f);
        if (GUILayout.Button("<color=#72d4d3>Area Teleports</color>", buttonStyle))
        {
            _atele.x = _main.width + 280f;
            AreasVisible = !AreasVisible;
        }
        GUILayout.Space(+5f);
        if (GUILayout.Button("<color=#72d4d3>Fun/OOB Teleports</color>", buttonStyle))
        {
            _ftele.x = _main.width + 540f;
            FunVisible = !FunVisible;
        }
        GUILayout.Space(+5f);
        GUILayout.Label("------------------------------------------------------", textStyle);
        GUILayout.Label("Teleport Geese Together", textStyle);
        if (GUILayout.Button("<color=#72d4d3>Teleport Player 1 to Player 2</color>", buttonStyle))
        {
              GooseModifier.GoosetoGeese();
        }
        if (GUILayout.Button("<color=#72d4d3>Teleport Player 2 to Player 1</color>", buttonStyle))
        {
             GooseModifier.GeesetoGoose();
        }
        GUI.DragWindow();
    }
    public void DrawAreaTele(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUILayout.Label("<color=#72d4d3>Area</color> <color=#FFFF00>Teleports</color>", titleStyle);
        GUILayout.Space(+5f);

        if (GUILayout.Button("<color=#72d4d3>Starting Area</color>", buttonStyle))
        {
             TeleportGoose.StartArea();
        }
        if (GUILayout.Button("<color=#72d4d3>Statue/Bench</color>", buttonStyle))
        {
              TeleportGoose.Statue();
        }
        if (GUILayout.Button("<color=#72d4d3>Garden</color>", buttonStyle))
        {
             TeleportGoose.Garden();
        }
        if (GUILayout.Button("<color=#72d4d3>High Street</color>", buttonStyle))
        {
             TeleportGoose.HighStreet();
        }
        if (GUILayout.Button("<color=#72d4d3>Back Gardens</color>", buttonStyle))
        {
              TeleportGoose.BackGardens();
        }
        if (GUILayout.Button("<color=#72d4d3>Pink House</color>", buttonStyle))
        {
            TeleportGoose.PinkHouse();
        }
        if (GUILayout.Button("<color=#72d4d3>Pub</color>", buttonStyle))
        {
              TeleportGoose.PubArea();
        }
        if (GUILayout.Button("<color=#72d4d3>Mini Village</color>", buttonStyle))
        {
             TeleportGoose.MiniVillage();
        }

        GUI.DragWindow();
    }
    public void DrawFunTele(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUILayout.Label("<color=#72d4d3>Fun/OOB</color> <color=#FFFF00>Teleports</color>", titleStyle);
        GUILayout.Space(+5f);

        if (GUILayout.Button("<color=#72d4d3>Above the Back of Pub</color>", buttonStyle))
        {
             TeleportGoose.BackTopPub();
        }
        if (GUILayout.Button("<color=#72d4d3>Back Garden's Skybox</color>", buttonStyle))
        {
             TeleportGoose.BGSkybox();
        }
        if (GUILayout.Button("<color=#72d4d3>Above the Hole by Well</color>", buttonStyle))
        {
             TeleportGoose.AboveWell();
        }
        if (GUILayout.Button("<color=#72d4d3>Above Pink House</color>", buttonStyle))
        {
             TeleportGoose.AbovePinkHouse();
        }
        if (GUILayout.Button("<color=#72d4d3>By Picnic Area</color>", buttonStyle))
        {
             TeleportGoose.PicnicArea();
        }
        if (GUILayout.Button("<color=#72d4d3>Behind Bell Pit</color>", buttonStyle))
        {
             TeleportGoose.BehindBellPit();
        }
        if (GUILayout.Button("<color=#72d4d3>Other Side of Mini Village</color>", buttonStyle))
        {
             TeleportGoose.OtherMiniVillage();
        }
        if (GUILayout.Button("<color=#72d4d3>Middle Threshold of Map</color>", buttonStyle))
        {
            TeleportGoose.MiddleMap();
        }
        if (GUILayout.Button("<color=#72d4d3>Above Bell Pit Shorter part</color>", buttonStyle))
        {
            TeleportGoose.AboveBellPit();
        }

        GUI.DragWindow();
    }

    public void DrawKeys(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 13;
        GUILayout.Label("<color=#72d4d3>Shortcut</color> <color=#FFFF00>Keys</color>", titleStyle);
        GUILayout.Label("------------------------------------------------------", textStyle);
        GUILayout.Label("Welcome to Honk Menu", textStyle);
        GUILayout.Label("This is the ported version for <color=#a0e8a0>MelonLoader</color>.", textStyle);
        GUILayout.Label("Here you will find the shortcut keys", textStyle);
        //GUILayout.Label("Some older shortcut keys have been removed for buttons.", textStyle);
        GUILayout.Space(+5f);
        GUILayout.Label("------------------------------------------------------", textStyle);
        //GUILayout.Label("Advanced Output: CTRL+Y", textStyle);
        GUILayout.Label("Remove Top Left and Middle Text: RIGHT-SHIFT", textStyle);
        GUILayout.Label("Save Goose Location: CTRL+S", textStyle);
        GUILayout.Label("Load Goose Location: CTRL+L", textStyle);
        GUILayout.Label("Remove All AI: K", textStyle);
        GUILayout.Label("Speed Game Up/Down: +/-", textStyle);
        GUILayout.Label("Spawn Bell: CTRL+T", textStyle);
        GUILayout.Label("Spawn Boot: CTRL+H", textStyle);
        GUILayout.Label("Spawn Timmy: CTRL+B", textStyle);
        GUILayout.Space(+5f);
        GUILayout.Label("------------------------------------------------------", textStyle);
        GUILayout.Label("NoClip: CTRL+N / Xbox: LB + RB + Y", textStyle);
        GUILayout.Label("W, A, S, D to move. Space to ascend, left CTRL to descend.", textStyle);
        GUILayout.Label("Xbox Controller: Left stick to move, A to Ascend Y to descend.", textStyle);
        GUILayout.Space(+5f);
        //GUILayout.Label("------------------------------------------------------", textStyle);
        //GUILayout.Label("No Clip doesn't have a button yet but shortcut keys work.", textStyle);
        GUI.DragWindow();
    }

    public void Experiment(int id)
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.label); buttonStyle.richText = true; buttonStyle.alignment = TextAnchor.UpperCenter; buttonStyle.fontSize = 15;
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 12;
        GUILayout.Label("<color=#72d4d3>Experi</color><color=#FFFF00>ments</color>", titleStyle);
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("This is Experimental");
        GUILayout.Label("Please be aware that some functions here MAY NOT work properly or as intended.");
        GUILayout.Label("Most parts have not been finished and will cause glitches/crashes.");
        GUILayout.Label("You will have to close Goose and restart. You have been warned.");
        GUILayout.Space(+5f);
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("Pressing 8 will activate First Person Mode.");
        GUILayout.Label("WASD to move and mouse to aim, Left shift to sprint.");
        GUILayout.Label("You can Honk and crouch and grab things like normal.");
        GUILayout.Label("Pressing 9 will deactivate First Person Mode.");
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("Random Bell Teleport CTRL + 2");
        GUILayout.Label("If you cannot find it then this marker will help CTRL + L");

        if (GUILayout.Button("<color=#72d4d3>GameObject item Spawner (Test)</color>", buttonStyle))
        {
            _gospawn.x = _main.width + 20f;
            GOVisible = !GOVisible;
        }
        if (GUILayout.Button("<color=#72d4d3>Toggle Player Icon</color>", buttonStyle))
        {
            FunStuff.TogglePlayerIcon(); // Call the method from FunStuff
        }

        GUILayout.Space(+5f);
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("Magic Free Cam by 0x0ade");
        GUILayout.Label("Free Cam: O");
        GUILayout.Label("Speed Up: 8, Speed Down: 9");
        GUILayout.Label("Freeze Camera: 1, Reset Camera: 0");
        GUILayout.Label("Teleport the Goose to Camera: 7");
        GUILayout.Label("(You will have to hold ALT after to get the mouse to work)");
        GUILayout.Label("Remove Top Left and Middle Text: RIGHT-SHIFT");
        GUI.DragWindow();
    }

    public void GoSpawn(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 12;
        GUILayout.Label("<color=#72d4d3>Spawn</color> <color=#FFFF00>Items</color>", titleStyle);
        GUILayout.Label("Spawn any item, Still WIP", textStyle);
        GUILayout.Label("The item will spawn at its original location", textStyle);
        GUILayout.Label("<color=#ff0000>Do NOT spawn item if attached to another item</color>", textStyle);
        GUILayout.Label("<color=#ff0000>I.E if gardner is wearing his hat</color>", textStyle);
        GOSpawner.GOSpawners();
        GUI.DragWindow();
    }

    public void Cloning(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUILayout.Label("<color=#72d4d3>Clone Human/</color> <color=#FFFF00>Goose</color>", titleStyle);
        CloneSpawn.CloneSpawner();
        GUI.DragWindow();
    }

    public void TaskItem(int id)
    {
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label); titleStyle.richText = true; titleStyle.alignment = TextAnchor.UpperCenter; titleStyle.fontSize = 20;
        GUIStyle textStyle = new GUIStyle(GUI.skin.label); textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter; textStyle.fontSize = 10;
        GUILayout.Label("<color=#72d4d3>Complete /</color> <color=#FFFF00>Tasks</color>", titleStyle);
        GUILayout.Label("Each button will complete the task", textStyle);
        GUILayout.Label("You will need to do the last part to get to the next area.", textStyle);
        GUILayout.Label("<color=#ff0000>THIS WILL UNLOCK STEAM ACHIEVEMENTS</color>", textStyle);
        ItemTask.CompleteTasks();
        GUI.DragWindow();
    }
}