using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float forwardSpeed = 8f;
    float sideSpeed = 6f;

    Rigidbody rb;

    Vector2 moveInput;

    float currentSideInput;
    float sideVelocity;

    float originalForwardSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalForwardSpeed = forwardSpeed;
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateSpeedBoost(float boostSpeed, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(boostSpeed, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostSpeed, float duration)
    {
        forwardSpeed = boostSpeed;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalForwardSpeed;
    }

    public void ActivateSpeedDrag(float dragSpeed, float duration)
    {
        StartCoroutine(SpeedDragRoutine(dragSpeed, duration));
    }

    private IEnumerator SpeedDragRoutine(float dragSpeed, float duration)
    {
        forwardSpeed = dragSpeed;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalForwardSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        currentSideInput = Mathf.SmoothDamp(
            currentSideInput,
            moveInput.x,
            ref sideVelocity,
            0.1f
        );

        Vector3 movement = new Vector3(
            currentSideInput * sideSpeed,
            rb.linearVelocity.y,
            forwardSpeed
        );

        rb.linearVelocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
