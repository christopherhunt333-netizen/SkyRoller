using UnityEngine;


public class DeathZone : MonoBehaviour
{
    public Transform target;

    public void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().GameOver();
        }
    }
}
