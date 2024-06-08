using UnityEngine;


public class User: MonoBehaviour {
    public string username;
    public string password;
    public int gold;

    public User() {
    }

    public User(string username, string password, int gold) {
        this.username = username;
        this.password = password;
        this.gold = gold;
    }

}