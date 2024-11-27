using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundStatic 
{
    public static void PlayBtnSound()
    {
        AudioSource btnSource = GameObject.FindGameObjectWithTag("BtnSound").GetComponent<AudioSource>();
        if (btnSource != null)
        {
            btnSource.Play();
        }
        else
        {
            Debug.LogWarning("Can't finde button sound");
        }
    }

    public static void PlayMenuSound()
    {
        AudioSource menuSource = GameObject.FindGameObjectWithTag("MenuSound").GetComponent<AudioSource>();
        if (menuSource != null)
        {
            menuSource.Play();
        }
        else
        {
            Debug.LogWarning("Can't finde menu sound");
        }
    }

    
}
