using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PathChoser pathChoser;
    [SerializeField] GameObject _resetWindow;

    [SerializeField] GameObject _backBtn;
    Animator menuAnimator;
    int CalendarSceneIndex = 1;
    private void Start() 
    {
        menuAnimator = gameObject.GetComponent<Animator>();
        _resetWindow.SetActive(false);
        _backBtn.SetActive(false);
    }
    public void OnOpenMenu()
    {
        menuAnimator.SetTrigger("MenuOpen");
        _backBtn.SetActive(true);
        SoundStatic.PlayMenuSound();
    }

    public void OnClosedMenu()
    {
        menuAnimator.SetTrigger("MenuClosed");
        _backBtn.SetActive(false);
        SoundStatic.PlayMenuSound();
    }

    public void OnExport()
    {
        if (pathChoser != null)
        {
            pathChoser.PathChose();
        }
        else
        {
            Debug.LogWarning("Can't finde exporter");
        }
    }

    public void OnResetAllBtn()
    {
        _resetWindow.SetActive(true);
        SoundStatic.PlayMenuSound();
    }
    public void OnExitBtn()
    {
        #if UNITY_EDITOR
        // Закрываем редактор
        SoundStatic.PlayMenuSound();
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        // Закрываем приложение
        SoundStatic.PlayMenuSound();
        Application.Quit();
    #endif

    }

    public void OnBackBtn ()
    {
        SoundStatic.PlayMenuSound();
        OnClosedMenu();  
    }

    public void OnReviewBtn()
    {
        SoundStatic.PlayMenuSound();
        SceneManager.LoadScene(CalendarSceneIndex);
    }
}
