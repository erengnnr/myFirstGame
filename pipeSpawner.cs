using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance;

    public GameObject pipePrefab;
    public float spawnInterval = 1.2f;
    public float minY = -1f;
    public float maxY = 3f;

    private bool spawning = false;

    void Awake() => instance = this;

    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnLoop());
        }

        ResumePipes();
    }

    public void StopSpawning()
    {
        spawning = false;
        StopAllCoroutines();
        PausePipes(); // ekle
    }


    IEnumerator SpawnLoop()
    {
        while (spawning)
        {
            float yPos = Random.Range(minY, maxY);
            Instantiate(pipePrefab, new Vector3(4f, yPos, 0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ResetPipes()
    {
        foreach (GameObject pipe in GameObject.FindGameObjectsWithTag("Pipe"))
            Destroy(pipe);
    }

    public void PausePipes()
    {
        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();
        foreach (PipeMovement pipe in pipes)
            pipe.Pause();
    }

    public void ResumePipes()
    {
        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();
        foreach (PipeMovement pipe in pipes)
            pipe.Resume();
    }
}
