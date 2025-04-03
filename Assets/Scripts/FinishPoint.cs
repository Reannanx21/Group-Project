using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public bool goToPreviousLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goToPreviousLevel)
            {
                SceneController.instance.goToNextLevel = false;
            }
            else
            {
                SceneController.instance.goToNextLevel = true;
            }


            SceneController.instance.LoadLevel();
        }
    }
}
