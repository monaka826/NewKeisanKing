using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EdMg : MonoBehaviour
{
    
    public Text Score;
    public GameObject TitleBt;

    int ScoreGet;
    string core = "–â“ž’B";
    // Start is called before the first frame update
    void Start()
    {
        ScoreGet = GmMg.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DrawScore", 3);
    }
    void DrawScore()
    {
        Score.text = ScoreGet.ToString() + core;
        Invoke("DrawBt", 1);
    }
    void DrawBt()
    {
        TitleBt.SetActive(true);
    }
    public void PushBt()
    {
        SceneManager.LoadScene("Title");
    }
}
