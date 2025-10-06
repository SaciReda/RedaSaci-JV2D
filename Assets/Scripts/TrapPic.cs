using UnityEngine;

public class TrapPic : MonoBehaviour
{
    public Movement players;
    public float delais = 1f; 
    
    

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player") && other.GetComponent<Movement>().code != 2)
    {
        Movement playerMovement = other.GetComponent<Movement>();
        if (playerMovement != null)
        {
           
            playerMovement.RespawnPlayer(delais);
        }
    }
}

    
}
