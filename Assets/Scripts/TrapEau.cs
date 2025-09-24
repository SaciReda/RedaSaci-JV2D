using UnityEngine;

public class TrapEau : MonoBehaviour
{
    public float spawnAxeX = -2.16f;
    public float spawnAxeY = 0.2f;
    public float delais = 1f; // le temps que l'animation joue avant que le respawn s'Acctionne 
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.GetComponent<Movement>().code != 1)
        {
           
            
            Animator animation = other.GetComponent<Animator>();
            //trigger trouver sur internet a l'air plus utile que un boolean
            animation.SetTrigger("Death");
            // coroutine une chose trouver sur internet pour delay le respawn et faire en sorte que l'animation joue
            StartCoroutine(RespawnJoueur(other.gameObject));
        }
    }

    private System.Collections.IEnumerator RespawnJoueur(GameObject player)
    {
        // attend que l'animation joue avec une seconde dans la variable delais
        yield return new WaitForSeconds(delais);

        //fait respawn le joueur
        player.transform.position = new Vector2(spawnAxeX, spawnAxeY);
    }
}
