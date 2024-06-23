using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help3 : MonoBehaviour
{
    public GameObject PrevBtn;

    public GameObject Hoverleft;

    public static void LoadStart()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public static void PrevHelp1()
    {
        SceneManager.LoadScene(12,LoadSceneMode.Single);
    }

}
