using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    public Button ButtonRestart;
    public Button MainMenu;

    private void Awake()
    {
        ButtonRestart.onClick.AddListener(RestartScene);
        MainMenu.onClick.AddListener(mainMenu);
        
    }
    public void PlayerDied()
    {
        
        gameObject.SetActive(true);
    }
    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    public void mainMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }

}
