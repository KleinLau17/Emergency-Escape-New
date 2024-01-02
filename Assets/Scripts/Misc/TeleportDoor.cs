using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform destination; // 另一个门的Transform
    public float teleportDistance = 2f; // 触发传送的最大距离
    public KeyCode teleportKey = KeyCode.H; // 触发传送的按键

    private GameObject player;
    private bool isPlayerNearby = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(teleportKey))
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= teleportDistance)
            {
                TeleportPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void TeleportPlayer()
    {
        if (destination != null && player != null)
        {
            Vector3 newPosition = destination.position;
            newPosition.y += 1.2f; // 在y轴上加1
            player.transform.position = newPosition;
        }
    }
}
