using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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
                Debug.Log("nivesu fini");
                movement.fin();
            }
        
    }
}
}
