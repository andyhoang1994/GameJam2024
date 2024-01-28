using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    StartScene,
    GameScene,
}
public class SceneLoader : MonoBehaviour
{
    public static void Load()
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());
    }
}
