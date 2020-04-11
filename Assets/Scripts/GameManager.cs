using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private void Awake()
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
        // Load highscores.
        string highscoresSerialized = PlayerPrefs.GetString("highscores", "");
        if (!String.IsNullOrEmpty(highscoresSerialized))
        {
            highscores = MyUnserialize(highscoresSerialized);
        } else
        {
            highscores = new Dictionary<string, int>();
        }
    }

    public void updateScore (float n)
    {
        score += (int)(n * 10);
    }

    private void orderScore()
    {
        Dictionary<string, int> tmp = new Dictionary<string, int>();
        var items = from pair in highscores
                    orderby pair.Value descending
                    select pair;

        foreach (KeyValuePair<string, int> pair in items)
        {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
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

            // YES
            // TO DO Get player name

            // Update highscores.
            highscores.Add("zouz", 10);
            highscores.Add("sacsaz", 100);
            // Order Score Desc
            orderScore();
            // Save highscores.
            string highscoresSerializedAgain = MySerialize(highscores);
            // PlayerPrefs.SetString("highscores", highscoresSerializedAgain);
            // PlayerPrefs.Save();

            // Show highscores

            // NO
        }
    }

    private Dictionary<string, int> MyUnserialize(string highscoresSerialized)
    {
        return Unserialize(highscoresSerialized);
    }

    public static string Serialize(Dictionary<string, int> obj)
    {
        MemoryStream memorystream = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(memorystream, obj);
        byte[] mStream = memorystream.ToArray();
        string slist = Convert.ToBase64String(mStream);
        return slist;
    }

    public static Dictionary<string, int> Unserialize(string str)
    {
        byte[] mData = Convert.FromBase64String(str);
        MemoryStream memorystream = new MemoryStream(mData);
        BinaryFormatter bf = new BinaryFormatter();
        Dictionary<string, int> obj = (Dictionary<string, int>)bf.Deserialize(memorystream);
        return obj;
    }

    private string MySerialize(Dictionary<string, int> highscores)
    {
        return Serialize(highscores);
    }
}
