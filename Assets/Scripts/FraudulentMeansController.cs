using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraudulentMeansController : MonoBehaviour
{
    private GameObject fraudulentMeansInfo;
    private GameObject selectCitizen;
    private Transform canvas;
    private GameObject helper;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        fraudulentMeansInfo = canvas.Find("FraudulentMeansInfo").gameObject;
        selectCitizen = canvas.Find("SelectCitizen").gameObject;
        helper = GameObject.FindGameObjectWithTag("Helper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFraudulentMeansInfo()
    {
        fraudulentMeansInfo.SetActive(true);
    }

    public void OpenSelectCitizen()
    {
        selectCitizen.SetActive(true);
    }

    public void SendTargerFraudullentMeans()
    {
        selectCitizen.GetComponent<OtherUIController>().targerFraudullentMeansItem = this.gameObject;
        helper.GetComponent<HelperUIController>().targerFraudullentMeansItem = this.gameObject;
        
    }
    public void SendTargerFraudullentMeansForContent()
    {
        fraudulentMeansInfo.GetComponent<ContentController>().targetItem = this.gameObject;
        fraudulentMeansInfo.GetComponent<ContentController>().ContentChange();
    }
}
