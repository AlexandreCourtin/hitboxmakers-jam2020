using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gui : MonoBehaviour
{
    public Text Score;
    public GameObject MenuScore;

    void Update()
    {
        Score.GetComponent<Text>().text = Game_Manager.GM.score.ToString();
    }
}
