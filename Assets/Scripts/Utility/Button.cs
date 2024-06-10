using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public static Button button;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject PauseButton;
    public GameObject PausePanel;

    public GameObject SettingPanel;

    public GameObject SettingButton;   

    private void Awake(){
        if (button == null)
        {
            button = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PauseGame() {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame() {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit2Menu(){
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void Settings()
    {
        SettingPanel.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void MuteAudio()
    {
        SettingPanel.GetComponent<Animator>().SetTrigger("Mute");
    }

    public void UnMuteAudio()
    {   
        SettingPanel.GetComponent<Animator>().SetTrigger("Unmute");
        
    }
}