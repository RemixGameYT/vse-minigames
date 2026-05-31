using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikTokAndSnapchatBullet : MonoBehaviour
{
    public float timeToShoot = 1f;
    public float timeToStop = 2f;
    public float fireRate = 0.1f;
    public int fireArc = 90;
    public GameObject miniBullet;
    private GameObject playerToTarget;
    private float counter = 0f;
    private float fireCounter = 0f;
    void Start()
    {
        playerToTarget = GameObject.FindWithTag("Player");
        Destroy(gameObject, timeToStop);
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timeToShoot)
        {
            fireCounter += Time.deltaTime;
            if (fireCounter >= fireRate)
            {
                fireCounter = 0f;
                FireSpreadBullet();
            }
        }
    }

    void FireSpreadBullet()
    {
        Vector2 directionToPlayer = (playerToTarget.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float randomOffset = Random.Range(-fireArc / 2f, fireArc / 2f);
        float finalAngle = baseAngle + randomOffset;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, finalAngle);
        Instantiate(miniBullet, transform.position, bulletRotation);
    }
}