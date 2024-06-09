using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    public GameObject PrevBtn;
    
    public GameObject Map1Btn;

    public GameObject Map2Btn;

    public GameObject Map3Btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadStart()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public static void LoadMap1()
    {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap2()
    {
        SceneManager.LoadScene(3,LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap3()
    {
        SceneManager.LoadScene(4,LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
