using UnityEngine;

public class Fruit : MonoBehaviour
{
   
    void Start()
    {

    }

    
    void Update()
    {
        

    }
    
    void OnCollisionEnter2D(Collision2D other)
{
        if (other.collider.CompareTag("Player"))
        {

            Movement movement = other.collider.GetComponent<Movement>();
            if (movement != null)
            {
                movement.Heal();
            }
        Destroy(gameObject, 0.1f);
    }
}
}
