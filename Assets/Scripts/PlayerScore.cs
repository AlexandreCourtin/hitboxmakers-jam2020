using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PlayerScore : MonoBehaviour
{
    Regex r = new Regex("^([-+]?)?[0-9]+(,[0-9]+)?$");

    public Text[] PN;
    private int let = 0;

    void Start()
    {
    }

    void resetPanel ()
    {
        foreach (Text ln in PN)
        {
            ln.GetComponent<Text>().text = "_";
        }
        let = 0;
        transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (let < 3)
        {
            // Get player name
            foreach (char c in Input.inputString)
            {
                PN[let].text = c.ToString();
                if (c == '\f' || c == '\t' || c == '\n' || c == '\r' || c == '\v' || c == '\b')
                {
                    if (PN[let].text.Length != 0)
                    {
                        PN[let].text = PN[let].text.Substring(0, PN[let].text.Length - 1);
                        PN[let].text = " ";
                    }
                }
                PN[let].text.ToUpper();
                Debug.Log(c);
                let++;
            }
        } else
        {
            string name = "";
            foreach (Text ln in PN)
            {
                name += ln.GetComponent<Text>().text;
            }
            Game_Manager.GM.playerName = name;
            Game_Manager.GM.save = true;
            resetPanel();
        }
    }

}