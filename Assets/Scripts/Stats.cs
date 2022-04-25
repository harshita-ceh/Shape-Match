using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Stats : MonoBehaviour
{

    public static Stats instance;


    private int score = 0;
    private int scoreRequirement;
    private int lives = 10;
    private int difficulty = 3;

    public Text scoreDisplay;
    public Text healthDisplay;

    public List<Scene> sceneList;

    private void Awake()
    {
        if (instance == null) //Singleton pattern. We need to keep the score stored as long as the game is running.
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //stats must remain on screen between level transitions.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneList = GameObject.Find("Game Manager").GetComponent<GameManager>().levels;
        //This would be useful when the scene is about to transition.

        scoreDisplay.text = score.ToString();
        healthDisplay.text = (lives / 2).ToString(); //A lazy patch to address the duplicate event bug.
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = score.ToString();
        healthDisplay.text = (lives / 2).ToString();
    }

    private void OnEnable()
    { //Collision events, with straightforward names.
        CollisionLogic.shapesMatchedInfo += RecalculateScore;
        CollisionLogic.shapeFossilizedInfo += RecalculateHealth;
        CollisionLogic.shapeFellOffInfo += RecalculateHealth;
    }

    private void OnDisable()
    {
        CollisionLogic.shapesMatchedInfo -= RecalculateScore;
        CollisionLogic.shapeFossilizedInfo -= RecalculateHealth;
        CollisionLogic.shapeFellOffInfo -= RecalculateHealth;
    }
    void RecalculateScore()
    {
        Debug.Log("Score Calculation Invoked.");
        score += 100; //Increment 100 for every shape, so 200 on each match.
      
    }

    void RecalculateHealth()
    {
        //If the health value is odd, then the displayed number wouldn't change unless we decrement it twice.
        //This is because of the displayed value being this "lives" int value divided by two, with its decimal part truncated.
        if (lives % 2 != 0) lives--;
        lives--;
       
    }
}
