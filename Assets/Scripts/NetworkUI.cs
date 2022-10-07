using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField joinCodeInput;
    [SerializeField]
    private TMP_InputField PlayerNameInput;

    public GameObject panel;
    public GameObject codePanel;
    public GameObject networkManager;
    [SerializeField]
    private TextMeshProUGUI code;
    private void Start()
    {
        this.panel.SetActive(true);
        this.codePanel.SetActive(false);
        
    }

    public async void StartHost()
    {
        RelayTest relaytest = networkManager.GetComponent<RelayTest>();
        if (RelayTest.Instance.IsRelayEnabled)
            await RelayTest.Instance.AllocateRelayServerAndGetJoinCode();
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.StartHost();
        this.panel.SetActive(false);
        this.codePanel.SetActive(true);
        code.text = "Code: " + relaytest.RelayJoinCode + " "+relaytest.RelayPort + " "+relaytest.RelayIPv4Address;
    }

    public async void StartClient()
    {
        if (RelayTest.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
        {
            await RelayTest.Instance.JoinRelayServerFromJoinCode(joinCodeInput.text);
            //NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheckTwo;
            NetworkManager.Singleton.StartClient();
            this.panel.SetActive(false);
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // 要认证的客户端标识符
        var clientId = request.ClientNetworkId;

        // 用户代码定义的附加连接数据
        var connectionData = request.Payload;

        // 您的批准逻辑确定以下值
        response.Approved = true;
        response.CreatePlayerObject = true;

        // NetworkPrefab的prefab哈希值，如果为null则使用默认的NetworkManagerplayer prefab
        if (clientId == 0 )
        {
            response.PlayerPrefabHash = 3096513311;
        }
        else if (clientId == 1 || clientId ==2)
        {
            response.PlayerPrefabHash = 1500210176;
        }
        
        

        // 生成玩家对象的位置（如果为 null，则使用默认的 Vector3.zero）
        response.Position = new Vector3(15.0f,0.5f,-26.0f);

        // 旋转生成玩家对象（如果为 null，则使用默认的 Quaternion.identity）
        response.Rotation = Quaternion.identity;


        Debug.Log(clientId);
        // 如果需要其他批准步骤，请将其设置为 true，直到完成其他步骤
        // 一旦它从 true 转换为 false，将处理连接批准响应。
        response.Pending = false;
        
    }

    

}
