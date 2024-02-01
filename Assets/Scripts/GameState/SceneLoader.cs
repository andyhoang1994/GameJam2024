using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    StartScene,
    GameScene,
    GameOverScene,
}
public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(Scene.StartScene.ToString());
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(Scene.GameOverScene.ToString());
    }
}
