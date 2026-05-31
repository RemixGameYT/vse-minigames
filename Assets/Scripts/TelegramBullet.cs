using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class TelegramBullet : MonoBehaviour
{
    public float timeToShoot = 1f;
    public float timeStopAiming = 0.7f;
    public float speed = 200f;
    public float liveTime = 5f;
    private GameObject playerToTarget;
    private float counter = 0f;
    public Vector3 direction;
    public Renderer sprite;
    void Start()
    {
        playerToTarget = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<Renderer>();
        Destroy(gameObject, liveTime);
    }

    void Update()
    {
        if (counter < timeStopAiming)
        {
            RotateToTarget(playerToTarget.transform);
            float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
            direction = new Vector3(-Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0).normalized;
        }
        else if (counter > timeToShoot)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
        else 
        {
            sprite.material.color = new Color((255-128*(counter-timeStopAiming)/(timeToShoot - timeStopAiming))/255, (255 - 255 * (counter - timeStopAiming) / (timeToShoot - timeStopAiming)) / 255, (255 - 255 * (counter - timeStopAiming) / (timeToShoot - timeStopAiming)) / 255);
        }
        counter += Time.deltaTime;

    }
    void RotateToTarget(Transform target)
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
