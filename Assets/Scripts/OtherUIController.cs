using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OtherUIController : MonoBehaviour
{
    private GameObject helper;
    private GameObject citizen;
    public GameObject targerFraudullentMeansItem;
    // Start is called before the first frame update
    void Start()
    {
        helper = GameObject.FindGameObjectWithTag("Helper");
        citizen = GameObject.FindGameObjectWithTag("Citizen");
        //Debug.Log(citizen.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyTargetInvestigateSpot()
    {
        Destroy(helper.GetComponent<HelperUIController>().targetinvestigateSpot);
        this.gameObject.SetActive(false);
    }

    public void DestroyTargerFraudullentMeansItem()
    {
        Destroy(this.targerFraudullentMeansItem);
    }

    public void CallHelperToGenerateFraudullentMeansItem()
    {
        helper.GetComponent<HelperUIController>().FraudullentMeansItemGeneration();
    }

    public void CallHelperToGenerateIncidentOccurs()
    {
        helper.GetComponent<HelperUIController>().IncidentOccursGeneration();
    }

    public void IncidentOccursTextChange()
    {
        this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "ÑI¿Ì÷–......";
    }

    public void IncidentCitizenButtonOneOnClick()
    {
        helper.GetComponent<HelperUIController>().HelperSendIncidentInfoToCitizen1();
    }
    public void IncidentCitizenButtonTwoOnClick()
    {
        helper.GetComponent<HelperUIController>().HelperSendIncidentInfoToCitizen2();
    }
    public void helperQuizButtonOneOnClick()
    {
        citizen.GetComponent<CitizenUIController>().CitizenResponseToHelper();
    }
    public void helperQuizButtonTwoOnClick()
    {
        citizen.GetComponent<CitizenUIController>().CitizenResponseToHelper();
    }
    public void helperQuizButtonThreeOnClick()
    {
        citizen.GetComponent<CitizenUIController>().CitizenResponseToHelper();
    }



}
