using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentController : MonoBehaviour
{
    public GameObject targetItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContentChange()
    {
        if (targetItem == null) return;
        
        string ItemName = targetItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        if (ItemName == "還付金詐欺")
        {
            this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "還付金詐欺\n医療費、税金、保険料等について、「還付金があるので手続きしてください」などと言って、被害者にATMを操作させ、被害者の口座から犯人の口座に送金させる手口です。";
        }
        else if(ItemName == "時間的切迫")
        {
            this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "時間的切迫\nもし詐欺で話を持ち掛けられたとしても、感情を落ち着かせて一度冷静になって考えれば「問題ない」と思えるかもしれない。しかし、加害者たちはその時間を与えない。時間的余裕を与えず、即座に振込みや支払いを要求してきる。";
        }
    }
}
