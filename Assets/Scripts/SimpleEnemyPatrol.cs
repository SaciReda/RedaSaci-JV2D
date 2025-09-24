using UnityEngine;
 
[RequireComponent(typeof(Rigidbody2D))]
public class SimpleEnemyPatrol : MonoBehaviour
{
    public Transform leftPoint, rightPoint;


    public float speed = 1f;
public int touchDamage = 1;
private bool toRight = true;
private Rigidbody2D rb;
private SpriteRenderer sr;
private Animator animator;
 
void Awake()
{
rb = GetComponent<Rigidbody2D>();
sr = GetComponent<SpriteRenderer>();
//  animator = GetComponent<Animator>();
}

    void FixedUpdate()
    {
        float dir = toRight ? 1f : -1f;
        
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        
        sr.flipX = !toRight;
        //  animator?.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
            

        if (toRight && transform.position.x >= rightPoint.position.x)
        {
            toRight = false;
            
        }
        else if (!toRight && transform.position.x <= leftPoint.position.x)
        {
            
            toRight = true;
            
        }
        
}
 
void OnCollisionEnter2D(Collision2D other)
{
    if (other.collider.CompareTag("Player"))
    {
        Movement movement = other.collider.GetComponent<Movement>();
        if (movement != null)
        {
            movement.Degat();
        }
    }
}
}