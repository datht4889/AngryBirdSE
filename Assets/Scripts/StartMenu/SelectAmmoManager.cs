using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SelectAmmoManager : MonoBehaviour
{
    public static LevelManager lvmn;
    public int map;
    private List<AmmoMechaism> ammoPrefabs = new List<AmmoMechaism> { };
    private int maxNumberOfAmmo = 3; //GameManager.instances.getMaxNumberOfAmmos();
    private int currentNumberOfAmmo = 0;
    AmmoMechaism ammoPrefab;
    public static SelectAmmoManager instances;
  
    private void Awake()
    {
        if (instances == null)
        {
            instances = this;
            map = lvmn.map;
        }
    }

        public void SelectAmmo()
    {   if (currentNumberOfAmmo < maxNumberOfAmmo)
        {

            GameObject ButtonRef = GameObject.FindGameObjectWithTag("SelectAmmo").GetComponent<EventSystem>().currentSelectedGameObject;

            int AmmoId = ButtonRef.GetComponent<ButtonInfo>().ItemID;
            if (AmmoId == 0)
            {
                ammoPrefab = Resources.Load<AmmoMechaism>("Ammo");
            }
            else if (AmmoId == 1)
            {
                ammoPrefab = Resources.Load<AmmoMechaism>("BiggerAmmo");
            }
            else if (AmmoId == 2)
            {
                ammoPrefab = Resources.Load<AmmoMechaism>("ExplosionAmmo");
            }
            if (ammoPrefab != null)
            {
                ammoPrefabs.Add(ammoPrefab);
                currentNumberOfAmmo++;
                Debug.LogError("add ammo to list");

            }
            else
            {
                Debug.LogError($"Ammo prefab 'Ammo {AmmoId}' could not be loaded. Check if the path and name are correct.");
            }
        }
        
    }

    public void RemoveAmmo()
    {
        if (currentNumberOfAmmo == 0){
            Debug.LogError("Can't delete");
            return;
        }
        ammoPrefabs.RemoveAt(ammoPrefabs.Count - 1);
        currentNumberOfAmmo--;
        Debug.LogError("Delete successfully");
    }
    


    public void PlayDefault()
    {   if (currentNumberOfAmmo == maxNumberOfAmmo)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
       
    }

    public void Play()
    {   if (currentNumberOfAmmo == maxNumberOfAmmo)
        {
            SceneManager.LoadScene(map+1, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
       
    }

    public List<AmmoMechaism> getAmmoPrefabs()
        
    {
        List<AmmoMechaism> copyAmmo = new List<AmmoMechaism>(ammoPrefabs);
        return copyAmmo;
    }
}
