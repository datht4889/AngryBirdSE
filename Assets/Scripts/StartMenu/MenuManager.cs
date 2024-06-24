using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Panel;

    public GameObject Button;
    public GameObject AboutBtn;
    public GameObject userPage;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI goldText;



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

    public static void Help()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/Help/HelpBtn.unity",LoadSceneMode.Single);
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
        // Load Log in scene
        SceneManager.LoadScene("Assets/Scenes/StartMenu/Login.unity",LoadSceneMode.Single);
    }

    public void OpenShop(){
        SceneManager.LoadScene("Assets/Scenes/StartMenu/Shop.unity",LoadSceneMode.Single);
    }
    public static void OpenLevelSelect()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/LevelSelect.unity",LoadSceneMode.Single);
    }

    public void GetUserInfo(){
        if (userPage.activeSelf==false){
            StartCoroutine(DataManager.dataManager.GetName((string name) =>
            {
                usernameText.text = "Username: " + name;
            }));
            StartCoroutine(DataManager.dataManager.GetGold((int gold) =>
            {
                goldText.text = "Gold: " + gold.ToString();
            }));
            userPage.SetActive(true);
        } else{
            userPage.SetActive(false);
        }
    }
}
