using UnityEngine;
public class PipeMovement : MonoBehaviour
{
    public float speed = 2f; // Speed at which the pipes move
    public float leftBoundary = -10f; // The x-position at which pipes get destroyed
    void Update()
    {
        // Move the pipes to the left
        transform.position += Vector3.left * speed * Time.deltaTime;
        // Destroy the pipe when it goes off-screen
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}