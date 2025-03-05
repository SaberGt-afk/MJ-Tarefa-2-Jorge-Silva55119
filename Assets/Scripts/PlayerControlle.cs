using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variáveis de movimento do jogador
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    // Variáveis de saúde
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    // Variáveis de invencibilidade
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Variáveis de Animação
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    // Variáveis de Projetil
    public GameObject projectilePrefab;

    // Start é chamado antes da primeira atualização do frame
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
        }
    }

    // FixedUpdate é chamado na mesma taxa do sistema de física
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        Debug.Log("FindFriend foi chamado!"); // Verifica se a função foi ativada

        // Executa o Raycast para detectar o NPC na camada "NPC"
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.5f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            Debug.Log("Raycast atingiu: " + hit.collider.gameObject.name); // Mostra qual objeto foi atingido

            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                Debug.Log("NPC encontrado, chamando DisplayDialogue()"); // Confirma que encontrou o NPC
                UIHandler.instance.DisplayDialogue();
            }
            else
            {
                Debug.Log("O objeto atingido não tem o script NonPlayerCharacter!"); // Caso o NPC não tenha o script correto
            }
        }
        else
        {
            Debug.Log("Raycast não encontrou nenhum NPC!"); // Caso não tenha acertado nada
        }
    }
}
