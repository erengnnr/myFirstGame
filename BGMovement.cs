using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public float speed = 3f;
    public float width; // public yapıldı
    public BGMovement nextGround;

    private bool isMoving = false;
    private Vector3 startPos;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (nextGround != null)
        {
            Camera cam = Camera.main;
            float screenLeftEdge = cam.ScreenToWorldPoint(Vector3.zero).x;

            if (transform.position.x <= screenLeftEdge - width)
            {
                float newX = nextGround.transform.position.x + nextGround.width;
                newX = Mathf.Round(newX * 1000f) / 1000f;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x <= -width)
                transform.position += new Vector3(width * 2, 0, 0);
        }
    }

    public void StartMoving() => isMoving = true;
    public void StopMoving() => isMoving = false;

    public void ResetPosition()
    {
        transform.position = startPos;
        StopMoving();
    }
}
