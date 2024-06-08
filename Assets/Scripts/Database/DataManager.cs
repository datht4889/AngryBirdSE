using Firebase;
using Firebase.Database;
using UnityEngine;


public class DataManager: MonoBehaviour {
    
    void Start() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

}