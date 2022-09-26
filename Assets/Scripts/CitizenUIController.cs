using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;

public class CitizenUIController : NetworkBehaviour
{
    //市民
    [SerializeField]
    GameObject sendButtonPrefab;
    public GameObject _sendButton;
    private Button _send;
    [SerializeField]
    GameObject destinationInfoPrefab;
    private GameObject _destinationInfo;
    [SerializeField]
    GameObject informationPrefab;
    private GameObject _information;
    [SerializeField]
    GameObject fraudTipPrefab;
    private GameObject _fraudTip;
    [SerializeField]
    GameObject fraudTipInfoPrefab;
    private GameObject _fraudTipInfo;
    [SerializeField]
    GameObject fraudTipItemPrefab;
    private GameObject _fraudTipItem;

    private Button _helperQuizButtonOne;
    private Button _helperQuizButtonTwo;
    private Button _helperQuizButtonThree;

    TextMeshProUGUI destinationInfoTextTwo;
    [SerializeField]
    GameObject destinationPrefab;
    [SerializeField]
    GameObject citizenQuiz;
    private Transform canvas;
    void Start()
    {
        if (this.IsOwner)
        {
            if (this.gameObject.tag == "Citizen")
            {
                canvas = GameObject.Find("Canvas").transform;
                SendButtonGeneration(); //ゲーム開始時、メッセージ送信ボタンの生成
                DestinationInfoGeneration(); //ゲーム開始時、目的地情報UIの生成
                destinationInfoTextTwo = _destinationInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                InformationGeneration(); //ゲーム開始時、情報一覧の生成
                FraudTipInfoGeneration(); //ゲーム開始時、詐欺Tipの表示画面の生成
                DestinationGeneration(); //ゲーム開始時、目的地を生成
                DestinationNameDisplay(); //ゲーム開始時、目的地の名前を表示する
                _fraudTipInfo.transform.SetParent(canvas, false);
                _fraudTipInfo.SetActive(false);
                _send = _sendButton.GetComponent<Button>();
                _send.onClick.AddListener(CitizenSendMessageServer);
                _send.onClick.AddListener(DestinationGeneration);
                _send.onClick.AddListener(DestinationNameDisplay);


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //目的地の生成（市民）
    public void DestinationGeneration()
    {
        //目的地をランダムに一つ生成
        GameObject newObj = Instantiate(destinationPrefab, new Vector3(Random.Range(-6f, 6f), 1.0f, Random.Range(-6f, 6f)), Quaternion.identity);
        newObj.name = "Destination"; //名前を変更
        _sendButton.SetActive(false);　//メッセージ送信ボタンを隠す
    }

    //目的地情報UIの生成（市民）
    private void DestinationInfoGeneration()
    {
        _destinationInfo = Instantiate(destinationInfoPrefab);
        _destinationInfo.name = "DestinationInfo";
    }

    //情報一覧の生成（市民）
    private void InformationGeneration()
    {
        _information = Instantiate(informationPrefab);
        _information.name = "Information";
    }

    //メッセージ送信ボタンの生成（市民）
    private void SendButtonGeneration()
    {
        _sendButton = Instantiate(sendButtonPrefab);
        _sendButton.name = "SendButton";
    }

    //詐欺Tipの生成（市民）
    public void FraudTipGeneration()
    {
        _fraudTip = Instantiate(fraudTipPrefab);
        _fraudTip.name = "FraudTip1";
    }

    //詐欺Tipの表示画面の生成
    public void FraudTipInfoGeneration()
    {
        _fraudTipInfo = Instantiate(fraudTipInfoPrefab);
        _fraudTipInfo.name = "FraudTipInfo";
    }

    public void FraudTipItemGeneration()
    {
        _fraudTipItem = Instantiate(fraudTipItemPrefab);
        _fraudTipItem.name = "FraudTipItem";
        //_fraudTipItem.transform.SetParent(_information.transform.GetChild(1).GetChild(0).GetChild(0), false);
    }

    /// <Summary>
    /// 目的地の名前を表示する
    /// </Summary>
    public void DestinationNameDisplay()
    {
        destinationInfoTextTwo.text = "1";
    }

    //クライアント側で指定されたクライアントにクイズを生成する
    [ClientRpc]
    private void CitizenGenerateCitizenQuizClientRpc(ClientRpcParams clientRpcParams = default)
    {
        
        Instantiate(citizenQuiz);
    }

    //ClientIdを判断してターゲットクライアントを確定、サーバー側でClientRpcを呼び出す
    [ServerRpc(RequireOwnership = false)]
    private void CitizenSendMessageServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];

            if (client.ClientId == 1)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 2 }
                    }
                };
                CitizenGenerateCitizenQuizClientRpc(clientRpcParams);
            }
            else if (client.ClientId == 2)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 1 }
                    }
                };
                CitizenGenerateCitizenQuizClientRpc(clientRpcParams);
            }
        }
    }
    public void CitizenSendMessageServer()
    {
        CitizenSendMessageServerRpc();
    }

    [ClientRpc]
    private void HelperDestroyIncidentOccursClientRpc(ClientRpcParams clientRpcParams = default)
    {
        Debug.Log("ClientRpc実行しました");
        var _incidentOccurs = GameObject.FindGameObjectWithTag("IncidentOccurs");
        _incidentOccurs.GetComponent<DestroyObject>().IncidenOccursClose();
    }

    //ClientIdを判断してターゲットクライアントを確定、サーバー側でClientRpcを呼び出す
    [ServerRpc(RequireOwnership = false)]
    private void CitizenResponseToHelperServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { 0 }
                }
            };
            HelperDestroyIncidentOccursClientRpc(clientRpcParams);
        }
    }

    public void CitizenResponseToHelper()
    {
        CitizenResponseToHelperServerRpc();
    }
}