using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushNumber : MonoBehaviour
{
    public Button[] NumberBt;

    public GameObject GameMg;
    GmMg Gm_Mg;
    // 結果表示テキスト
    public Text Answer;

    string InsertAns;
    public float FinalAns;

    int PushCnt;
    int LimitCnt;
    // Start is called before the first frame update
    void Start()
    {
        Gm_Mg = GameMg.GetComponent<GmMg>();
        Answer.text = "";
        PushCnt = 0;
        LimitCnt = 7; // 入力制限
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 数字の入力処理
    public void PushBt(Text number)
    {
        if (Gm_Mg.NowGameState == GmMg.GameState.InGame)

            if (PushCnt < LimitCnt && Gm_Mg.AnsOK)
            {
                Answer.text += number.text;
                InsertAns = Answer.text;
                PushCnt++;
            }
    }
    // 決定ボタンを押した際の処理
    public void PushBtDecade()
    {
        if (InsertAns != "")
        {
            if (PushCnt != 0)
            { 
               // FinalAns = int.Parse(InsertAns); // int型に変換
                float.TryParse(InsertAns, out FinalAns);
                Gm_Mg.SendAnswer(FinalAns);
                PushCnt = 0;
            }
        }
    }
    // 文字クリア
    public void PushBtCancel()
    {
        Answer.text = "";
        InsertAns = "";
    }
}
