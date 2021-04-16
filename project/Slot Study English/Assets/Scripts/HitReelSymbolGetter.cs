using UnityEngine;
public class HitReelSymbolGetter : MonoBehaviour
{
    int hitSymbol = -1;
    int hitIndex = -1;
    ReelController reelController;//Reelcontrollerの宣言
    private void Awake()
    {
        reelController = GameObject.Find("ReelController").GetComponent<ReelController>();//Reelcontrollerの取得
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("reel"))//reelタグが付いたオブジェクトにぶつかったら
        {
            ReelSymbol reelSymbol = other.GetComponent<ReelSymbol>(); //ぶつかった相手のReelSymbolにアクセス
            this.hitSymbol = reelSymbol.symbolType;  //symbolNumをcenterMiddleSymbolに代入する
            this.hitIndex = reelSymbol.GetIndexOnReel();
            Debug.Log("Hit Symbol" + this.hitSymbol);
        }
    }

    public int GetHitSymbolType()
    {
        return this.hitSymbol;
    }

    public int GetHitSymbolIndex()
    {
        return this.hitIndex;
    }
}