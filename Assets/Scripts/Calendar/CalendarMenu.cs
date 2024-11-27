using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalendarMenu : MonoBehaviour
{
    
    public void OnCloseBtn ()
    {
        SoundStatic.PlayMenuSound();
        SceneManager.LoadScene(0);    
    }
}
