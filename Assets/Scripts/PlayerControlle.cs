using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTutorialUpdates : MonoBehaviour
{
    public InputAction MoveAction;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable(); // Habilita a ação de input MoveAction
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * 0.1f;
        transform.position = position;
    }
}