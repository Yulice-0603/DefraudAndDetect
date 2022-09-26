using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetParent : MonoBehaviour
{
    Transform canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        if (this.gameObject.tag == "FraudTipItem")
        {
            this.gameObject.transform.SetParent(canvas.transform.Find("Information").GetChild(1).GetChild(0).GetChild(0), false);
        }
        else if (this.gameObject.tag == "FraudulentMeansItem")
        {
            this.gameObject.transform.SetParent(canvas.transform.Find("ShareButton").GetChild(1).GetChild(0).GetChild(0), false);
        }
        else
        {
            this.gameObject.transform.SetParent(canvas, false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
