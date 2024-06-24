using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{
    public GameObject PrevBtn;

    public GameObject Hover;

    public static void LoadStart()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/StartScene.unity",LoadSceneMode.Single);
    }
    public static void NextHelp1(){
         SceneManager.LoadScene("Assets/Scenes/StartMenu/Help/HelpBtn 1.unity",LoadSceneMode.Single);
    }
}
