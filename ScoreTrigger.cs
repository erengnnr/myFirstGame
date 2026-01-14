using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Player"))
        {
            scored = true;

            if(ScoreManager.instance != null)
                ScoreManager.instance.AddScore(1);
            else
                Debug.LogError("ScoreManager instance null!");
        }
    }
}
