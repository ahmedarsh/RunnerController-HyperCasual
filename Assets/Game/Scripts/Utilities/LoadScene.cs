using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName = "GameScene";

    public void LoadGivenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}