using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    // Toggle to determine if we should go to the next or previous level
    public bool goToPreviousLevel = false; // Default is false, which will go to the next level

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goToPreviousLevel)
            {
                // Load previous level if the toggle is true
                SceneController.instance.goToNextLevel = false; // Set the SceneController to load the previous level
            }
            else
            {
                // Load next level if the toggle is false
                SceneController.instance.goToNextLevel = true; // Set the SceneController to load the next level
            }

            // Call the SceneController to load the level based on the toggle
            SceneController.instance.LoadLevel();
        }
    }
}