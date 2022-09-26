using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraudTipController : MonoBehaviour
{
    private Transform canvas;
    private GameObject fraudTipInfo;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        fraudTipInfo = canvas.Find("FraudTipInfo").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenFraudTipInfo()
    {
        fraudTipInfo.SetActive(true);
    }

    public void SendTargerFraudTipForContent()
    {
        fraudTipInfo.GetComponent<ContentController>().targetItem = this.gameObject;
        fraudTipInfo.GetComponent<ContentController>().ContentChange();
    }

}
