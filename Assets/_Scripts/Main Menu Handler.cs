using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    private void Start() => SingletonHandler.musicManager.PlayMusic("MainMenuBGM");

    public void OnGameSceneButton() => SingletonHandler.sceneHandler.ChangeSceneTo("Game");
}