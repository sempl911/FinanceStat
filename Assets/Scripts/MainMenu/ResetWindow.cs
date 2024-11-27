using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWindow : MonoBehaviour
{
    SQL_Loader sQL_Loader;

    void Start ()
    {
        sQL_Loader = gameObject.transform.parent.GetComponent<SQL_Loader>();
    }

    public void OnYesReset()
    {
        ResetAll();
        gameObject.SetActive(false);
    }
    public void OnNoReset()
    {
        gameObject.SetActive(false);
    }
    void ResetAll()
    {
        if (sQL_Loader != null)
        {
            sQL_Loader.OnResetAllBtn();
        }
        else
        {
            Debug.LogWarning("Can't finde SQL_Loader");
        }
    }
}
