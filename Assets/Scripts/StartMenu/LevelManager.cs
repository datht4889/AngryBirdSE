using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    public static LevelManager lvmn;

    public int map;
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

    private void Awake()
    {
        if (lvmn == null)
        {
            lvmn = this;
        }
    }

    public static void LoadStart()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public static void LoadMap1()
    {
        lvmn.map = 1;
        SceneManager.LoadScene(5,LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap2()
    {
        lvmn.map = 2;
        SceneManager.LoadScene(5,LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap3()
    {
        lvmn.map = 3;
        SceneManager.LoadScene(5,LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
