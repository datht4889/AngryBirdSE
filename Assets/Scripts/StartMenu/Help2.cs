using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help2 : MonoBehaviour
{
    public GameObject PrevBtn;

    public GameObject Hoverright;
    
    public GameObject Hoverleft;

    public static void LoadStart()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/StartScene.unity",LoadSceneMode.Single);
    }

    public static void PrevHelp1()
    {
        SceneManager.LoadScene("Assets/Scenes/StartMenu/Help/HelpBtn.unity",LoadSceneMode.Single);
    }

    public static void NextHelp2(){
         SceneManager.LoadScene("Assets/Scenes/StartMenu/Help/HelpBtn 2.unity",LoadSceneMode.Single);
    }

}
