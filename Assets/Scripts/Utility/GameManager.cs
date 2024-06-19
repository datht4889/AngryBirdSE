using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instances;

    public int MaxNumberOfAmmos = 3;
    private int _usedNumberOfAmmos;

    private IconHandler _iconHandler;

    [SerializeField] private GameObject _restartScreenObject;
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;
    [SerializeField] private GameObject _star_final_1;
    [SerializeField] private GameObject _star_final_2;
    [SerializeField] private GameObject _star_final_3;

    // [SerializeField] private GameObject ScoreText;

    [SerializeField] private int No_Alien;

    [SerializeField] private GameObject _WinScene;
    [SerializeField] private GameObject _LoseScene;

    private List<Alien> _aliens = new List<Alien>();

    public int MAX_SCORE;

    private void Awake(){
        if (instances == null){
            instances = this;
        }

        _iconHandler = GameObject.FindObjectOfType<IconHandler>();

        // Get number of enemies
        Alien[] aliens = FindObjectsOfType<Alien>();
        for (int i =0; i < aliens.Length; i++){
            _aliens.Add(aliens[i]);
        }
        Glass[] _glasses = FindObjectsOfType<Glass>();
        Metal[] _metals = FindObjectsOfType<Metal>();
        Stone[] _stones = FindObjectsOfType<Stone>();
        Wood[] _woods = FindObjectsOfType<Wood>();
        No_Alien = aliens.Length;
        MAX_SCORE = 100*aliens.Length + 50*(_glasses.Length + _metals.Length + _stones.Length + _woods.Length);
        Debug.Log(MAX_SCORE);
    }

    #region AmmoSettings
    public void UseAmmo()
    {
        _usedNumberOfAmmos++;
        _iconHandler.UseAmmo(_usedNumberOfAmmos);
    }

    public int getMaxNumberOfAmmos()
    {
        return MaxNumberOfAmmos;
    }
    public bool HasEnoughAmmos()
    {
        if (_usedNumberOfAmmos < MaxNumberOfAmmos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region CheckAlien
    public void removeAlien(Alien alien){
        _aliens.Remove(alien);
        float percent = (float)ScoreScript.scoreValue / MAX_SCORE;
        if (percent < 0.9 && percent >= 0.66){
                AppearStar1();
        } else if(percent > 0 && percent < 0.66){
                AppearStar2();
        } else if (percent == 0){
                AppearStar3();
        }

        CheckForEndGame();
    }

    public bool CheckForEndGame(){
        
        if (_aliens.Count == 0){
            ScoreScript.scoreValue += 1000 * (MaxNumberOfAmmos - _usedNumberOfAmmos);
            WinGame();
            return true;
        }
        return false;
        
    }

    #endregion

    #region Win/Lose

    private void WinGame(){
        // int currentGold = int.Parse(goldText.text.Substring(6));
        int updateGold = ScoreScript.scoreValue;
        DataManager.dataManager.UpdateGold(updateGold);
        Time.timeScale = 0;
        _WinScene.SetActive(true);

    }

    public void LoseGame(){
        Time.timeScale = 0;
        _LoseScene.SetActive(true);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region star
    public void AppearStar1(){
        Destroy(_star1);
        Destroy(_star_final_1);
    }

    public void AppearStar2(){
        Destroy(_star2);
        Destroy(_star_final_2);
    }

    public void AppearStar3(){
        Destroy(_star3);
        Destroy(_star_final_3);
    }
    #endregion
}