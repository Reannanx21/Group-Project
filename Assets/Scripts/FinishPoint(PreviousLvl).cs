using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private bool goToPreviousLevel = false; // Flag to determine if it should go to the previous level

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goToPreviousLevel)
            {
                // Go to the previous level if the flag is true
                Debug.Log("Going to the Previous Level");
                SceneController.instance.PreviousLevel();
            }
            else
            {
                // Go to the next level if the flag is false
                Debug.Log("Going to the Next Level");
                SceneController.instance.NextLevel();
            }
        }
    }
}