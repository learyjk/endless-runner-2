using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float upForce = 75.0f;
    private Rigidbody2D rb;
    //public ParticleSystem upParticles;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float screenOffsetPercent = 0.85f;
    private Animator animator;

    public GameObject coinParticlesPrefab;
    public GameObject deathParticlesPrefab;

    public bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        float vertExtent = Camera.main.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;
        transform.position = new Vector3(horizExtent * screenOffsetPercent * (-1), 0f, 0f);
    }


    void FixedUpdate()
    {


        bool upForceApplied = Input.GetButton("Fire1");
        if (upForceApplied && !isDead)
        {
            rb.AddForce(new Vector2(0, upForce));
            //ParticleSystem clone = Instantiate(upParticles, new Vector2(transform.position.x, transform.position.y + spriteRenderer.bounds.extents.y/2), Quaternion.identity);
            //GameObject.Destroy(clone.gameObject, 2.0f);
            animator.SetTrigger("hasClicked");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Lightning"))
        {
            isDead = true;
            GameObject deathParticleClone = Instantiate(deathParticlesPrefab, this.transform.position, deathParticlesPrefab.transform.rotation);
            Destroy(deathParticleClone.gameObject, 3f);
            animator.SetTrigger("isBurned");
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(this.gameObject, 3f);
        }
        if(other.CompareTag("Plane"))
        {
            isDead = true;
            GameObject deathParticleClone = Instantiate(deathParticlesPrefab, this.transform.position, deathParticlesPrefab.transform.rotation);
            Destroy(deathParticleClone.gameObject, 3f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (other.CompareTag("Coin"))
        {
            GameManager.GetInstance().AddToScore(1);
            GameObject coinParticleClone = Instantiate(coinParticlesPrefab, other.transform.position, coinParticlesPrefab.transform.rotation);
            Destroy(other.gameObject);
            Destroy(coinParticleClone.gameObject, 3f);
        }
    }
}