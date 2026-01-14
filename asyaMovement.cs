using UnityEngine;
using UnityEngine.SceneManagement;

public class asyaMovement : MonoBehaviour
{
    [Header("Karakter Ayarları")]
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isAlive = false;
    private bool gameStarted = false;

    [Header("Animator")]
    public Animator anim;

    [Header("UI")]
    public GameObject tapButton;

    [Header("Sesler")]
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip deathClip;

    [Header("Başlangıç Pozisyonu")]
    public Transform startPos;

    private BGMovement[] grounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();

        rb.isKinematic = true;
        isAlive = false;
        gameStarted = false;
        tapButton.SetActive(true);

        // Ground objelerini al
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("grounds");
        grounds = new BGMovement[groundObjects.Length];
        for (int i = 0; i < groundObjects.Length; i++)
            grounds[i] = groundObjects[i].GetComponent<BGMovement>();
    }

    void Update()
    {
        // Oyun başlat
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            StartGame();

        // Zıplama
        if (isAlive && gameStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Zıplama sesi çal
            if (audioSource != null && jumpClip != null)
                audioSource.PlayOneShot(jumpClip);
        }

        // Animasyon
        if (anim != null && isAlive)
            anim.SetFloat("verticalSpeed", rb.velocity.y);
    }

    void StartGame()
    {
        tapButton.SetActive(false);
        rb.isKinematic = false;
        isAlive = true;
        gameStarted = true;

        if (PipeSpawner.instance != null)
            PipeSpawner.instance.StartSpawning();

        if (grounds != null)
            foreach (BGMovement g in grounds)
                if (g != null) g.StartMoving();

        if (anim != null)
            anim.SetBool("isDead", false);

        if (ScoreManager.instance != null)
            ScoreManager.instance.StartGame();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipe") || collision.CompareTag("Ceilingground"))
            Die();
    }

    void Die()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        tapButton.SetActive(true);
        gameStarted = false;

        // Ölme sesi çal
        if (audioSource != null && deathClip != null)
            audioSource.PlayOneShot(deathClip);

        if (PipeSpawner.instance != null)
        {
            PipeSpawner.instance.StopSpawning();
            PipeSpawner.instance.PausePipes();
        }

        if (grounds != null)
            foreach (BGMovement g in grounds)
                if (g != null) g.StopMoving();

        if (anim != null)
            anim.SetBool("isDead", true);

        if (ScoreManager.instance != null)
            ScoreManager.instance.GameOver();
    }

    // Sahneyi tamamen yeniden yükler
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
