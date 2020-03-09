using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using GameSystem;
using System.IO;

#region EnumMap Drawer Definition
[CustomPropertyDrawer(typeof(GameSceneMap), true)]
public class GameSceneMapDrawer : EnumMapDrawer<TheMatrix.GameScene> { }
[CustomPropertyDrawer(typeof(SoundClipMap), true)]
public class AudioClipMapDrawer : EnumMapDrawer<AudioSystem.Sound> { }
[CustomPropertyDrawer(typeof(InputKeyMap), true)]
public class InputKeyMapDrawer : EnumMapDrawer<InputSystem.InputKey> { }
#endregion

public class GameEditorExtention : EditorWindow
{
    //这里是一些编辑器方法
    [MenuItem("MatrixTool/Open Tool Window 打开工具箱 #F1")]
    public static void OpenToolWindow()
    {
        var comfirmWindow = EditorWindow.GetWindow<GameEditorExtention>("Minstreams工具箱");
    }
    /// <summary>
    /// 导航到系统配置文件
    /// </summary>
    [MenuItem("MatrixTool/System Config 系统配置 _F2")]
    public static void NavToSystemConfig()
    {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>("Assets/Resources/System/TheMatrixSetting.asset");
    }
    /// <summary>
    /// 测试当前场景
    /// </summary>
    [MenuItem("MatrixTool/Debug Current Scene 测试当前场景 #F5")]
    public static void DebugCurrent()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveOpenScenes();
        var sysScene = EditorSceneManager.OpenScene("Assets/Scenes/System.unity", OpenSceneMode.Additive);
        sysScene.GetRootGameObjects()[0].GetComponent<TheMatrix>().testAll = false;
        EditorApplication.isPlaying = true;
    }
    /// <summary>
    /// 测试全部场景
    /// </summary>
    [MenuItem("MatrixTool/Debug All Scenea 测试全部场景 _F5")]
    public static void DebugAll()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveOpenScenes();
        var sysScene = EditorSceneManager.OpenScene("Assets/Scenes/System.unity", OpenSceneMode.Single);
        sysScene.GetRootGameObjects()[0].GetComponent<TheMatrix>().testAll = true;
        foreach (string sceneName in TheMatrix.Setting.gameSceneMap.list)
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.AdditiveWithoutLoading);
        }
        EditorApplication.isPlaying = true;
    }
    /// <summary>
    /// 添加子系统
    /// </summary>
    public static void AddSubSystem(string name)
    {
        if (name == "") return;
        if (AssetDatabase.IsValidFolder("Assets/Scripts/SubSystems/" + name))
        {
            Debug.LogAssertion(name + " already Exists!");
            return;
        }
        AssetDatabase.CreateFolder("Assets/Scripts/SubSystems", name);
        //Setting-------------------------------
        var fSetting = File.CreateText("Assets/Scripts/SubSystems/" + name + "/" + name + "Setting.cs");
        fSetting.Write(
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    namespace Setting
    {
        [CreateAssetMenu(fileName = " + "\"" + name + "Setting\", menuName = \"系统配置文件/" + name + "Setting\"" + @")]
        public class " + name + @"Setting : ScriptableObject
        {
            //data definition here
        }
    }
}");
        fSetting.Close();

        var fSystem = File.CreateText("Assets/Scripts/SubSystems/" + name + "/" + name + ".cs");
        fSystem.Write(
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Setting;

namespace GameSystem
{
    /// <summary>
    /// " + name + @"
    /// </summary>
    public class " + name + @" : SubSystem<" + name + @"Setting>
    {
        //Your code here


        [RuntimeInitializeOnLoadMethod]
        private static void RuntimeInit()
        {
            //用于控制Action初始化
            TheMatrix.onGameAwake += OnGameAwake;
            TheMatrix.onGameStart += OnGameStart;
        }
        private static void OnGameAwake()
        {
            //在进入游戏第一个场景时调用
        }
        private static void OnGameStart()
        {
            //在主场景游戏开始时和游戏重新开始时调用
        }


        //API---------------------------------
        //public static void SomeFunction(){}
    }
}
");
        fSystem.Close();

        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>("Assets/Scripts/SubSystems/" + name);
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Minstreams工具箱", name + " created!", "Cool~");
    }
    public void CreateSettingAsset()
    {
        if (subSystemName == "") return;
        if (!AssetDatabase.IsValidFolder("Assets/Scripts/SubSystems/" + subSystemName))
        {
            Debug.LogAssertion(subSystemName + " doesn't exist!");
            return;
        }
        if (File.Exists("Assets/Resources/System/" + subSystemName + "Setting.asset"))
        {
            Debug.LogAssertion(subSystemName + " Setting Asset already exist!");
            NavToSystemConfig();
            return;
        }
        Selection.selectionChanged += _CreateSettingAsset;
        NavToSystemConfig();
    }
    private void _CreateSettingAsset()
    {
        EditorApplication.ExecuteMenuItem("Assets/Create/系统配置文件/" + subSystemName + "Setting");
        EditorUtility.DisplayDialog("Minstreams工具箱", subSystemName + "  Setting Asset created!", "Cool~");
        Selection.selectionChanged -= _CreateSettingAsset;
    }

    /// <summary>
    /// 添加Linker
    /// </summary>
    public static void AddLinker(string name)
    {
        if (name == "") return;
        var f = File.CreateText("Assets/Scripts/Linker/" + name + ".cs");
        f.Write(
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    namespace Linker
    {
        [AddComponentMenu(" + "\"Linker/" + name + "\"" + @")]
        public class " + name + @" : MonoBehaviour
        {
            //Inner code here

            //Output
            public SimpleEvent output;

            //Input
            [ContextMenu(" + "\"Invoke\"" + @")]
            public void Invoke()
            {
                output?.Invoke();
            }
        }
    }
}");
        f.Close();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Minstreams工具箱", name + " created!", "Cool~");
    }
    /// <summary>
    /// 添加Operator
    /// </summary>
    public static void AddOperator(string name)
    {
        if (name == "") return;
        var f = File.CreateText("Assets/Scripts/Operator/" + name + ".cs");
        f.Write(
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    namespace Operator
    {
        [AddComponentMenu(" + "\"Operator/" + name + "\"" + @")]
        public class " + name + @" : MonoBehaviour
        {
            //Inner code here

            //Input
            //public void SomeFuntion(){}
        }
    }
}");
        f.Close();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Minstreams工具箱", name + " created!", "Cool~");
    }




    public enum EditorMode
    {
        AddSubSystem,
        AddLinker,
        AddOperator
    }
    private EditorMode editorMode;
    private string subSystemName = "";
    private string linkerName = "";
    private string operatorName = "";
    private GUIStyle headerStyle;
    private GUIStyle HeaderStyle
    {
        get
        {
            if (headerStyle == null)
            {
                headerStyle = new GUIStyle("OL Title");
                headerStyle.fontSize = 22;
                headerStyle.fixedHeight = 32;
            }
            return headerStyle;
        }
    }
    private GUIStyle btnStyle;
    private GUIStyle BtnStyle
    {
        get
        {
            if (btnStyle == null)
            {
                btnStyle = new GUIStyle("PreButton");
                btnStyle.alignment = TextAnchor.MiddleLeft;
                btnStyle.fontSize = 14;
                btnStyle.fixedHeight = 20;
                btnStyle.margin = new RectOffset(4, 4, 4, 4);
            }
            return btnStyle;
        }
    }

    private void SectionHeader(string title)
    {
        GUILayout.Label(title, HeaderStyle);
    }
    private string TextArea(string name, string target)
    {
        string result;
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(name, "InvisibleButton", GUILayout.ExpandWidth(false), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            result = GUILayout.TextField(target, 24);
        }
        GUILayout.EndHorizontal();
        return result;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("GameViewBackground");
        SectionHeader("数据导航");
        if (GUILayout.Button("系统配置", BtnStyle)) NavToSystemConfig();

        SectionHeader("自动测试");
        if (GUILayout.Button("测试全部场景", BtnStyle)) DebugAll();
        if (GUILayout.Button("测试当前场景", BtnStyle)) DebugCurrent();

        SectionHeader("自动化代码生成");
        GUILayout.BeginHorizontal("AnimationEventBackground", GUILayout.Height(EditorGUIUtility.singleLineHeight));
        {
            if (GUILayout.Button("SubSystem", editorMode == EditorMode.AddSubSystem ? "PreLabel" : "PreButton")) editorMode = EditorMode.AddSubSystem;
            if (GUILayout.Button("Linker", editorMode == EditorMode.AddLinker ? "PreLabel" : "PreButton")) editorMode = EditorMode.AddLinker;
            if (GUILayout.Button("Operator", editorMode == EditorMode.AddOperator ? "PreLabel" : "PreButton")) editorMode = EditorMode.AddOperator;
        }
        GUILayout.EndHorizontal();
        switch (editorMode)
        {
            case EditorMode.AddSubSystem:
                subSystemName = TextArea("Sub System Name", subSystemName);
                if (GUILayout.Button("Add", BtnStyle)) AddSubSystem(subSystemName);
                if (GUILayout.Button("Create Setting Asset", BtnStyle)) CreateSettingAsset();
                GUILayout.Label("由于实在无法把生成代码与生成配置文件功能做到一起，生成新系统时，请依次点这两个按钮。", "ColorPickerBackground");
                break;
            case EditorMode.AddLinker:
                linkerName = TextArea("Linker Name", linkerName);
                if (GUILayout.Button("Add", BtnStyle)) AddLinker(linkerName);
                break;
            case EditorMode.AddOperator:
                operatorName = TextArea("Operator Name", operatorName);
                if (GUILayout.Button("Add", BtnStyle)) AddOperator(operatorName);
                break;
        }
        GUILayout.EndVertical();
    }
}