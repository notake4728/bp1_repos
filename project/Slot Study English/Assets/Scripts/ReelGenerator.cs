using UnityEngine;
using Define;
public class ReelGenerator : MonoBehaviour
{
    public GameObject[] imgobj; //絵柄のプレハブを格納
    public int[] reelSort;
    int reelMaxNum = 0;
    float reelWidth = CO.REEL_WIDTH;
    GameObject[] tmp_obj = {};//リールの配列（プレハブの種類が入る）
    Transform[] img_pos = {};//リールの柄の位置

    void Awake()//ゲーム開始時に
    {
        reelMaxNum = reelSort.GetLength(0);
        tmp_obj = new GameObject[reelMaxNum];//リールの配列（プレハブの種類が入る）
        img_pos = new Transform[reelMaxNum];//リールの柄の位置

        GenerateReel();//リール生成関数を呼び出す
    }

    public void GenerateReel()//リール生成関数本体
    {
        for(int i = 0; i < reelMaxNum; ++i)
        {
            Vector3 pos = new Vector3(0.0f, reelWidth * (i - 2), 0.0f);//プレハブ位置の決定
            tmp_obj[i] = (GameObject)Instantiate(this.imgobj[this.reelSort[i]]); //プレハブからtempの絵柄idのGameObjectを生成
            tmp_obj[i].transform.SetParent(transform, false); //リールのオブジェクトを親にする
            img_pos[i] = tmp_obj[i].GetComponent<Transform>();//プレハブのtransformを取得
            img_pos[i].localPosition = pos;//プレハブ位置の代入

            tmp_obj[i].GetComponent<ReelSymbol>().SetIndexOnReel(i);
        }
    }

    private void FixedUpdate()　//ゲームが始まったら
    {
        for(int i = 0; i < reelMaxNum; ++i)
        {
            if(img_pos[i])
            {
                if(img_pos[i].position.y < -reelWidth * 2.0f)
                {
                    float offY = img_pos[i].localPosition.y + (reelWidth * reelMaxNum);
                    img_pos[i].localPosition = new Vector3(img_pos[i].localPosition.x, offY, img_pos[i].localPosition.z);
                }
            }
        }
    }

    public int GetReelElmNum()
    {
        return reelMaxNum;
    }

    public void DestroyReel()
    {
        //リールをデストロイしてリセットする関数
        foreach(GameObject child in this.tmp_obj)
        {
            // 一つずつ破棄する
            Destroy(child);
        }
    }
}