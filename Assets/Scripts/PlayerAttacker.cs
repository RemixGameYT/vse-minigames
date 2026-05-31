using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public GameObject playerObject;
    private Transform target;
    public float minDistanceFromPlayer = 30f;
    public float maxDistanceFromPlayer = 50f;
    public GameObject TelegramBullet;
    public int minTelegramBulletsPerAttack;
    public int maxTelegramBulletsPerAttack;
    public GameObject TikTokBullet;
    public GameObject VKVerticalBullet;
    public GameObject VKHorizontalBullet;
    public GameObject SnapChatBullet;
    public float VKOffset = 40f;
    public float fireRate = 0.5f;
    private float timeTillNextSpawn = 1f;
    void Start()
    {

    }
    void Update()
    {
        timeTillNextSpawn -= Time.deltaTime;
        if (timeTillNextSpawn <= 0)
        {
            int attackChoice = UnityEngine.Random.Range(0, 101);
            if (attackChoice < 40)
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack+1);
                SpawnTelegramBullets(telegramBulletsPerAttack);
            }
            else if (attackChoice < 60)
            {
                SpawnTikTokBullets();
            }
            else if (attackChoice < 80)
            {
                SpawnVKBullets();
            }
            else if (attackChoice < 100)
            {
                SpawnSnapChatBullet();
            }
            else
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack + 1);
                SpawnMAXBullet(telegramBulletsPerAttack);
            }
            timeTillNextSpawn = fireRate;
        }
    }
    private void SpawnTelegramBullets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            timeTillNextSpawn /= 0.6f;
            float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            Vector3 target = playerObject.transform.position;
            float directonRad = UnityEngine.Random.Range(-90, 90);
            Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
            Vector3 newPos = target + bulletDirection;
            Instantiate(TelegramBullet, newPos, Quaternion.identity);
        }
    }
    private void SpawnTikTokBullets()
    {
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(TikTokBullet, newPos, Quaternion.identity);
    }
    private void SpawnVKBullets()
    {
        Vector3 newPos = transform.position;
        Vector3 horizontalOffset = new Vector3(0f, VKOffset, 0f);
        Vector3 verticalOffset = new Vector3(VKOffset, 0f, 0f);
        Instantiate(VKHorizontalBullet, newPos+horizontalOffset, Quaternion.identity);
        Instantiate(VKVerticalBullet, newPos + verticalOffset, Quaternion.identity);
    }

    private void SpawnSnapChatBullet()
    {
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(SnapChatBullet, newPos, Quaternion.identity);
    }
    private void SpawnMAXBullet(int amount)
    {
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(TikTokBullet, newPos, Quaternion.identity);
        newPos = transform.position;
        Vector3 horizontalOffset = new Vector3(0f, VKOffset, 0f);
        Vector3 verticalOffset = new Vector3(VKOffset, 0f, 0f);
        Instantiate(VKHorizontalBullet, newPos + horizontalOffset, Quaternion.identity);
        Instantiate(VKVerticalBullet, newPos + verticalOffset, Quaternion.identity);
        distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        target = playerObject.transform.position;
        directonRad = UnityEngine.Random.Range(-90, 90);
        bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        newPos = target + bulletDirection;
        Instantiate(SnapChatBullet, newPos, Quaternion.identity);
        for (int i = 0; i < amount; i++)
        {
            distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            target = playerObject.transform.position;
            directonRad = UnityEngine.Random.Range(-90, 90);
            bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
            newPos = target + bulletDirection;
            Instantiate(TelegramBullet, newPos, Quaternion.identity);
        }
    }
}
