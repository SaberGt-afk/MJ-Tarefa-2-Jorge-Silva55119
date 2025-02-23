using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTutorialUpdates : MonoBehaviour
{
  // Variables related to player character movement
  public InputAction MoveAction;
  Rigidbody2D rigidbody2d;
  Vector2 move;
  public float speed = 3.0f;


  // Variables related to the health system
  public int maxHealth = 5;
  public int health { get { return currentHealth; }}
  int currentHealth = 1;
    void Start()
    {
      MoveAction.Enable();
      rigidbody2d = GetComponent<Rigidbody2D>();
      //currentHealth = maxHealth;
    }

    // Update is called once per frame
  void Update()
    {
      move = MoveAction.ReadValue<Vector2>();
      //Debug.Log(move);
    }


    void FixedUpdate()
    {
      Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
      rigidbody2d.MovePosition(position);
    }

     public void ChangeHealth (int amount)
    {
      currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
      Debug.Log(currentHealth + "/" + maxHealth);
    }
}