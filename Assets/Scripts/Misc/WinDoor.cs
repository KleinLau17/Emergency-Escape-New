using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDoor : MonoBehaviour
{
    public Transform destination; 
    public float teleportDistance = 2f; 
    public KeyCode teleportKey = KeyCode.H;
    public string winSceneName = "Win";

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
            player.transform.position = destination.position;

            // Check if the scene named "Win" exists and load it
            if (SceneManager.GetSceneByName(winSceneName) != null)
            {
                GameEvents.Win?.Invoke();
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
           
            }
        }
    }
}
