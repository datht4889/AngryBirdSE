using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SelectAmmoManager : MonoBehaviour
{
    private List<bool> AmmoChecker = new List<bool> {true, true, false};
    private List<AmmoMechaism> ammoPrefabs = new List<AmmoMechaism> { };
    private List<SpriteRenderer> ammoImagePrefabs = new List<SpriteRenderer> { };
    private int maxNumberOfAmmo = 3; //GameManager.instances.getMaxNumberOfAmmos();
    private int currentNumberOfAmmo = 0;
    AmmoMechaism ammoPrefab;
    SpriteRenderer ammoImagePrefab;
    public static SelectAmmoManager instances;
    public string map;
  
    private void Awake()
    {
        if (instances == null)
        {
            instances = this;
            map = LevelManager.lvmn.map;
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
                ammoImagePrefab = Resources.Load<SpriteRenderer>("Ammo");
            }
            else if (AmmoId == 1)
            {
                ammoPrefab = Resources.Load<AmmoMechaism>("BiggerAmmo");
                ammoImagePrefab = Resources.Load<SpriteRenderer>("BiggerAmmo");
            }
            else if (AmmoId == 2)
            {
                ammoPrefab = Resources.Load<AmmoMechaism>("ExplosionAmmo");
                ammoImagePrefab = Resources.Load<SpriteRenderer>("ExplosionAmmo");
            }
            if (!AmmoChecker[AmmoId]){
                ammoPrefab = null;
            }
            if (ammoPrefab != null)
            {
                ammoPrefabs.Add(ammoPrefab);
                ammoImagePrefabs.Add(ammoImagePrefab);
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
        ammoImagePrefabs.RemoveAt(ammoImagePrefabs.Count - 1);
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

    public List<AmmoMechaism> getAmmoPrefabs()
    {
        List<AmmoMechaism> copyAmmo = new List<AmmoMechaism>(ammoPrefabs);
        return copyAmmo;
    }

    public List<SpriteRenderer> getAmmoImagePrefabs()
    {
        List<SpriteRenderer> copyImageAmmo = new List<SpriteRenderer>(ammoImagePrefabs);
        return copyImageAmmo;
    }

    public void Play()
    {   if (currentNumberOfAmmo == maxNumberOfAmmo)
        {
            SceneManager.LoadScene(map, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
       
    }
}