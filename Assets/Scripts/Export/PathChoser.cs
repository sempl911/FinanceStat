using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChoser : MonoBehaviour
{
    ExelGenerator exelGenerator; 
    private void Start()
    {
        exelGenerator = gameObject.GetComponent<ExelGenerator>();
        
    }
    public void PathChose()
    {
        SaveToAppFolder();
        /*string filePath = Application.persistentDataPath + "/statisticExport.xlsx";
        

        string filePath = Application.persistentDataPath + "/statisticExport.xlsx";
        ShareXlsx share = new ShareXlsx();
        share.ShareFile(filePath);*/

        Debug.Log("Saving to external storage");
    }

    void SaveToAppFolder()
    {
       // exelGenerator.ExportToXlsx();
       // exelGenerator.GetPathAndSave(Application.persistentDataPath);
    }
}
