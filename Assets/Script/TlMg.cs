using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TlMg : MonoBehaviour
{
    public Text BestScoreText;

    int Score;
    int BestScore;
    string core = "ベストスコア:";
    string core2 = "問";

    [SerializeField] private AudioSource Eff; 
    [SerializeField] private AudioClip BtEff;
    // Start is called before the first frame update
    void Start()
    {
        Score = GmMg.GetScore();
        BestScore = PlayerPrefs.GetInt("HIGHSCORE",0);
    }

    // Update is called once per frame
    void Update()
    {
        // スコア保存
        if (BestScore < Score)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("HIGHSCORE", BestScore);
            PlayerPrefs.Save();
        }
        BestScoreText.text = core + BestScore.ToString() + core2;
    }
    public void PushBt()
    {
        Invoke("SceneTrip", 2);
        Eff.PlayOneShot(BtEff);
        //PlayerPrefs.DeleteKey("HIGHSCORE");
    }
    void SceneTrip()
    {
        SceneManager.LoadScene("MainScene");
    }
}
