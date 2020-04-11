using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Restart : MonoBehaviour
{
    public Button StartButton;
    public GameObject UL;
    public GameObject li;

    void Start()
    {
        StartButton.onClick.AddListener(LaunchGame);

        // Display LB
        for (int i = 0; i < 10; i++)
        {
            if (i < Game_Manager.GM.highscores.Count)
            {
                GameObject lo = Instantiate(li);
                lo.transform.SetParent(UL.transform);
                lo.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                lo.transform.GetChild(1).GetComponent<Text>().text = Game_Manager.GM.highscores.ElementAt(i).Key;
                lo.transform.GetChild(2).GetComponent<Text>().text = Game_Manager.GM.highscores.ElementAt(i).Value.ToString();
            } else
            {
                GameObject lo = Instantiate(li);
                lo.transform.SetParent(UL.transform);
                lo.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            }

        }
    }

    void LaunchGame ()
    {
        Debug.Log("RESTART");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        
    }
}
