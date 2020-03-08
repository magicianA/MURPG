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

public class GameEditorExtention
{
    //这里是一些编辑器方法
    /// <summary>
    /// 导航到系统配置文件
    /// </summary>
    [MenuItem("MatrixTool/System Config 系统配置 #F1")]
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

    //TODO
    [MenuItem("MatrixTool/Add Sub System 添加子系统")]
    public static void AddSubSystem()
    {
        //var bb= File.CreateText("Assets/Scripts/SubSystems/tesdst.txt");
        //bb.Write("asdas");
        //bb.Close();
        AssetDatabase.Refresh();
        throw new System.NotImplementedException();
    }
    [MenuItem("MatrixTool/Add Linker")]
    public static void AddLinker()
    {
        throw new System.NotImplementedException();
    }
    [MenuItem("MatrixTool/Add Operator")]
    public static void AddOperator()
    {
        throw new System.NotImplementedException();
    }
}