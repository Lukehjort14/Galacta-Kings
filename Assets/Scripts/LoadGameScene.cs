using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LoadGameScene : MonoBehaviour
{
    // Reference to the input field where the player types their name
    public TMP_InputField nameInputField;

    // Image component of the input field (used to change color)
    private Image inputFieldImage;

    // Normal and error colors for the input field
    public Color normalColor = Color.white;
    public Color errorColor = Color.red;

    void Start()
    {
        // Get the Image component from the input field if it exists
        if (nameInputField != null)
        {
            inputFieldImage = nameInputField.GetComponent<Image>();
        }
    }



    // Called when the Start button is clicked
    public void LoadScene()
    {
        Debug.Log("LoadScene clicked");

        // Print the current text inside the input field
        Debug.Log("Current text = [" + nameInputField.text + "]");

        // Check if the name is empty or just spaces
        if (string.IsNullOrWhiteSpace(nameInputField.text))
        {
            Debug.Log("Player must enter a name!");

            // If the image exists, blink red to show error
            if (inputFieldImage != null)
            {
                StopAllCoroutines(); // stop previous blinking if spam clicked
                StartCoroutine(BlinkRed());
            }

            return; // stop loading the scene
        }

        // Save the player's name (trim removes extra spaces)
        Player.playerName = nameInputField.text.Trim();
        LeaderboardData.playerName = nameInputField.text.Trim();

        LeaderboardData.playerScore = 0;
        LeaderboardData.roundNumber = 1;

        // Load the main game scene
        SceneManager.LoadScene("GameScene");
    }

    //loads the instructions scene
    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    // Coroutine to make the input field blink red when there's an error
    IEnumerator BlinkRed()
    {
        for (int i = 0; i < 3; i++) // number of blinks
        {
            inputFieldImage.color = errorColor; // turn red
            yield return new WaitForSeconds(0.2f); // wait

            inputFieldImage.color = normalColor; // back to normal
            yield return new WaitForSeconds(0.2f); // wait
        }
    }
}