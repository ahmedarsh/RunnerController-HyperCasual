using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public bool hasToReload;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void OnClose()
    {
        if (hasToReload)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            hasToReload = false;
        }
    }
}