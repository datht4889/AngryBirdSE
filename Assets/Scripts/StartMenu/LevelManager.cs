using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    public static LevelManager lvmn;

    public string map;
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
        SceneManager.LoadScene("Assets/Scenes/StartMenu/StartScene.unity",LoadSceneMode.Single);
    }

    public static void LoadEnd()
    {
        SceneManager.LoadScene("Assets/Scenes/Map/Congratulation.unity",LoadSceneMode.Single);
    }

    public static void LoadMap1()
    {
        lvmn.map = "Assets/Scenes/Map/Map1.unity";
        SceneManager.LoadScene("Assets/Scenes/StartMenu/AmmoSelect.unity",LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap2()
    {
        lvmn.map = "Assets/Scenes/Map/Map2.unity";
        SceneManager.LoadScene("Assets/Scenes/StartMenu/AmmoSelect.unity",LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public static void LoadMap3()
    {
        lvmn.map = "Assets/Scenes/Map/Map3.unity";
        SceneManager.LoadScene("Assets/Scenes/StartMenu/AmmoSelect.unity",LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public static void LoadMap4()
    {
        lvmn.map = "Assets/Scenes/Map/Map4.unity";
        SceneManager.LoadScene("Assets/Scenes/StartMenu/AmmoSelect.unity",LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public static void LoadMap5()
    {
        lvmn.map = "Assets/Scenes/Map/Map5.unity";
        SceneManager.LoadScene("Assets/Scenes/StartMenu/AmmoSelect.unity",LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
