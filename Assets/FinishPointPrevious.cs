using UnityEngine;

public class FinishPointPrevious : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call the PreviousLevel method from the SceneController
            SceneController.instance.PreviousLevel();
        }
    }
}