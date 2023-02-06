using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GmMg : MonoBehaviour
{
    const int capacity = 3;
    const float TimeLimit = 10.0f;


    public List<int> InsertQ = new List<int>(capacity);
    public List<string> InsertQ_Formura = new List<string>(capacity);
    public enum GameState {
    StartUp,
    InGame,
    EndGame,
    }
    public GameState NowGameState;
   
    public Text CountDownText;

    public Text Q_Text;

    public Text GameTimeText;

    public Text CorrectText;

    public float GameTime;

    float CountDown;
    int PushCountDown;
    string StartText;

    public GameObject PushMg;
    PushNumber pushNumber;

    int InsertQ_Cnt;
    public int PubQ_Cnt;
    int Correct_Cnt;

    public int Answer;

    bool AnsCheck;
    bool CorrectCheck;
    bool ClearCheck;
    // Start is called before the first frame update
    void Start()
    {
        NowGameState = GameState.StartUp;

        GameTime = TimeLimit;
        CountDown     = 4.0f;
        PushCountDown = 0;
        InsertQ_Cnt = 0;
        PubQ_Cnt = 0;
        Correct_Cnt = 0;
        Answer = 0;

        AnsCheck = false;
        CorrectCheck = false;
        ClearCheck = false;
        StartText = "START";

        pushNumber = PushMg.GetComponent<PushNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NowGameState == GameState.StartUp)
        {
            FirstCountDown();
            QuestionList();
        }
        if (NowGameState == GameState.InGame)
        {
            MainGame();
        }
        if (NowGameState == GameState.EndGame)
        {
            SceneManager.LoadScene("GameEnd");
        }
        
    }
    void FirstCountDown()
    {
        CountDown -= Time.deltaTime;
        PushCountDown = (int)CountDown;
        CountDownText.text = PushCountDown.ToString().PadLeft(6);

        if(CountDown <= 1.0f)
        {
            CountDownText.text = StartText;
            if(CountDown <= 0.0f)
            {
                NowGameState = GameState.InGame;
                Destroy(CountDownText);
            }
        }
    }
    void QuestionList()
    {
        var Q1 = new List<int>(capacity);
        var Q2 = new List<int>(capacity);
        var Rand = new List<int>(capacity);

        string Fomura;

        if (InsertQ_Cnt < capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                Q1.Add(Random.Range(1, 10));
                Q2.Add(Random.Range(1, 10));

                Rand.Add(Random.Range(1, 4));

                InsertQ_Cnt++;
                switch (Rand[i])
                {
                    case 1:
                        InsertQ.Add(Q1[i] + Q2[i]);
                        Fomura = Q1[i].ToString() + "+" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                       break;
                    case 2:
                        InsertQ.Add(Q1[i] - Q2[i]);
                        Fomura = Q1[i].ToString() + "-" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                        break;
                    case 3:
                        InsertQ.Add(Q1[i] * Q2[i]);
                        Fomura = Q1[i].ToString() + "×" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                        break;
                    case 4:
                        InsertQ.Add(Q1[i] / Q2[i]);
                        Fomura = Q1[i].ToString() + "÷" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                        break;
                    default:
                        break;
                }

                Fomura = "";
            }

        }

    }
    void MainGame()
    {
        GameTime -= Time.deltaTime;
        GameTimeText.text = GameTime.ToString().PadLeft(6);
        
            if (PubQ_Cnt < capacity && GameTime >= 0.0f) // // 格納した問題分、タイマーが0になるまで
            {
                // 一門ずつ問題を出題し、合っていたら次の問題、解答のクリアを行う
                // 答える前にアンサーの初期値が0であるため、勝手に正解、不正解になってしまうので、そこを治す。
                for (int i = 0; i < capacity; i++)
                {
                    Q_Text.text = InsertQ_Formura[PubQ_Cnt].ToString();
                    if (AnsCheck == true)
                    {
                        if (Answer == InsertQ[PubQ_Cnt])
                        {
                            Correct();
                        }

                        if (Answer != InsertQ[PubQ_Cnt] && !CorrectCheck)
                        {
                            NotCorrect();
                        }

                    }
                }
                CorrectCheck = false;
            }
            if (GameTime <= 0.0f)
            {
                NotCorrect();
            }
       
    }
    void Correct()
    {
        string count = "問正解！";

        PubQ_Cnt++;
        Correct_Cnt++;
        Answer = 0;
        GameTime = TimeLimit;
        AnsCheck = false;
        CorrectCheck = true;
        Debug.Log("正解");

        pushNumber.PushBtCancel();

        CorrectText.text = Correct_Cnt.ToString() + count;

    }
    void NotCorrect()
    {
        Destroy(GameTimeText);
        Destroy(CorrectText);
        Destroy(Q_Text);
        Debug.Log("不正解");

        pushNumber.PushBtCancel();
        NowGameState = GameState.EndGame;
    }
    public void SendAnswer(int ans)
    {
        Debug.Log(ans);
        Answer = ans;
        AnsCheck = true;
    }
}
