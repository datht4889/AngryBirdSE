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
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
    public static void NextHelp1(){
         SceneManager.LoadScene(6,LoadSceneMode.Single);
    }
}
