using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Animator animator;
    private Collider2D lightningCollider;
    private SpriteRenderer sr;

    private float speed;

    private float toggleInterval;
    private float timeUntilNextToggle;
    private bool isLightningOn = false;

    void Start()
    {
        toggleInterval = Random.Range(0.5f, 3f);
        timeUntilNextToggle = toggleInterval;
        lightningCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        speed = GameManager.GetInstance().GetObjectSpeed();
    }

    void Update()
    {
        timeUntilNextToggle -= Time.deltaTime;

        if (timeUntilNextToggle <= 0)
        {
            isLightningOn = !isLightningOn;
            lightningCollider.enabled = isLightningOn;

            if (isLightningOn)
            {
                animator.SetBool("activateLightning", true);
            }
            else
            {
                animator.SetBool("activateLightning", false);
            }
            timeUntilNextToggle = toggleInterval;
        }
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            transform.Translate(new Vector3(speed, 0, 0));
        }
    }
}
