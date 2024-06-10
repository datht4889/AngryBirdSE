using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // public static DataManager dataManager;
    public GameObject Panel;

    public GameObject Button;
    public GameObject userPage;
    public TextMeshProUGUI userInfo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings()
    {
        Panel.GetComponent<Animator>().SetTrigger("Pop");
    }

    public static void LoadLevel()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    public static void PlayDefault()
    {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public static void Quit()
    {
        Application.Quit();
    }

    public void MuteAudio()
    {
        Panel.GetComponent<Animator>().SetTrigger("Mute");
    }

    public void UnMuteAudio()
    {   
        Panel.GetComponent<Animator>().SetTrigger("Unmute");
        
    }

    public void LogOut(){
        // DataManager.dataManager.LogOut();
    }

    public void GetUserInfo(){
        // StartCoroutine(DataManager.dataManager.GetGold((int gold) =>
        // {
        //     userInfo.text = "Gold: " + gold.ToString();
        // }));
        Debug.Log("info");
        string userInfoText = DataManager.dataManager.GetUserInfo();
        userInfo.text = userInfoText;
        Debug.Log(userInfoText);
    }
}
