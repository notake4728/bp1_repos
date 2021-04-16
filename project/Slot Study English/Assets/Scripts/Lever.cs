using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    //private AudioSource audioSource;//オーディオの宣言
    public ReelController reelController;//ReelControllerの宣言
    public GameController gameController;//GameControllerの宣言
    public Button1 button1;
    public Button2 button2;
    public Button3 button3;
    public GameObject win_object; // Textオブジェクト

    void Start()
    {
    }
    public void OnMouseDown()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();//オーディオの取得
        //audioSource.Play();//ガチャっというオーディオの取得

        Sprite sprite = Resources.Load<Sprite>("【BP01】img_btn_lever02");
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        //リールを回す関数を呼ぶ
        if(reelController.StartReel())
        {
            gameController.coin -= 3.0f;
            Text win_text = win_object.GetComponent<Text>();
            win_text.text = "";

            button1.ReadyButton();
            button2.ReadyButton();
            button3.ReadyButton();
        }
    }
    public void OnMouseUp()
    {
        Sprite sprite = Resources.Load<Sprite>("【BP01】img_btn_lever01");
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
