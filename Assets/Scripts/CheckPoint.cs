using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public Vector2 checkpointPosition;

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("marche");
            
            Movement playerMovement = other.GetComponent<Movement>();
            if (playerMovement != null)
            {
               
              
                playerMovement.SetCheckpoint(checkpointPosition);
                Debug.Log("Checkpoint updated: " + checkpointPosition);
            }
        }
    }
}
