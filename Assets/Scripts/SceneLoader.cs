using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int numberOfScenes;

    private void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(DelayLevelLoad());
    }

    IEnumerator DelayLevelLoad()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(numberOfScenes - 1);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(!gameSession) { return; }
        gameSession.ResetGame();
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(numberOfScenes - 2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
