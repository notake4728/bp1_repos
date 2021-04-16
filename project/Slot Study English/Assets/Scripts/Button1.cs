using UnityEngine;

public class Button1 : MonoBehaviour
{
    //private AudioSource audioSource;//オーディオの宣言
    public ReelController reelController;//reelcontrollerの使用
    public GameObject leftTopGetter;//centerMiddleGetterの宣言
    public GameObject leftMiddleGetter;//centerMiddleGetterの宣言
    public GameObject leftBottomGetter;//centerMiddleGetterの宣言

    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.Find("leftTopGetter"))
        {
            GameObject cloneObject1 = Instantiate(leftTopGetter);//左リール上段にインスタンス化
            GameObject cloneObject2 = Instantiate(leftMiddleGetter);//左リール中段にインスタンス化
            GameObject cloneObject3 = Instantiate(leftBottomGetter);//左リール下段にインスタンス化

            ReelGenerator reelGenerator1 = GameObject.Find("ReelGenerator").GetComponent<ReelGenerator>();//ReelGenerator1の取得

            cloneObject1.name = "leftTopGetter"; // クローンしたオブジェクトの名前を変更
            cloneObject1.transform.position = new Vector3(reelGenerator1.transform.position.x, 1.5f, 0.0f); // 座標を変更
            cloneObject2.name = "leftMiddleGetter"; // クローンしたオブジェクトの名前を変更
            cloneObject2.transform.position = new Vector3(reelGenerator1.transform.position.x, 0.2f, 0.0f);
            cloneObject3.name = "leftBottomGetter"; // クローンしたオブジェクトの名前を変更
            cloneObject3.transform.position = new Vector3(reelGenerator1.transform.position.x, -1.0f, 0.0f);
        }
    }

    public void OnMouseDown()
    {
        reelController.StopReel1();//リールを止める関数を実行

        //audioSource = gameObject.GetComponent<AudioSource>();//オーディオの取得
        //audioSource.Play();//ぴこっというオーディオの取得

        Sprite sprite = Resources.Load<Sprite>("【BP01】img_btn_push01_0");
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void OnMouseUp() 
    {
        Sprite sprite = Resources.Load<Sprite>("【BP01】img_btn_off01_1");
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void ReadyButton()
    {
        Sprite sprite = Resources.Load<Sprite>("【BP01】img_btn_on01_0");
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}