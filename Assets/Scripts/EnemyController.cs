using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed = 1.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    // Private variables
    private Rigidbody2D rigidbody2d;
    private float timer;
    private int direction = 1;

    // Animator component reference
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  // Retrieve Animator component
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer
        timer -= Time.deltaTime;

        // Change direction when timer reaches zero
        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // FixedUpdate is used for physics calculations
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        
        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
            // Update animator for vertical movement
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction); // "direction" controls up/down movement
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            // Update animator for horizontal movement
            animator.SetFloat("Move X", direction); // "direction" controls left/right movement
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    // Handle collision with the player
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
