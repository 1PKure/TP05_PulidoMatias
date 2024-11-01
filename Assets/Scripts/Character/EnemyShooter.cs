using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 5f;
    private float fireRate = 2f;
    private float nextFireTime = 0f;

    private void Update()
    {
        CheckVisionRange();
    }

    private void CheckVisionRange()
    {
        
        Vector2 playerDirection = player.position - transform.position;
        float playerDIstance = Vector2.Distance(transform.position, player.position);

        
        if (playerDIstance <= visionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, visionRange, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player") && Time.time >= nextFireTime)
            {
                Shoot(playerDirection);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject proyectil = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;
    }
}


