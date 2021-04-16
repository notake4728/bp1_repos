using UnityEngine;
using UnityEngine.UI;
using Define;
using System.Collections;
public class GameController : MonoBehaviour
{
    public double coin;
    public GameObject score_object; // Textオブジェクト
    public GameObject win_object; // Textオブジェクト
    public GameObject debug_flag_object;
    public GameObject bonusLamp;

    //AudioSource[] sounds;//オーディオの宣言
    CO.ROLE_FLG_TYPE m_uRoleType = CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_NONE;

    // ボーナスゲーム管理用
    double m_getMedalCnt = 0;
    double m_maxGetMedalNum = 0;
    int m_modeBonus = 0;

    private void Awake()
    {
        //sounds = gameObject.GetComponents<AudioSource>();//オーディオの取得
    }
    // Update is called once per frame
    void Update()
    {
        Text score_text = score_object.GetComponent<Text>();
        score_text.text = coin.ToString() + "枚";
    }

    public CO.ROLE_TYPE LotteryRole()
    {
        int rand = Random.Range(0, CO.DENOMINATOR_PROBABILITY_MAX);

        CO.ROLE_TYPE ret = CO.ROLE_TYPE.ROLE_TYPE_MAX;

        // ボーナスゲーム中でないなら抽選
        if(m_modeBonus <= 0)
        {
            int num = (int)CO.ROLE_TYPE.ROLE_TYPE_MAX;
            int tmp = 0;
            for(int i = 0; i < num; ++i)
            {
                tmp += CO.ROLE_RATE_LIST[i];
                if((int)(m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_MASK) > 0)
                {
                    if(i <= (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_05)
                    {
                        continue;
                    }
                }

                if(rand <= tmp)
                {
                    ret = (CO.ROLE_TYPE)i;
                    break;
                }
            }
        }
        else
        {
            ret = CO.ROLE_TYPE.ROLE_TYPE_BELL;
        }

        // 小役フラグを下ろす
        m_uRoleType &= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_MASK;

        // 小手先ボーナス成立デバッグ
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_01;
        }

        // ボーナス抽選時、ペカ
        if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_MASK) == 0 &&
            ret <= CO.ROLE_TYPE.ROLE_TYPE_BIG_05)
        {
            Sprite sprite = Resources.Load<Sprite>("【BP1】img_lamp02");
            SpriteRenderer spriteRenderer = bonusLamp.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }

        switch(ret)
        {
            case CO.ROLE_TYPE.ROLE_TYPE_BIG_01:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_01;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_BIG_02:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_02;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_BIG_03:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_03;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_BIG_04:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_04;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_BIG_05:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_05;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_SUICA:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_SUICA;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_CHERRY:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_CHERRY;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_BELL:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BELL;
                break;
            case CO.ROLE_TYPE.ROLE_TYPE_REPLAY:
                m_uRoleType |= CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_REPLAY;
                break;
            default:
                break;
        }

        // 何も抽選されなかった場合、ボーナスフラグが立ってないか見る
        if(ret == CO.ROLE_TYPE.ROLE_TYPE_MAX)
        {
            if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_01) > 0)
            {
                ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_01;
            }
            else if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_02) > 0)
            {
                ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_02;
            }
            else if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_03) > 0)
            {
                ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_03;
            }
            else if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_04) > 0)
            {
                ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_04;
            }
            else if((m_uRoleType & CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_05) > 0)
            {
                ret = CO.ROLE_TYPE.ROLE_TYPE_BIG_05;
            }
        }

        // 成立役表示
        Text flag_text = debug_flag_object.GetComponent<Text>();
        flag_text.text = (ret != CO.ROLE_TYPE.ROLE_TYPE_MAX) ? ret.ToString() : "";

        return ret;
    }

    public CO.ROLE_FLG_TYPE GetRoleFlg()
    {
        return m_uRoleType;
    }

    public int GetHitPattern(CO.REEL_TYPE _reelType, CO.REEL_POS_TYPE _posType)
    {
        HitReelSymbolGetter useGetter;

        if(_reelType == CO.REEL_TYPE.REEL_TYPE_LEFT)
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("leftTopGetter").GetComponent<HitReelSymbolGetter>();//LeftTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("leftMiddleGetter").GetComponent<HitReelSymbolGetter>();//LeftMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("leftBottomGetter").GetComponent<HitReelSymbolGetter>();//LeftBottomGetterの取得
            }
        }
        else if(_reelType == CO.REEL_TYPE.REEL_TYPE_CENTER)
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("centerTopGetter").GetComponent<HitReelSymbolGetter>();//CenterTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("centerMiddleGetter").GetComponent<HitReelSymbolGetter>();//CenterMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("centerBottomGetter").GetComponent<HitReelSymbolGetter>();//CenterBottomGetterの取得
            }
        }
        else
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("rightTopGetter").GetComponent<HitReelSymbolGetter>();//RightTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("rightMiddleGetter").GetComponent<HitReelSymbolGetter>();//RightMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("rightBottomGetter").GetComponent<HitReelSymbolGetter>();//RightBottomGetterの取得
            }
        }

        return useGetter.GetHitSymbolType();
    }

    public int GetHitIndexOnReel(CO.REEL_TYPE _reelType, CO.REEL_POS_TYPE _posType)
    {
        HitReelSymbolGetter useGetter;

        if(_reelType == CO.REEL_TYPE.REEL_TYPE_LEFT)
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("leftTopGetter").GetComponent<HitReelSymbolGetter>();//LeftTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("leftMiddleGetter").GetComponent<HitReelSymbolGetter>();//LeftMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("leftBottomGetter").GetComponent<HitReelSymbolGetter>();//LeftBottomGetterの取得
            }
        }
        else if(_reelType == CO.REEL_TYPE.REEL_TYPE_CENTER)
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("centerTopGetter").GetComponent<HitReelSymbolGetter>();//CenterTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("centerMiddleGetter").GetComponent<HitReelSymbolGetter>();//CenterMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("centerBottomGetter").GetComponent<HitReelSymbolGetter>();//CenterBottomGetterの取得
            }
        }
        else
        {
            if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_TOP)
            {
                useGetter = GameObject.Find("rightTopGetter").GetComponent<HitReelSymbolGetter>();//RightTopGetterの取得
            }
            else if(_posType == CO.REEL_POS_TYPE.REEL_POS_TYPE_MIDDLE)
            {
                useGetter = GameObject.Find("rightMiddleGetter").GetComponent<HitReelSymbolGetter>();//RightMiddleGetterの取得
            }
            else
            {
                useGetter = GameObject.Find("rightBottomGetter").GetComponent<HitReelSymbolGetter>();//RightBottomGetterの取得
            }
        }

        return useGetter.GetHitSymbolIndex();
    }

    public void CheckMiddle()
    {
        Debug.Log("yobaretayo");
        //leftTopGetter = GameObject.Find("leftTopGetter").GetComponent<HitReelSymbolGetter>();//LeftTopGetterの取得
        //leftMiddleGetter = GameObject.Find("leftMiddleGetter").GetComponent<HitReelSymbolGetter>();//LeftMiddleGetterの取得
        HitReelSymbolGetter leftBottomGetter = GameObject.Find("leftBottomGetter").GetComponent<HitReelSymbolGetter>();//LeftBottomGetterの取得
        //centerTopGetter = GameObject.Find("centerTopGetter").GetComponent<HitReelSymbolGetter>();//CenterTopGetterの取得
        HitReelSymbolGetter centerMiddleGetter = GameObject.Find("centerMiddleGetter").GetComponent<HitReelSymbolGetter>();//CenterMiddleGetterの取得
        //centerBottomGetter = GameObject.Find("centerBottomGetter").GetComponent<HitReelSymbolGetter>();//CenterBottomGetterの取得
        HitReelSymbolGetter rightTopGetter = GameObject.Find("rightTopGetter").GetComponent<HitReelSymbolGetter>();//RightTopGetterの取得
        //rightMiddleGetter = GameObject.Find("rightMiddleGetter").GetComponent<HitReelSymbolGetter>();//RightMiddleGetterの取得
        //rightBottomGetter = GameObject.Find("rightBottomGetter").GetComponent<HitReelSymbolGetter>();//RightBottomGetterの取得

        int lSymbol = leftBottomGetter.GetHitSymbolType();
        int cSymbol = centerMiddleGetter.GetHitSymbolType();
        int rSymbol = rightTopGetter.GetHitSymbolType();

        bool isBonus = false;
        {
            // 青7
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01
              )
            {
                //sounds[0].Play();
                //StartCoroutine(Sound7Coroutine());
                WinR7();
                m_uRoleType &= ~CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_01;
                m_modeBonus = (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_01 + 1;
                m_maxGetMedalNum = 349;

                isBonus = true;
            }

            // 赤7
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01
              )
            {
                //sounds[0].Play();
                //StartCoroutine(Sound7Coroutine());
                WinR7();
                m_uRoleType &= ~CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_02;
                m_modeBonus = (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_02 + 1;
                m_maxGetMedalNum = 279;

                isBonus = true;
            }

            // 青白7
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_CHERRY
              )
            {
                //sounds[1].Play();
                //StartCoroutine(Sound7Coroutine());
                Win7();
                m_uRoleType &= ~CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_03;
                m_modeBonus = (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_03 + 1;
                m_maxGetMedalNum = 287;

                isBonus = true;
            }

            // 赤白7
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_01 &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_03
              )
            {
                //sounds[1].Play();
                //StartCoroutine(Sound7Coroutine());
                Win7();
                m_uRoleType &= ~CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_04;
                m_modeBonus = (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_04 + 1;
                m_maxGetMedalNum = 161;

                isBonus = true;
            }

            // REG
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BAR &&
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BAR &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BAR
              )
            {
                //sounds[2].Play();
                WinBrilliant();
                m_uRoleType &= ~CO.ROLE_FLG_TYPE.ROLE_FLG_TYPE_BONUS_05;
                m_modeBonus = (int)CO.ROLE_TYPE.ROLE_TYPE_BIG_05 + 1;
                m_maxGetMedalNum = 79;

                isBonus = true;
            }

            // BELL
            if(
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BELL &&
                (lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_03 || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BAR || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BLANK) &&
                (rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 || rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_03 || rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BLANK)
              )
            {
                //sounds[3].Play();
                //StartCoroutine(SoundCoroutine());
                WinBell();
            }

            // SUICA
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BELL &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BELL &&
                (cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SUICA || cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SUICA_02)
              )
            {
                //sounds[3].Play();
                //StartCoroutine(SoundCoroutine());
                WinWatermelon();
            }

            // CHERRY
            if(
                lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_CHERRY &&
                rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BELL
              )
            {
                //sounds[3].Play();
                //StartCoroutine(SoundCoroutine());
                WinCherry();
            }

            // REPLAY
            if(
                cSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_REPLAY &&
                (lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_03 || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BAR || lSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BLANK) &&
                (rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_02 || rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_SEVEN_03 || rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_REPLAY || rSymbol == (int)CO.PATTERN_TYPE.PATTERN_TYPE_BLANK)
              )
            {
                //sounds[3].Play();
                //StartCoroutine(SoundCoroutine());
                WinReplay();
            }
        }
        /*
        IEnumerator SoundCoroutine()//コルーチン本体
        {
            yield return new WaitForSeconds(1.5f);
            sounds[4].Play();
        }
        IEnumerator Sound7Coroutine()//コルーチン本体
        {
            yield return new WaitForSeconds(5f);
            sounds[5].Play();
        }
        */

        if(isBonus)
        {
            // ボーナス揃え時、ランプ消灯
            Sprite sprite = Resources.Load<Sprite>("【BP1】img_lamp01");
            SpriteRenderer spriteRenderer = bonusLamp.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }
    }
    void WinR7()
    {
        //coin += 150;
        //double win0 = 150;
        Text win_text = win_object.GetComponent<Text>();
        //win_text.text = win0.ToString() + "枚WIN!!";
        win_text.text = "BONUS START!!::WinR7";
    }
    void Win7()
    {
        //coin += 100;
        //double win1 = 100.00f;
        Text win_text = win_object.GetComponent<Text>();
        //win_text.text = win1.ToString() + "枚WIN!!";
        win_text.text = "BONUS START!!::Win7";
    }
    void WinBrilliant()
    {
        Text win_text = win_object.GetComponent<Text>();
        win_text.text = "BONUS START!!::WinBrilliant";
    }
    void WinBell()
    {
        coin += 7;
        double win3 = 7;
        Text win_text = win_object.GetComponent<Text>();
        win_text.text = win3.ToString() + "枚WIN!!";

        // ボーナスゲーム中なら獲得メダル加算
        if(m_modeBonus > 0)
        {
            m_getMedalCnt += win3;

            // 獲得枚数が規定数超えたら終了
            if(m_getMedalCnt > m_maxGetMedalNum)
            {
                // 終わりの演出
                m_modeBonus = 0;
                win_text.text = "BONUS END. 獲得枚数: " + m_getMedalCnt;

                //とりあえず5秒待つ
                StartCoroutine("sleep");

                m_getMedalCnt = 0;

                // フラグリセット
                m_uRoleType = 0;
            }
        }
    }
    IEnumerator sleep()
    {
        yield return new WaitForSeconds(5.0f);
    }
    void WinCherry()
    {
        coin += 4;
        double win4 = 4;
        Text win_text = win_object.GetComponent<Text>();
        win_text.text = win4.ToString() + "枚WIN!!";
    }
    void WinWatermelon()
    {
        coin += 5;
        double win5 = 5;
        Text win_text = win_object.GetComponent<Text>();
        win_text.text = win5.ToString() + "枚WIN!!";
    }

    void WinReplay()
    {
        coin += 3;
        double win6 = 3;
        Text win_text = win_object.GetComponent<Text>();
        win_text.text = win6.ToString() + "枚WIN!!";
    }
}