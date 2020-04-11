using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button Game;
    public Button Exit;

    void Start()
    {
        Button onGame = Game.GetComponent<Button>();
        Button onExit = Exit.GetComponent<Button>();
        onGame.onClick.AddListener(LaunchGame);
        onExit.onClick.AddListener(LaunchExit);
    }
    void LaunchGame()
    {
        Debug.Log("LAUNCH GAME");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void LaunchExit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
    void Update()
    {
        
    }
}
