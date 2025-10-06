using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxVictory;
    [SerializeField] AudioClip sfxCheck;
    private AudioSource audioSource;
    private float x;
    private bool jump = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    public float spawnAxeX = -2.16f;
    public float spawnAxeY = 0.2f;
    public float delais = 1f;
    public float Health = 3f;
    public float code = 0;
    public Boolean alive = true;
    public GameObject[] characters;
    private int present = 0;
    private Vector2 spawnPoint;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spawnPoint = new Vector2(spawnAxeX, spawnAxeY);
    }

    void Update()
    {
        if (alive)
        {
            x = Input.GetAxis("Horizontal");
            animator.SetFloat("x", Mathf.Abs(x));
            transform.Translate(Vector2.right * 0.9f * Time.deltaTime * x);
            if (x > 0f) spriteRenderer.flipX = false;
            if (x < 0f) spriteRenderer.flipX = true;
            if (transform.position.y < -4.5f) transform.position = spawnPoint;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump = true;
                audioSource.PlayOneShot(sfxJump);
            }
            if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger("Attack");
            if (Input.GetKeyDown(KeyCode.F)) SwapToNextCharacter();
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

    public int GetCurrentHealth()
    {
        return Mathf.RoundToInt(Health);
    }

    public void Degat()
    {
        Health = Health - 1;
        animator.SetTrigger("Death");
        StartCoroutine(Invincible());
        if (Health <= 0)
        {
            animator.SetTrigger("Death");
            alive = false;
            StartCoroutine(RespawnJoueur());
            Health = 3;
        }
    }

    public void SetCheckpoint(Vector2 newSpawnPoint)
    {
        audioSource.PlayOneShot(sfxCheck);
        spawnPoint = newSpawnPoint;
    }

    public void RespawnPlayer(float delay)
    {
        alive = false;
        StartCoroutine(RespawnCoroutine(delay));
    }

    private IEnumerator RespawnCoroutine(float delay)
    {

        animator.SetTrigger("Death");


        transform.tag = "Untagged";


        yield return new WaitForSeconds(delay);


        transform.position = spawnPoint;

        alive = true;
        transform.tag = "Player";
    }

    public void fin()
    {
        animator.SetTrigger("Death");
        StartCoroutine(RespawnJoueurFini());
    }

    public void Heal()
    {
        Health = Health + 1;
        animator.SetTrigger("Death");
        StartCoroutine(Invincible());
        if (Health <= 0)
        {
            Animator animation = GetComponent<Animator>();
            animation.SetTrigger("Death");
            StartCoroutine(RespawnJoueur());
            Health = 3;
        }
    }

    private IEnumerator RespawnJoueur()
    {
        yield return new WaitForSeconds(delais);
        transform.position = spawnPoint;
    }

    private IEnumerator RespawnJoueurFini()
    {
        audioSource.PlayOneShot(sfxVictory);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }



    private IEnumerator Invincible()
    {
        transform.tag = "Untagged";
        yield return new WaitForSeconds(delais);
        transform.tag = "Player";
    }

    private void SwapToNextCharacter()
    {
        if (alive)
        {

            if (characters.Length < 2) return;
            int prochain = (present + 1) % characters.Length;
            Vector3 temporaire = characters[prochain].transform.position;
            characters[prochain].transform.position = characters[present].transform.position;
            characters[present].transform.position = temporaire;
            present = prochain;
            
        }
        else
        {
            Debug.Log("entraint de die");
        }
    }

 
}
