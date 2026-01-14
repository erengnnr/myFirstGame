using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool isPaused = false;

    void Update()
    {
        if (!isPaused)
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -3f)
            Destroy(gameObject);
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
