using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    private enum TurretSide {Left, Right};

    [SerializeField] private TurretSide turretSide;
    [SerializeField] private float rotSpeed = 50f;

    [SerializeField] private GameObject projectile;

    private PlayerInput playerInput;

    private float nextShotTime;
    private float timeBetweenShots = .2f;

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

        if (playerInput.actions[$"{turretSide}TurretFire"].ReadValue<float>() != 0)
            Shoot();
    }

    private void Shoot()
    {
        if (Time.time > nextShotTime) {
            Instantiate(projectile, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
}
