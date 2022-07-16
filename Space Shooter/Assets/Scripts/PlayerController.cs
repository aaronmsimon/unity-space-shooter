using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveRotSpeed = 5f;
    [SerializeField] private float turretRotSpeed = 5f;

    private PlayerInput playerInput;
    private Transform leftTurret;
    private Transform rightTurret;

    private float moveInput;
    private float rotation;
    private float leftTurretRot;
    private float rightTurretRot;

    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        leftTurret = transform.Find("Graphics/LeftTurret").transform;
        rightTurret = transform.Find("Graphics/RightTurret").transform;
    }

    private void Update()
    {
        if (leftTurret != null) {
            leftTurretRot = playerInput.actions["LeftTurretRotate"].ReadValue<float>();
            leftTurret.Rotate(Vector3.up, leftTurretRot * turretRotSpeed * Time.deltaTime);
        }

        if (rightTurret != null) {
            rightTurretRot = playerInput.actions["RightTurretRotate"].ReadValue<float>();
            rightTurret.Rotate(Vector3.up, rightTurretRot * turretRotSpeed * Time.deltaTime);
        }

        moveInput = playerInput.actions["Move"].ReadValue<float>();
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0), Space.World);

        Quaternion targetRot;
        if (moveInput != 0) {
            rotation += moveInput * -1;
            rotation = Mathf.Clamp(rotation, -45, 45);
            targetRot = Quaternion.Euler(0, 0, rotation);
        } else {
            rotation = 0;
            targetRot = Quaternion.identity;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, moveRotSpeed * Time.deltaTime);
    }
}
