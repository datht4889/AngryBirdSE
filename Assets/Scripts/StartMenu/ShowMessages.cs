using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class ShowSameMessage : MonoBehaviour
{
    private Texture newSprite; // Assign your Prefab in the Inspector

    public Texture newSprite1;

    public Texture newSprite2;

    public Texture newSprite3;

    private GameObject position;

    public GameObject position1;

    public GameObject position2;

    public GameObject position3;
    public TMP_Text messageText; // Assign in Inspector
    public List<UnityEngine.UI.Button> messageButtons; // Assign in Inspector

    public UnityEngine.UI.Button deleteButton;
    
    public int currentNumberOfAmmo = 0;
    private int maxNumberOfAmmo = 3;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        messageText.gameObject.SetActive(false); // Ensure the text is initially hidden

        // Add listeners to each button in the list
        foreach (UnityEngine.UI.Button button in messageButtons)
        {

            button.onClick.AddListener(() => ShowMessage("Add successfully", messageButtons.IndexOf(button)));
        }

        deleteButton.onClick.AddListener(() => ShowMessageDelete("Delete successfully"));
    }

    private void ShowMessage(string message, int index)
    {
        
        if (currentNumberOfAmmo == maxNumberOfAmmo){
            messageText.text = "Full of ammo slots";
            messageText.color = Color.red;
        } else{
        messageText.text = message;
        currentNumberOfAmmo++; 

        if (index == 0){
            newSprite = newSprite1;
        } else if (index == 1){
            newSprite = newSprite2;
        } else if (index == 2){
            newSprite = newSprite3;
        }

        if (currentNumberOfAmmo == 1){
            position = position1;
            
        } else if (currentNumberOfAmmo == 2){
            position = position2;
        }  else if (currentNumberOfAmmo == 3){
            position = position3;
        }
        
        RawImage imageComponent1 = position.GetComponent<RawImage>();
            if (imageComponent1 != null)
            {
                imageComponent1.texture = newSprite;
                Debug.Log("Sprite has been set.");
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer component found on target object.");
            }

            messageText.color = Color.green;
        // Set the message text
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1f); // Ensure alpha is 1
        messageText.gameObject.SetActive(true); // Show the text
        
        RawImage imageComponent = position.GetComponent<RawImage>();

        Color color = imageComponent.color;
        color.a = 1f; // Set the alpha value
        imageComponent.color = color;
        }


        // Cancel previous fade coroutine if exists
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Start the fade out coroutine
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private void ShowMessageDelete(string message)
    {
        if (currentNumberOfAmmo == 0){
            messageText.text = "No ammo in slots";
            messageText.color = Color.red;
            
        } else{
        messageText.text = message;
        currentNumberOfAmmo--; 
        messageText.color = Color.green;

        RawImage imageComponent = position.GetComponent<RawImage>();

        Color color = imageComponent.color;
        color.a = 0f; // Set the alpha value
        imageComponent.color = color;

        if (position == position3){
            position = position2;
        } else if (position == position2){
            position = position1;
        } else if (position == position1){
            position = null;
        }
        }
        // Set the message text
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1f); // Ensure alpha is 1
        messageText.gameObject.SetActive(true); // Show the text
        


        // Cancel previous fade coroutine if exists
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Start the fade out coroutine
        fadeCoroutine = StartCoroutine(FadeOut());
    }



    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0); // Wait for 3 seconds

        float fadeDuration = 1f; // Duration of the fade out effect
        float timer = 0f;
        Color originalColor = messageText.color;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration); // Calculate alpha value from 1 to 0
            messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Apply new color with faded alpha
            timer += Time.deltaTime;
            yield return null;
        }

        messageText.gameObject.SetActive(false); // Hide the text after fading out
    }
}
