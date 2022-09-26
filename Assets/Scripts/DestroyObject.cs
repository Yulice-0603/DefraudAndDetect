using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    
    public void FraudClose()
    {
        Destroy(this.gameObject);
    }

    public void QuizClose()
    {
        Destroy(this.gameObject);
    }

    public void FraudulentMeansDestroy()
    {
        Destroy(this.gameObject);
    }

    public void IncidenOccursClose()
    {
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Helper").GetComponent<HelperUIController>().isActive = true;
    }

    
}
