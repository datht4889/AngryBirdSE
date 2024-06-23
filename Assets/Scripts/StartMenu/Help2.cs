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
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public static void PrevHelp1()
    {
        SceneManager.LoadScene(11,LoadSceneMode.Single);
    }

    public static void NextHelp2(){
         SceneManager.LoadScene(13,LoadSceneMode.Single);
    }

}
