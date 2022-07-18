using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    private enum TurretSide {Left, Right};

    [SerializeField] private TurretSide turretSide;
    [SerializeField] private float rotSpeed = 50f;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = transform.parent.transform.parent.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        float rot = playerInput.actions[$"{turretSide}TurretRotate"].ReadValue<float>();
        transform.Rotate(Vector3.up, rot * rotSpeed * Time.deltaTime);

        if (playerInput.actions[$"{turretSide}TurretReset"].ReadValue<float>() != 0)
            transform.rotation = Quaternion.identity;
    }
}
