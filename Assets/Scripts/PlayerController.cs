using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5f;

    [SerializeField] private float spawnOffset = 0.9f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Vector2 lastMoveDirection = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Keyboard.current == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        Vector2 direction = Vector2.zero;

        if (Keyboard.current.wKey.IsPressed() || Keyboard.current.upArrowKey.IsPressed())
        {
            direction.y += 1;
        }

        if (Keyboard.current.sKey.IsPressed() || Keyboard.current.downArrowKey.IsPressed())
        {
            direction.y -= 1;
        }

        if (Keyboard.current.dKey.IsPressed() || Keyboard.current.rightArrowKey.IsPressed())
        {
            direction.x += 1;
        }

        if (Keyboard.current.aKey.IsPressed() || Keyboard.current.leftArrowKey.IsPressed())
        {
            direction.x -= 1;
        }

        //moveInput = direction.normalized;
        moveInput = direction;

        if (moveInput.sqrMagnitude > 0f)
        {
            lastMoveDirection = moveInput;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Arraste o prefab do projétil para o Inspector.");
            return;
        }

        Vector2 spawnPosition = rb.position + lastMoveDirection * spawnOffset;

        GameObject projectile = Instantiate(projectilePrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0f), Quaternion.identity);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        if (projectileRb == null)
        {
            Debug.LogError("O prefab precisa ter um rigidbody2d");

            Destroy(projectile);
            return; 
        }

        projectileRb.linearVelocity = lastMoveDirection * projectileSpeed;


    }
}
