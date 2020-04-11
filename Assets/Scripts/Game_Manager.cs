using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager GM;
    void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
    }

    public GameObject Player;
    public bool isWin = false;
    public bool isLose = false;
    public int score = 0;
    public int phase = 1; // difficulte

    // SCORES
    public Dictionary<string, int> highscores;

    void Start()
    {
        // Load highscores
        string highscoresSerialized = PlayerPrefs.GetString("highscores", "");
        if (!String.IsNullOrEmpty(highscoresSerialized))
        {
            highscores = MyUnserialize(highscoresSerialized);
        }
        else
        {
            highscores = new Dictionary<string, int>();
        }
        UpdateScoreText();
    }

    public void updateScore(float n)
    {
        score += (int)(n * 10);
        UpdateScoreText();
    }

    private bool checkTopTen()
    {
        orderScore();
        if (highscores.Count > 0)
        {
            if (highscores.ElementAt(highscores.Count - 1).Value < score)
                return true;
            else
                return false;
        } else
        {
            return true;
        }
    }

    private void orderScore()
    {
        Dictionary<string, int> tmp = new Dictionary<string, int>();

        var items = from pair in highscores
                    orderby pair.Value descending
                    select pair;

        foreach (KeyValuePair<string, int> pair in items)
        {
            tmp.Add(pair.Key, pair.Value);
        }
        highscores = tmp;
    }

    void Update()
    {
        if (Player.transform.GetComponent<EarthLogic>().life <= 0)
        {
            isLose = true;
            // check player prefs if top 10
            if (checkTopTen())
            {
                // TO DO Get player name
                AddSafe(highscores, "zouz", 10);
                AddSafe(highscores, "adsadasd", 1000);

                // Save highscores
                string highscoresSerializedAgain = MySerialize(highscores);
                // Order Score Desc
                orderScore();
                // Save player prefs !
                PlayerPrefs.SetString("highscores", highscoresSerializedAgain);
                PlayerPrefs.Save();

                // TO DO Show highscores
            } else
            {
                // NO
                // TO DO Restart
            }
        }
    }

    void AddSafe(Dictionary<string, int> dictionary, string key, int value)
    {
        if (dictionary.ContainsKey(key))
        {
            if (dictionary[key] != value)
                dictionary.Add(key, value);
        } else
        {
            dictionary.Add(key, value);
        }
    }

    Dictionary<string, int> MyUnserialize(string highscoresSerialized)
    {
        byte[] mData = Convert.FromBase64String(highscoresSerialized);
        MemoryStream memorystream = new MemoryStream(mData);
        BinaryFormatter bf = new BinaryFormatter();
        Dictionary<string, int> obj = (Dictionary<string, int>)bf.Deserialize(memorystream);
        return obj;
    }

    string MySerialize(Dictionary<string, int> highscores)
    {
        MemoryStream memorystream = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(memorystream, highscores);
        byte[] mStream = memorystream.ToArray();
        string slist = Convert.ToBase64String(mStream);
        return slist;
    }

    void UpdateScoreText() {
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score;
    }

}
