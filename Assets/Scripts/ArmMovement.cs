using UnityEngine;

// Makes the arm swing back and forth
public class ArmMovement : MonoBehaviour
{
    public float speed = 2f;
    public float angle = 25f;

    public bool reverse = false; // NEW

    void Update()
    {
        float rotation = Mathf.Sin(Time.time * speed) * angle;

        if (reverse)
            rotation *= -1f;

        transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }
}