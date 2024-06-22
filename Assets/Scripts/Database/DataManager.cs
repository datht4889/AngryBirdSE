using System;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;
    public TMP_InputField logInUsername;
    public TMP_InputField logInPassword;
    public TMP_InputField signUpUsername;
    public TMP_InputField signUpPassword;
    public TMP_InputField signUpConfirmPassword;
    // public InputField gold;

    public GameObject logInPage;
    public GameObject signUpPage;
    // public GameObject forgortPasswordPage;
    public TextMeshProUGUI logInFailed;
    public TextMeshProUGUI signUpFailed;

    public string UserID;
    public DatabaseReference dbReference;

    void Awake()
    {
        // if (dataManager == null)
        // {
            dataManager = this;
            DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    void Start()
    {
        OpenLogIn();
        logInPassword.contentType = TMP_InputField.ContentType.Password;
        signUpPassword.contentType = TMP_InputField.ContentType.Password;
        signUpConfirmPassword.contentType = TMP_InputField.ContentType.Password;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized and connected to the database.");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });
    }


    public void OpenLogIn(){
        logInPage.SetActive(true);
        signUpPage.SetActive(false);
        // forgortPasswordPage.SetActive(false);
        TextMeshProUGUI logInUsernamePlaceholder = logInUsername.placeholder as TextMeshProUGUI;
        logInUsernamePlaceholder.text = "username";
        TextMeshProUGUI logInPasswordPlaceholder = logInPassword.placeholder as TextMeshProUGUI;
        logInPasswordPlaceholder.text = "password";
        logInFailed.color = Color.red;
        signUpFailed.color = Color.red;
    }
    public void OpenSignUp(){
        logInPage.SetActive(false);
        signUpPage.SetActive(true);
        // forgortPasswordPage.SetActive(false);
        TextMeshProUGUI signUpUsernamePlaceholder = signUpUsername.placeholder as TextMeshProUGUI;
        signUpUsernamePlaceholder.text = "username";
        TextMeshProUGUI signUpPasswordPlaceholder = signUpPassword.placeholder as TextMeshProUGUI;
        signUpPasswordPlaceholder.text = "password";
    }

    // public void OpenForgortPassword(){
    //     logInPage.SetActive(false);
    //     signUpPage.SetActive(false);
    //     forgortPasswordPage.SetActive(true);
    // }

    public void LogIn(){
        string enteredUsername = logInUsername.text;
        string enteredPassword = logInPassword.text;

        if(string.IsNullOrEmpty(enteredUsername)||string.IsNullOrEmpty(enteredPassword)){
            // logInFailed.SetActive(true);
            logInFailed.text = "Username and Password cannot be empty";
        }else{
            dbReference.Child("users").OrderByChild("username").EqualTo(enteredUsername).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Exists)
                    {
                        foreach (var userSnapshot in snapshot.Children)
                        {
                            IDictionary userDict = (IDictionary)userSnapshot.Value;
                            string storedPassword = userDict["password"].ToString();

                            if (storedPassword == enteredPassword)
                            {
                                UserID = userSnapshot.Key;
                                logInFailed.color = Color.green;
                                logInFailed.text = "Login successful!";
                                
                                // StartScene
                                SceneManager.LoadScene(0,LoadSceneMode.Single);
                                Time.timeScale = 1;
                            }
                            else
                            {
                                logInFailed.text = "Incorrect password. Please try again.";
                            }
                        }
                    }
                    else
                    {
                        logInFailed.text = "Username does not exist. Please register.";
                    }
                }
                else
                {
                    logInFailed.text = "Error log in: " + task.Exception;
                }
            });
        }
    }

    public void SignUp()
    {
        string enteredUsername = signUpUsername.text;
        string enteredPassword = signUpPassword.text;
        string confirmedPassword = signUpConfirmPassword.text;

        // Check if the username already exists
        if(string.IsNullOrEmpty(enteredUsername)||string.IsNullOrEmpty(enteredPassword)||string.IsNullOrEmpty(confirmedPassword)){
            // logInFailed.SetActive(true);
            signUpFailed.text = "Username and Password cannot be empty";
        } else{
            dbReference.Child("users").OrderByChild("username").EqualTo(enteredUsername).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Exists)
                    {
                        // Username already exists, display error message
                        signUpFailed.text = "Username already exists. Please choose another username.";
                    }
                    else
                    {
                        // Check if the password matches the confirmed password
                        if (enteredPassword.Equals(confirmedPassword))
                        {
                            // Passwords match, proceed with user creation
                            UserID = Guid.NewGuid().ToString();
                            User newUser = new User(enteredUsername, enteredPassword, 0);
                            string json = JsonUtility.ToJson(newUser);
                            dbReference.Child("users").Child(UserID).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
                            {
                                if (task.IsCompleted)
                                {
                                    logInFailed.color = Color.green;
                                    signUpFailed.color = Color.green;
                                    signUpFailed.text = "Sign up successfully! Please log in.";
                                    logInFailed.text = "Sign up successfully! Please log in.";
                                    // OpenLogIn();
                                }
                                else
                                {
                                    signUpFailed.text = "Error sign up: " + task.Exception;
                                }
                            });
                        }
                        else
                        {
                            // Passwords don't match, display error message
                            signUpFailed.text = "Confirm password doesn't match.";
                        }
                    }
                }
                else
                {
                    signUpFailed.text = "Error sign up: " + task.Exception;
                }
            });
        }
    }

    public void LogOut(){
        OpenLogIn();
    }

    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(UserID).Child("username").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null && userNameData.Result.Exists)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallback.Invoke(snapshot.Value.ToString());
        }
        else
        {
            Debug.LogError("Error retrieving username data");
        }
    }

    public IEnumerator GetGold(Action<int> onCallback)
    {
        var userGoldData = dbReference.Child("users").Child(UserID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null && userGoldData.Result.Exists)
        {
            DataSnapshot snapshot = userGoldData.Result;
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Error retrieving gold data");
        }
    }

    public IEnumerator GetAmmo(string ammoType, Action<bool> onCallback)
    {
        var userAmmoData = dbReference.Child("users").Child(UserID).Child(ammoType).GetValueAsync();

        yield return new WaitUntil(predicate: () => userAmmoData.IsCompleted);

        if (userAmmoData != null && userAmmoData.Result.Exists)
        {
            DataSnapshot snapshot = userAmmoData.Result;
            onCallback.Invoke(bool.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError($"Error retrieving {ammoType} data");
        }
    }

    public string GetUserInfo()
    {
        string goldText;
        StartCoroutine(GetName((string name) =>
        {
            string nameText = "Username: " + name;
        }));
        StartCoroutine(GetGold((int gold) =>
        {
            goldText = "Gold: " + gold.ToString();
        }));
        return $"Username: {logInUsername.text}, Password: {logInPassword.text}";
    }

    public void UpdateName(string newName)
    {
        // string newName = nameText.text.Replace("Username: ", "");
        dbReference.Child("users").Child(UserID).Child("username").SetValueAsync(newName).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Username updated successfully!");
            }
            else
            {
                Debug.LogError("Error updating username: " + task.Exception);
            }
        });
    }

    public void UpdateGold(int goldValue)
    {
        // string goldStr = goldText.text.Replace("Gold: ", "");
        StartCoroutine(GetGold((int gold) =>
        {
            if (goldValue.GetType() == typeof(int))
            {
                dbReference.Child("users").Child(UserID).Child("gold").SetValueAsync(gold+goldValue).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Gold updated successfully!");
                    }
                    else
                    {
                        Debug.LogError("Error updating gold: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("Invalid gold value");
            }
        }));
    }

    public void UpdateAmmo( string AmmoType)
    {
        // string goldStr = goldText.text.Replace("Gold: ", "");
        
            
       
                dbReference.Child("users").Child(UserID).Child(AmmoType).SetValueAsync(true).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log($"{AmmoType} updated successfully!");
                    }
                    else
                    {
                        Debug.LogError($"Error updating {AmmoType}: " + task.Exception);
                    }
                });
           
    }

}