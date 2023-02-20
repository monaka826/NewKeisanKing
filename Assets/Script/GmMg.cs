using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GmMg : MonoBehaviour
{
    const int capacity = 500;
  
    const int One = 1;
    const int Two = 2;
    const int Three = 3;
    const int Four = 4;
    const int Five = 5;
    const int Ten = 10;
    const int Fifteen = 15;
    const int Twenty = 20; 
    const int TwentyFive = 25; 
    const int Thirty = 30;
    const int Fourty = 40;
    const int Fifty = 50;
    const int Sixty = 60;

    const int OneHand = 100;
    const int OneHandFifty = 150;
    const int TwoHand = 200;
    const int TwoHandFifty = 250;
    const int ThreeHand = 300;
    const int FourHand = 400;

    float TimeLimit;


    public List<float> InsertQ = new List<float>(capacity);
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

    float GameTime;

    float CountDown;
    int PushCountDown;
    string StartText;

    public GameObject PushMg;
    PushNumber pushNumber;

    int InsertQ_Cnt;
    public int PubQ_Cnt;
    public static int Correct_Cnt;

    public float Answer;

    bool AnsCheck;
    bool CorrectCheck;
    bool ClearCheck;
    public bool AnsOK;

    [SerializeField] Image BackColar;
    [SerializeField] Image BackImage;
    [SerializeField] Sprite CorectImage;
    [SerializeField] Sprite NotCorectImage;

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource Eff;
    [SerializeField] private AudioClip BGMEff;
    [SerializeField] private AudioClip CorectEff;
    [SerializeField] private AudioClip NotCorectEff;
    // Start is called before the first frame update
    void Start()
    {
        NowGameState = GameState.StartUp;

        TimeLimit = 10.0f;
        GameTime = TimeLimit;
        CountDown     = 4.0f;
        PushCountDown = 0;
        InsertQ_Cnt = 0;
        PubQ_Cnt = 0;
        Correct_Cnt = 0;
        Answer = 0;

        AnsCheck = false;
        AnsOK = true;
        CorrectCheck = false;
        ClearCheck = false;
        StartText = "START";

        pushNumber = PushMg.GetComponent<PushNumber>();

        BackImage.enabled = false;
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
    // ゲームスタート時のカウントダウン
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
    // 問題の挿入
    void QuestionList()
    {
        var Q1 = new List<float>(capacity);
        var Q2 = new List<float>(capacity);
        var FQ = new List<float>(capacity);
        var Rand = new List<int>(capacity);

        string Fomura;

        if (InsertQ_Cnt < capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                if (InsertQ_Cnt < Ten)
                {
                    Q1.Add(Random.Range(Five, Ten));
                    Q2.Add(Random.Range(One, Five));
                    Rand.Add(Random.Range(One, Two));
                }
                if (InsertQ_Cnt >= Ten && InsertQ_Cnt < Twenty)
                {
                    Q1.Add(Random.Range(Ten, Fifteen));
                    Q2.Add(Random.Range(One, Ten));
                    Rand.Add(Random.Range(One, Three));
                }
                if (InsertQ_Cnt >= Twenty && InsertQ_Cnt < Thirty)
                {
                    Q1.Add(Random.Range(Fifteen, Twenty));
                    Q2.Add(Random.Range(One, Fifteen));
                    Rand.Add(Random.Range(One, Four));
                }
                if (InsertQ_Cnt >= Thirty && InsertQ_Cnt < Sixty)
                {
                    Q1.Add(Random.Range(Twenty, TwentyFive));
                    Q2.Add(Random.Range(One, Twenty));
                    Rand.Add(Random.Range(One, Five));
                }

                if (InsertQ_Cnt >= Sixty && InsertQ_Cnt < OneHand)
                {
                    Q1.Add(Random.Range(TwentyFive,Fourty));
                    Q2.Add(Random.Range(One, TwentyFive));
                    Rand.Add(Random.Range(One, Five));
                }
                if (InsertQ_Cnt >= OneHand && InsertQ_Cnt < TwoHand)
                {
                    Q1.Add(Random.Range(Fourty, Sixty));
                    Q2.Add(Random.Range(One, Fourty));
                    Rand.Add(Random.Range(One, Five));
                }
                if (InsertQ_Cnt >= TwoHand && InsertQ_Cnt < ThreeHand)
                {
                    Q1.Add(Random.Range(Sixty, OneHand));
                    Q2.Add(Random.Range(One, Sixty));
                    Rand.Add(Random.Range(One, Five));
                }
                if (InsertQ_Cnt >= ThreeHand && InsertQ_Cnt < FourHand)
                {
                    Q1.Add(Random.Range(OneHand, TwoHand));
                    Q2.Add(Random.Range(One, OneHand));
                    Rand.Add(Random.Range(One, Five));
                }
                if (InsertQ_Cnt >= FourHand)
                {
                    Q1.Add(Random.Range(TwoHand, FourHand));
                    Q2.Add(Random.Range(One, TwoHand));
                    Rand.Add(Random.Range(One, Five));
                }
                InsertQ_Cnt++;
                
                switch (Rand[i])
                {
                    case 1:
                        FQ.Add((Q1[i] + Q2[i]) * Ten);                       
                        InsertQ.Add(Mathf.Floor(FQ[i])/ Ten);
                        //InsertQ.Add(Q1[i] + Q2[i]);
                        Fomura = Q1[i].ToString() + "+" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                       break;
                    case 2:
                        FQ.Add((Q1[i] - Q2[i]) * Ten);
                        InsertQ.Add(Mathf.Floor(FQ[i]) / Ten);
                        //InsertQ.Add(Q1[i] - Q2[i]);
                        Fomura = Q1[i].ToString() + "-" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                        break;
                    case 3:
                        FQ.Add((Q1[i] * Q2[i]) * Ten);
                        InsertQ.Add(Mathf.Floor(FQ[i]) / Ten);
                        //InsertQ.Add(Q1[i] * Q2[i]);
                        Fomura = Q1[i].ToString() + "×" + Q2[i].ToString();
                        InsertQ_Formura.Add(Fomura);
                        break;
                    case 4:
                        FQ.Add((Q1[i] / Q2[i]) * Ten);
                        InsertQ.Add(Mathf.Floor(FQ[i]) / Ten);
                        //InsertQ.Add(Q1[i] / Q2[i]);
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
        if (AnsOK)
        {
            BGM.PlayOneShot(BGMEff);
            GameTime -= Time.deltaTime;
            GameTimeText.text = GameTime.ToString().PadLeft(6);

            if (PubQ_Cnt < capacity && GameTime >= 0.0f) // // 格納した問題分、タイマーが0になるまで
            {
                for (int i = 0; i < capacity; i++)
                {
                    if (!ClearCheck)
                    {
                        Q_Text.text = InsertQ_Formura[PubQ_Cnt].ToString().PadLeft(8);
                        if (AnsCheck == true)
                        {
                            CorrectCheck = false;
                            if (Answer != InsertQ[PubQ_Cnt] && !CorrectCheck)
                            {
                                NotCorrect();
                            }
                            if (Answer == InsertQ[PubQ_Cnt])
                            {
                                Correct();
                            }
                        }
                    }

                }

            }
        }
        // 時間切れでゲームオーバー。
        if (GameTime <= 0.0f)
        {
            NotCorrect();
        }
        // 問題数に応じて背景色、タイマーを変化。
        if (PubQ_Cnt == Fifty)
        {
            TimeLimit = 9.0f;
            BackColar.color = new Color32(255, 225, 225, 255);
        }
        if (PubQ_Cnt == OneHand)
        {
            TimeLimit = 8.0f;
            BackColar.color = new Color32(255, 195, 195, 255);
        }
        if (PubQ_Cnt == OneHandFifty)
        {
            TimeLimit = 7.0f;
            BackColar.color = new Color32(255, 165, 165, 255);
        }
        if (PubQ_Cnt == TwoHand)
        {
            TimeLimit = 6.0f;
            BackColar.color = new Color32(255, 135, 135, 255);
        }
        if (PubQ_Cnt == TwoHandFifty)
        {
            TimeLimit = 5.0f;
            BackColar.color = new Color32(255, 95, 95, 255);
        }
        if (PubQ_Cnt == ThreeHand)
        {
            TimeLimit = 4.0f;
            BackColar.color = new Color32(255, 65, 65, 255);
        }
        if (PubQ_Cnt == FourHand)
        {
            TimeLimit = 3.0f;
            BackColar.color = new Color32(255, 35, 35, 255);
        }
   
    }
    // 正解処理
    void Correct()
    {
        //Eff.PlayOneShot(CorectEff);

        // 正解画像表示
        BackImage.enabled = true;
        BackImage.sprite = CorectImage;

        string count = "問正解！";
        // 問題、正解数をプラス
        PubQ_Cnt++;
        Correct_Cnt++;

        Answer = 0;
        // タイマーを初期化
        GameTime = TimeLimit;

        AnsCheck = false;
        CorrectCheck = true;

        Debug.Log("正解");

        pushNumber.PushBtCancel();

        CorrectText.text = Correct_Cnt.ToString() + count; // 正解数表示

        Invoke("DelayDelete",0.3f);
        // 正解数が問題数に達した場合の処理
        if(PubQ_Cnt == capacity)
        {
            ClearCheck = true;
            NowGameState = GameState.EndGame;
        }
        
    }
    // 正解画像非表示
    void DelayDelete()
    {
        BackImage.enabled = false; 
    }
    //不正解処理
    void NotCorrect()
    {
        Eff.PlayOneShot(NotCorectEff);
        BGM.Stop();
        // UI非表示
        Destroy(GameTimeText);
        pushNumber.PushBtCancel();
        // 不正解画像表示
        BackImage.enabled = true;
        BackImage.sprite = NotCorectImage;
        // 解答を受け付けない
        AnsOK = false;

        Invoke("DelayEnd", 2);
    }
    // ゲーム終了処理
    void DelayEnd()
    {
        //Destroy(GameTimeText);
        //Destroy(CorrectText);
        //Destroy(Q_Text);
        Debug.Log("不正解");
        NowGameState = GameState.EndGame;
    }
    // 解答を受け取る関数    
    public void SendAnswer(float ans)
    {
        Debug.Log(ans);
        Answer = ans;
        AnsCheck = true;
    }
    // getter、スコアを取得する関数
    public static int GetScore()
    {
        return Correct_Cnt;
    }
}
