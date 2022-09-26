using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class InvestigateSpotController : MonoBehaviour
{
    private Transform canvas;
    private GameObject investigateButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;　//CanvasのTransfromの取得
        investigateButton = canvas.transform.Find("InvestigateButton").gameObject; //調査ボタンを取得
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //調査スポットの衝突コライダーの対象がHelperなら、調査ボタンを表示します
        if (other.gameObject.tag == "Helper" && other.GetComponent<NetworkObject>().IsOwner && other.GetComponent<HelperUIController>().isActive == true)
        {
            investigateButton.SetActive(true);
            other.GetComponent<HelperUIController>().targetinvestigateSpot = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //離れた時、調査ボタンを隠す
        if (other.gameObject.tag == "Helper" && other.GetComponent<NetworkObject>().IsOwner && other.GetComponent<HelperUIController>().isActive == true)
        {
            investigateButton.SetActive(false);
        }

    }
}
