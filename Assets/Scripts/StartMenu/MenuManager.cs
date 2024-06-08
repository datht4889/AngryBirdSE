using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject Panel;

    public GameObject Button;


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
}
