using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
    
[RequireComponent(typeof(Rigidbody2D))]



 

    
    
 

public class Movement : MonoBehaviour
{

    [SerializeField] AudioClip sfxJump;

    [SerializeField] AudioClip sfxVictory;

    private AudioSource audioSource;

    // Movement
    private float x;
    private bool jump = false;


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    // Respawn
    public float spawnAxeX = -2.16f;
    public float spawnAxeY = 0.2f;
    public float delais = 1f;

    public float Health = 3f;


    public float code = 0;

    // liste perso
    public GameObject[] characters;
    private int present = 0;    // le personnage en ce moment

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        

    }

    void Update()
    {

        x = Input.GetAxis("Horizontal");
        animator.SetFloat("x", Mathf.Abs(x));
        transform.Translate(Vector2.right * 0.9f * Time.deltaTime * x);


        if (x > 0f) spriteRenderer.flipX = false;
        if (x < 0f) spriteRenderer.flipX = true;

        // respawn si le perso tombe dans le vide
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector2(spawnAxeX, spawnAxeY);
        }

        // ---- saut ----
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            audioSource.PlayOneShot(sfxJump);
        }

        // ---- Attack animation ----
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }

        // ---- swap les personnages ----
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwapToNextCharacter();
        }

        
    }

    private void FixedUpdate()
    {

        transform.Translate(Vector2.right * 0.5f * Time.deltaTime * x);


        if (jump)
        {
            jump = false;
            audioSource.PlayOneShot(sfxJump);
            rb.AddForce(Vector2.up * 240f);
        }
    }

    public void Degat()
    {
        Health = Health - 1;
        Debug.Log("le joueur a " + Health + " point de vie");
        animator.SetTrigger("Death");
        StartCoroutine(Invincible());

        if (Health <= 0)
        {
            Debug.Log("joueur est mort");
            animator.SetTrigger("Death");
            StartCoroutine(RespawnJoueur());
            Health = 3;
            
        }
    }

    public void fin()
    {
        Debug.Log("Niveau terminé");
        animator.SetTrigger("Death");
        StartCoroutine(RespawnJoueurFini());
    }





    public void Heal()
    {
        Health = Health + 1;
        Debug.Log("le joueur a " + Health + " point de vie");
        animator.SetTrigger("Death");
        StartCoroutine(Invincible());

        if (Health <= 0)
        {
            Debug.Log("joueur est mort");
            Animator animation = GetComponent<Animator>();
            animation.SetTrigger("Death");
            StartCoroutine(RespawnJoueur());
            
            Health = 3;
        }
    }



    private IEnumerator RespawnJoueur()
    {
        yield return new WaitForSeconds(delais);
        transform.position = new Vector2(spawnAxeX, spawnAxeY);

         
          }

    private IEnumerator RespawnJoueurFini()
    {
        audioSource.PlayOneShot(sfxVictory);
        yield return new WaitForSeconds(delais);

        
        transform.position = new Vector2(spawnAxeX, spawnAxeY);
    }

 private IEnumerator RespawnJoueurScene()
    {
       
        yield return new WaitForSeconds(delais);

       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    private IEnumerator Invincible()
    {
        //change le tag afin de le rendre temporairement invincible pour eviter de mourir trop vite 
        transform.tag = "Untagged";
        yield return new WaitForSeconds(delais);
        transform.tag = "Player";


    }
   

    private void SwapToNextCharacter()
    {   //liste de personnage disponible
        if (characters.Length < 2) return;

        // prend le prochain personnage avec un truc trouver sur internet pour faire en sorte de pas aller out of bound 
        int prochain = (present + 1) % characters.Length;

        //met le perso qui seras sur le terrain dans une variable temporaire
        Vector3 temporaire = characters[prochain].transform.position;
        //met le perso qui est sur le terrain dans la case prochain afin de laisser la place au prochain
        characters[prochain].transform.position = characters[present].transform.position;
        //met le nouveau en position de perso actuel
        characters[present].transform.position = temporaire;

        // met le nouveau arrivant sur la position presente
        present = prochain;
    }
}
