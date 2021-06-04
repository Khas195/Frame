using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
[InitializeOnLoadAttribute]
public static class GameMasterEditor
{


    [MenuItem("Game Master/Play from Master Scene")]
    static void PlayFromMasterScene()
    {
        if (EditorSceneManager.GetSceneByName("MasterScene").IsValid() == false)
        {
            AddMasterScene();
        }
        EditorApplication.EnterPlaymode();
    }

    [MenuItem("Game Master/Add Master Scene")]
    static void AddMasterScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/DevScenes/MasterScene.unity", OpenSceneMode.Additive);
    }
    [MenuItem("Game Master/ClearAll")]
    static void ClearAll()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            if (EditorSceneManager.GetSceneAt(i).name != "MasterScene")
            {
                EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(i));
            }
        }
    }
}
#endif
