using UnityEngine;
using Define;
public class ReelController : MonoBehaviour
{
    public GameObject m_Reel1; //リールを取得
    public GameObject m_Reel2;
    public GameObject m_Reel3;
    Vector3 m_initialpos1 = Vector3.zero;//初期位置
    Vector3 m_initialpos2 = Vector3.zero;
    Vector3 m_initialpos3 = Vector3.zero;
    float m_speed1 = 0.0f; //リールの回転速度
    float m_speed2 = 0.0f;
    float m_speed3 = 0.0f;

    float m_reelYPos1 = 0.0f;
    float m_reelYPos2 = 0.0f;
    float m_reelYPos3 = 0.0f;

    float m_delMove1 = 0.0f; //リールの移動差分量
    float m_delMove2 = 0.0f;
    float m_delMove3 = 0.0f;

    bool m_stopflag1 = false;//ボタンが押されたかどうか
    bool m_stopflag2 = false;
    bool m_stopflag3 = false;
    bool m_allflag = false;//子役チェックを一度だけ行うためのbool値

    ReelGenerator m_reelGenerator1;//ReelGenerator1の宣言
    ReelGenerator m_reelGenerator2;
    ReelGenerator m_reelGenerator3;

    GameController m_gameController;

    int m_lotResult = 0;

