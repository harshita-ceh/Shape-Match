using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameOver GameOver;
    public static GameManager instance;

    [SerializeField]
    public List<Scene> levels;
    //Storess all available scenes in an array. Would be used by other scripts to determine the next scene to load.

    [SerializeField]
    public List<int> scoreRequirements;
    //This would be used to determine the condition under which the scene may be changed.

    private void Awake()
    {
        if (instance == null) //Singleton. Can't have multiple instances.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            //Start spawning.
        }
        if (scene.name == "Level2")
        {
            //Start spawning.
        }
        if (scene.name == "Level3")
        {
            //Start spawning.
        }
        
    }
    public void PlayerDead()
    {
        GameOver.PlayerDied();
    }

}
