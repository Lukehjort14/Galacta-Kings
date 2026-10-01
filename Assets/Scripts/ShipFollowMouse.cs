using UnityEngine;

// Makes the player smoothly follow the mouse position
public class PlayerFollowMouse : MonoBehaviour
{
    public float followSpeed = 10f; // controls how fast the player follows the mouse

    private Vector3 targetPosition; // stores the target position to move toward

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // get mouse position on screen
        mousePosition.z = 10f; // set distance from camera for conversion

        targetPosition = Camera.main.ScreenToWorldPoint(mousePosition); // convert screen position to world position
        targetPosition.z = 0f; // lock movement to 2D plane

        transform.position = Vector3.Lerp( // smoothly move player toward target position
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}