    private void Awake()　//ゲーム開始時に
    {
        m_initialpos1 = this.m_Reel1.transform.position;　//初期位置を取得しておく
        m_initialpos2 = this.m_Reel2.transform.position;
        m_initialpos3 = this.m_Reel3.transform.position;

        m_reelYPos1 = m_initialpos1.y;
        m_reelYPos2 = m_initialpos2.y;
        m_reelYPos3 = m_initialpos3.y;

        m_reelGenerator1 = GameObject.Find("ReelGenerator").GetComponent<ReelGenerator>();//ReelGenerator1の取得
        m_reelGenerator2 = GameObject.Find("ReelGenerator (1)").GetComponent<ReelGenerator>();
        m_reelGenerator3 = GameObject.Find("ReelGenerator (2)").GetComponent<ReelGenerator>();

        m_gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public bool StartReel()//リールを回し始める関数
    {
        //DestroyGetter();

        //m_speed1 = -0.384f;　//リールの回転スピードの代入
        //m_speed2 = -0.384f;
        //m_speed3 = -0.384f;

        if(m_speed1 != 0 || m_speed2 != 0 || m_speed3 != 0)
        {
            return false;
        }

        m_speed1 = -30.0f;　//リールの回転スピードの代入
        m_speed2 = -30.0f;
        m_speed3 = -30.0f;

        m_stopflag1 = false;　//ボタンを押していない状態にリセット
        m_stopflag2 = false;
        m_stopflag3 = false;
        m_allflag = false;

        // レバーオン時役抽選
        m_lotResult = (int)m_gameController.LotteryRole();

        return true;
    }

    private void FixedUpdate()　//ゲームが始まったら
    {
        //this.Reel1.transform.Translate(0, speed1, 0);　//リールをｙ方向（下）に動かす
        //this.Reel2.transform.Translate(0, speed2, 0);
        //this.Reel3.transform.Translate(0, speed3, 0);

        m_delMove1 = Time.deltaTime * m_speed1;
        m_delMove2 = Time.deltaTime * m_speed2;
        m_delMove3 = Time.deltaTime * m_speed3;

        m_reelYPos1 += m_delMove1;
        m_reelYPos2 += m_delMove2;
        m_reelYPos3 += m_delMove3;
        //this.Reel1.transform.Translate(0, m_reelYPos1, 0);　//リールをｙ方向（下）に動かす
        //this.Reel2.transform.Translate(0, m_reelYPos2, 0);
        //this.Reel3.transform.Translate(0, m_reelYPos3, 0);

        if (m_reelYPos1 < -CO.REEL_WIDTH * m_reelGenerator1.GetReelElmNum())　//リールが一番下に来たら
        {
            this.m_Reel1.transform.position = m_initialpos1;　//初期位置に戻す
            m_reelYPos1 = m_initialpos1.y;
            m_reelGenerator1.DestroyReel();
            m_reelGenerator1.GenerateReel();
        }
        if (m_reelYPos2 < -CO.REEL_WIDTH * m_reelGenerator2.GetReelElmNum())
        {
            this.m_Reel2.transform.position = m_initialpos2;
            m_reelYPos2 = m_initialpos2.y;
            m_reelGenerator2.DestroyReel();
            m_reelGenerator2.GenerateReel();
        }
        if (m_reelYPos3 < -CO.REEL_WIDTH * m_reelGenerator3.GetReelElmNum())
        {
            this.m_Reel3.transform.position = m_initialpos3;
            m_reelYPos3 = m_initialpos3.y;
            m_reelGenerator3.DestroyReel();
            m_reelGenerator3.GenerateReel();
        }

        //ボタンが押されていて、かつ、リールを図柄感覚で割った余りが規定値以内であったら
        if(m_speed1 < 0.0f && m_stopflag1 && Mathf.Abs(m_reelYPos1) % CO.REEL_WIDTH <= (CO.REEL_WIDTH * 0.5f))
        {
            //図柄チェックを行って、止めていい役なら
            int slideNum = GetSlideFrameNum(CO.REEL_TYPE.REEL_TYPE_LEFT);
            if(slideNum == 0)
            {
                m_reelYPos1 = -(int)(Mathf.Abs(m_reelYPos1) / CO.REEL_WIDTH) * CO.REEL_WIDTH;
                m_speed1 = 0.0f;//リールの回転スピードを０にする
            }
        }
        if(m_speed2 < 0.0f && m_stopflag2 && Mathf.Abs(m_reelYPos2) % CO.REEL_WIDTH <= (CO.REEL_WIDTH * 0.5f))
        {
            //図柄チェックを行って、止めていい役なら
            int slideNum = GetSlideFrameNum(CO.REEL_TYPE.REEL_TYPE_CENTER);
            if(slideNum == 0)
            {
                m_reelYPos2 = -(int)(Mathf.Abs(m_reelYPos2) / CO.REEL_WIDTH) * CO.REEL_WIDTH;
                m_speed2 = 0.0f;//リールの回転スピードを０にする
            }
        }
        if(m_speed3 < 0.0f && m_stopflag3 && Mathf.Abs(m_reelYPos3) % CO.REEL_WIDTH <= (CO.REEL_WIDTH * 0.5f))
        {
            //図柄チェックを行って、止めていい役なら
            int slideNum = GetSlideFrameNum(CO.REEL_TYPE.REEL_TYPE_RIGHT);
            if(slideNum == 0)
            {
                m_reelYPos3 = -(int)(Mathf.Abs(m_reelYPos3) / CO.REEL_WIDTH) * CO.REEL_WIDTH;
                m_speed3 = 0.0f;//リールの回転スピードを０にする
            }
        }

        this.m_Reel1.transform.position = new Vector3(this.m_Reel1.transform.position.x, m_reelYPos1, this.m_Reel1.transform.position.z);
        this.m_Reel2.transform.position = new Vector3(this.m_Reel2.transform.position.x, m_reelYPos2, this.m_Reel2.transform.position.z);
        this.m_Reel3.transform.position = new Vector3(this.m_Reel3.transform.position.x, m_reelYPos3, this.m_Reel3.transform.position.z);

        //すべてのリールが停止していてallflagがfalseの場合
        if(m_speed1 == 0.0f && m_speed2 == 0.0f && m_speed3 == 0.0f && !m_allflag)
        {
            m_allflag = true;//小役チェックが一度しか行われないようにtrueにしておく
            m_gameController.CheckMiddle();//子役チェックプログラムの呼び出し
        }
    }
    public void StopReel1() //この関数がボタン1を押すと呼ばれる
    {
        //if(!m_stopflag1) { m_speed1 = -0.05f; }
        m_stopflag1 = true;
    }
    public void StopReel2()
    {
        //if(!stopflag2) { m_speed2 = -0.05f; }
        m_stopflag2 = true;
    }
    public void StopReel3()
    {
        //if(!m_stopflag3) { m_speed3 = -0.05f; }
        m_stopflag3 = true;
    }
    public void DestroyGetter()
    {
        //絵柄ID取得プレファブをデストロイしてリセットする関数
        GameObject[] getters = GameObject.FindGameObjectsWithTag("getter"); //getterタグが付いているオブジェクトをすべて取得
        foreach(GameObject i in getters)//各getterのオブジェクトに対して以下を行う
        {
            Destroy(i);//getterの各オブジェクトを破壊する
        }
    }

    private int GetSlideFrameNum(CO.REEL_TYPE _reelType)
    {
        int ret = 0;
        int pattern = 0;
        int symbolInx = 0;
        switch(_reelType)
        {
            case CO.REEL_TYPE.REEL_TYPE_LEFT:
                pattern = m_gameController.GetHitPattern(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_BOTTOM);
                symbolInx = m_gameController.GetHitIndexOnReel(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_BOTTOM);
                ret = CO.LEFT_REEL_STOP_TABLE_LIST[m_lotResult][symbolInx];
                break;
            case CO.REEL_TYPE.REEL_TYPE_CENTER:
                pattern = m_gameController.GetHitPattern(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE);
                symbolInx = m_gameController.GetHitIndexOnReel(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE);
                ret = CO.MIDDLE_REEL_STOP_TABLE_LIST[m_lotResult][symbolInx];
                break;
            case CO.REEL_TYPE.REEL_TYPE_RIGHT:
                pattern = m_gameController.GetHitPattern(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP);
                symbolInx = m_gameController.GetHitIndexOnReel(_reelType, CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP);
                ret = CO.RIGHT_REEL_STOP_TABLE_LIST[m_lotResult][symbolInx];
                break;
            default:
                break;
        }
        return ret;
    }
}