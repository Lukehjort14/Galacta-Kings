using UnityEngine;

// Makes a sprite fade in and out like blinking stars
public class BlinkStarsSprite : MonoBehaviour
{
    public float minAlpha = 0.3f; // lowest transparency value (how dim the star gets)
    public float maxAlpha = 1f;   // highest transparency value (how bright the star gets)
    public float speed = 1f;      // speed of blinking effect

    private SpriteRenderer sr; // reference to the SpriteRenderer component

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // get the SpriteRenderer component on this object

        // check if SpriteRenderer exists to avoid errors
        if (sr == null)
        {
            Debug.LogError("BlinkStarsSprite requires a SpriteRenderer!"); // log error if missing
            enabled = false; // disable script if no renderer is found
        }
    }

    void Update()
    {
        // calculate alpha value using a smooth sine wave (creates fade in and out effect)
        float a = Mathf.Lerp(
            minAlpha,
            maxAlpha,
            (Mathf.Sin(Time.time * speed) + 1f) / 2f
        );

        Color c = sr.color; // get the current color of the sprite
        c.a = a;            // update the alpha (transparency)
        sr.color = c;       // apply the new color back to the sprite
    }
}