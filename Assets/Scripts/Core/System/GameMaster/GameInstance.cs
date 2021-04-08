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
}