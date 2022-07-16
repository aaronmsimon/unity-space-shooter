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
    private float lastMoveInput;
    private float rotation;
    private float rotSnapbackSpeed = 200f;
    private float maxShipRotation = 45;
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
        float rotSpeed;
        if (Sign(lastMoveInput) != Sign(moveInput)) {
            transform.rotation = Quaternion.identity;
        } else {
            if (moveInput != 0) {
                rotation += moveInput * -1;
                rotation = Mathf.Clamp(rotation, Mathf.Min(0, maxShipRotation * Mathf.Sign(rotation)), Mathf.Max(0, maxShipRotation * Mathf.Sign(rotation)));
                targetRot = Quaternion.Euler(0, 0, rotation);
                rotSpeed = moveRotSpeed;
            } else {
                rotation = 0;
                targetRot = Quaternion.identity;
                rotSpeed = rotSnapbackSpeed;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }
        lastMoveInput = moveInput;
    }

    private float Sign(float number) {
        return number < 0 ? -1 : (number > 0 ? 1 : 0);
    }
}
