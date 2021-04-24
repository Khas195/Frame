using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Game Instance", menuName = "Build Settings/Game Instance", order = 1)]
public class GameInstance : ScriptableObject
{
    public string instanceName = "";
    [Scene]
    public List<string> sceneList = new List<string>();
    public GameState.GameStateEnum desiredGameState;
#if UNITY_EDITOR
    [Button]
    public void AddScenesToHierarchy()
    {
        for (int i = 0; i < sceneList.Count; i++)
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/GameScenes/" + sceneList[i] + ".unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);
        }
    }
#else
#endif
}