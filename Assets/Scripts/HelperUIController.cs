using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class HelperUIController : NetworkBehaviour
{
    //ヘルパー
    [SerializeField]
    GameObject investigateSpotPrefab;
    [SerializeField]
    GameObject fraudulentMeansInfoPrefab;
    private GameObject _fraudulentMeansInfo;
    [SerializeField]
    GameObject selectCitizenPrefab;
    private GameObject _selectCitizen;
    private Button shareCitizenButtonOne;
    private Button shareCitizenButtonTwo;
    private Button incidentCitizenButtonOne;
    private Button incidentCitizenButtonTwo;
    [SerializeField]
    GameObject investigateButtonPrefab;
    private GameObject _investigateButton;
    [SerializeField]
    GameObject shareButtonPrefab;
    private GameObject _shareButton;
    [SerializeField]
    GameObject fraudulentMeansItemPrefab;
    private GameObject _fraudulentMeanItem;
    [SerializeField]
    GameObject fraudTipItemPrefab;
    private GameObject _fraudTipItem;
    [SerializeField]
    GameObject incidentOccursPrefab;
    private GameObject _incidentOccurs;
    [SerializeField]
    GameObject helperQuiz;
    private int shareTargetClientId;
    private int incidentTargetClientId;
    public GameObject targetinvestigateSpot;
    public GameObject targerFraudullentMeansItem;
    private Transform canvas;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        if (this.IsOwner)
        {
            if (this.gameObject.tag == "Helper")
            {
                isActive = true;
                canvas = GameObject.Find("Canvas").transform;
                InvestigateSpotGeneration();
                InvestigateButtonGeneration();
                ShareButtonnGeneration();
                FraudulentMeansInfoGeneration();
                SelectCitizenGeneration();
                _fraudulentMeansInfo.transform.SetParent(canvas, false);
                _fraudulentMeansInfo.SetActive(false);
                _investigateButton.transform.SetParent(canvas, false);
                _investigateButton.SetActive(false);
                _selectCitizen.transform.SetParent(canvas, false);
                _selectCitizen.SetActive(false);
                shareCitizenButtonOne = _selectCitizen.transform.GetChild(1).GetComponent<Button>();
                shareCitizenButtonTwo = _selectCitizen.transform.GetChild(2).GetComponent<Button>();
                shareCitizenButtonOne.onClick.AddListener(HelperShareToCitizen1);
                shareCitizenButtonTwo.onClick.AddListener(HelperShareToCitizen2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsOwner)
        {
            if (isActive == true) _shareButton.SetActive(true);
            else _shareButton.SetActive(false);
        }
            
    }

    //調査スポットの生成
    private void InvestigateSpotGeneration()
    {
        int count = 0;
        GameObject _investigate1 = Instantiate(investigateSpotPrefab, new Vector3(0f, 1.0f, 5.0f), Quaternion.identity);
        _investigate1.name = "investigate1";
        float random1 = Random.Range(0, 5); if (random1 <= 2 && count < 2) { _investigate1.tag = "Fraud"; count++; }
        GameObject _investigate2 = Instantiate(investigateSpotPrefab, new Vector3(3.0f, 1.0f, 5.0f), Quaternion.identity);
        _investigate2.name = "investigate2";
        float random2 = Random.Range(0, 5); if (random2 <= 2 && count < 2) { _investigate2.tag = "Fraud"; count++; }
        GameObject _investigate3 = Instantiate(investigateSpotPrefab, new Vector3(-3.0f, 1.0f, 5.0f), Quaternion.identity);
        _investigate3.name = "investigate3";
        float random3 = Random.Range(0, 5); if (random3 <= 2 && count < 2) { _investigate3.tag = "Fraud"; count++; }
        GameObject _investigate4 = Instantiate(investigateSpotPrefab, new Vector3(6.0f, 1.0f, 5.0f), Quaternion.identity);
        _investigate4.name = "investigate4";
        float random4 = Random.Range(0, 5); if (random4 <= 2 && count < 2) { _investigate4.tag = "Fraud"; count++; }
        GameObject _investigate5 = Instantiate(investigateSpotPrefab, new Vector3(-6.0f, 1.0f, 5.0f), Quaternion.identity);
        _investigate5.name = "investigate5";
        float random5 = Random.Range(0, 5); if (random5 <= 2 && count < 2) { _investigate5.tag = "Fraud"; count++; }
    }

    private void FraudulentMeansInfoGeneration()
    {
        _fraudulentMeansInfo = Instantiate(fraudulentMeansInfoPrefab);
        _fraudulentMeansInfo.name = "FraudulentMeansInfo";
    }

    private void SelectCitizenGeneration()
    {
        _selectCitizen = Instantiate(selectCitizenPrefab);
        _selectCitizen.name = "SelectCitizen";
    }
    private void InvestigateButtonGeneration()
    {
        _investigateButton = Instantiate(investigateButtonPrefab);
        _investigateButton.name = "InvestigateButton";
    }
    private void ShareButtonnGeneration()
    {
        _shareButton = Instantiate(shareButtonPrefab);
        _shareButton.name = "ShareButton";
    }
    public void FraudullentMeansItemGeneration()
    {
        if (targetinvestigateSpot.tag == "Fraud")
        {
            _fraudulentMeanItem = Instantiate(fraudulentMeansItemPrefab);
            _fraudulentMeanItem.name = "FraudullentMeansItem";
            //_fraudulentMeanItem.transform.SetParent(_shareButton.transform.GetChild(1).GetChild(0).GetChild(0), false);
        }
    }
    public void IncidentOccursGeneration()
    {
        if (targetinvestigateSpot.tag == "Blank")
        {
            float random = Random.Range(0f, 2.0f);
            if (random<0.5f)
            {
                _incidentOccurs = Instantiate(incidentOccursPrefab);
                _incidentOccurs.name = "IncidentOccurs";
                isActive = false;
                Debug.Log("IncidentOccurs is Create");
            }
        }
        
    }

    [ClientRpc]
    private void CitizenGenerateFraudTipItemClientRpc(string targerFraudullentMeansItemName, ClientRpcParams clientRpcParams = default)
    {

        _fraudTipItem = Instantiate(fraudTipItemPrefab);
        _fraudTipItem.name = "FraudTipItem";
        _fraudTipItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = targerFraudullentMeansItemName;
    }


    [ServerRpc(RequireOwnership = false)]
    private void HelperShareInfoServerRpc(string targerFraudullentMeansItemName, int targetClientId, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];

            if (targetClientId == 1)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 1 }
                    }
                };
                CitizenGenerateFraudTipItemClientRpc(targerFraudullentMeansItemName, clientRpcParams);
            }
            else if (targetClientId == 2)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 2 }
                    }
                };
                CitizenGenerateFraudTipItemClientRpc(targerFraudullentMeansItemName, clientRpcParams);
            }
        }
    }
    public void HelperShareToCitizen1()
    {
        shareTargetClientId = 1;
        HelperShareInfoServerRpc(targerFraudullentMeansItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, shareTargetClientId);
    }
    public void HelperShareToCitizen2()
    {
        shareTargetClientId = 2;
        HelperShareInfoServerRpc(targerFraudullentMeansItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, shareTargetClientId);
    }

    [ClientRpc]
    private void CitizenGenerateHelperQuizClientRpc(ClientRpcParams clientRpcParams = default)
    {

        Instantiate(helperQuiz);
    }


    [ServerRpc(RequireOwnership = false)]
    private void HelperSendIncidentInfoServerRpc( int targetClientId, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];

            if (targetClientId == 1)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 1 }
                    }
                };
                CitizenGenerateHelperQuizClientRpc(clientRpcParams);
            }
            else if (targetClientId == 2)
            {
                ClientRpcParams clientRpcParams = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { 2 }
                    }
                };
                CitizenGenerateHelperQuizClientRpc(clientRpcParams);
            }

        }
    }
    public void HelperSendIncidentInfoToCitizen1()
    {
        Debug.Log("市民1に送りました");
        incidentTargetClientId = 1;
        HelperSendIncidentInfoServerRpc(incidentTargetClientId);
    }
    public void HelperSendIncidentInfoToCitizen2()
    {
        incidentTargetClientId = 2;
        HelperSendIncidentInfoServerRpc(incidentTargetClientId);
    }

}
