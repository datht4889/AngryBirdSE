using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SelectAmmoManager : MonoBehaviour
{
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

    public void PlayDefault()
    {   if (currentNumberOfAmmo == maxNumberOfAmmo)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
       
    }

    public List<AmmoMechaism> getAmmoPrefabs()
    {
        return ammoPrefabs;
    }
}