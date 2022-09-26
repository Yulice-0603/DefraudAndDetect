using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class DestinationController : MonoBehaviour
{
    Transform canvas;
    TextMeshProUGUI destinationInfo;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;　//CanvasのTransfromの取得
        destinationInfo = canvas.transform.Find("DestinationInfo").GetChild(1).GetComponent<TextMeshProUGUI>(); //目的地の名前を取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突コライダーの対象がプレーヤーなら、詐欺Tip画面を生成し表示する
        if (other.gameObject.tag=="Citizen" && other.GetComponent<NetworkObject>().IsOwner)
        {
            other.GetComponent<CitizenUIController>().FraudTipGeneration();
            other.GetComponent<CitizenUIController>().FraudTipItemGeneration();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Citizen" && other.GetComponent<NetworkObject>().IsOwner)
        {
            Destroy(this.gameObject); //この目的地を削除する
            other.GetComponent<CitizenUIController>()._sendButton.SetActive(true); //メッセージ送信ボタンを表示する
            destinationInfo.text = "???";
        }
            
    }


}
