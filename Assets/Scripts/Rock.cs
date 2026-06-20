using UnityEngine;

public class Rock : MonoBehaviour
{
    float dragSpeed = 3f;
    float dragDuration = 2f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedDrag(dragSpeed, dragDuration);
            }
        }
    }
}
