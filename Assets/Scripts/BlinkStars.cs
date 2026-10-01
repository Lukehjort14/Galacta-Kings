using UnityEngine;
using UnityEngine.UI;

// Makes a UI image fade in and out like blinking stars
public class BlinkStars : MonoBehaviour
{
    public float minAlpha = 0.3f; // lowest transparency value
    public float maxAlpha = 1f; // highest transparency value
    public float speed = 1f; // speed of blinking effect

    private Image sr; // reference to the UI Image component

    void Start()
    {
        sr = GetComponent<Image>(); // get the Image component on this object

        // check if Image component exists to avoid errors
        if (sr == null)
        {
            Debug.LogError("BlinkStars requires a SpriteRenderer!"); // log error if missing
            enabled = false; // disable script if no Image is found
        }
    }

    void Update()
    {
        float a = Mathf.Lerp( // smoothly change alpha between min and max
            minAlpha,
            maxAlpha,
            (Mathf.Sin(Time.time * speed) + 1f) / 2f
        );

        Color c = sr.color; // get current color
        c.a = a; // update alpha value
        sr.color = c; // apply new color back to image
    }
}