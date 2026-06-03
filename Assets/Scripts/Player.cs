using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Vector2 moveToPos;
    public Vector2 currentPos;
    public float Speed;
    public Vector3 mouseWorldPos;
    public float BoxLimits;
    public float BoxScale = 1f;
    public CircleCollider2D Collider;
    public GameObject battleBox;
    public TMP_Text HitText;
    public TMP_Text TimeText;
    public TMP_Text FinalText;
    public GameObject GameOverText;
    public GameObject MenuBG;
    public GameObject ScoreAndTimeCounters;
    public Image panel;
    private float TimeElapsed = 0f;
    public int Score = 100;
    public float InvFrames = 1f;
    private float InvCurr = 0f;
    private float blink = 0f;
    public int blinkAmount = 6;
    public SpriteRenderer currentSprite;
    private bool hasStarted = false;
    private int maxTime = 15;
    public int TelegramDamage;
    public int TikTokDamage;
    public int SnapChatDamage;
    public int VKDamage;
    public PlaySound soundPlayer;
    private bool playedEndgameSFX = false;

    void Start()
    {
        soundPlayer = GetComponent<PlaySound>();
        battleBox.transform.localScale = transform.localScale / 6.7f * BoxLimits * BoxScale;
        BoxLimits -= transform.localScale.y / 2;
        Collider = GetComponent<CircleCollider2D>();
        currentSprite = GetComponent<SpriteRenderer>();
        Instantiate(battleBox);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.V) && !hasStarted)
        {
            maxTime = 10000;
        }
        if (hasStarted)
        {
            TimeElapsed += Time.deltaTime;
        }
        int seconds = Mathf.FloorToInt(TimeElapsed);
        if (seconds == maxTime)
        {
            if (!playedEndgameSFX)
            {
                playedEndgameSFX=true;
                soundPlayer.PlaySFX(1);
            }
            Time.timeScale = 0f;
            GameOverText.SetActive(true);
            MenuBG.SetActive(true);
            ScoreAndTimeCounters.SetActive(false);
        }
        else
        {
            TimeText.text = $"Время: {maxTime - seconds - 1}";
        }
        if (Input.GetMouseButton(0))
        {
            if (!hasStarted)
            {
                Time.timeScale = 1f;
                MenuBG.SetActive(false);
                ScoreAndTimeCounters.SetActive(true);
                hasStarted = true;
            }
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;
            moveToPos = mouseWorldPos;
        }
        if (InvCurr > 0)
        {
            InvCurr -= Time.deltaTime;
            blink = Mathf.Cos(InvCurr * 4 * blinkAmount);
            currentSprite.color = new Color((191 + 64 * blink) / 255, (191 + 64 * blink) / 255, (191 + 64 * blink) / 255);
        }
        else
        {
            InvCurr = 0f;
            blink = 1f;
            currentSprite.color = new Color(1f, 1f, 1f);
        }
        currentPos = transform.position;
        Vector2 direction = (moveToPos - currentPos).normalized;
        Vector2 newPos;
        float dist;
        dist = Vector2.Distance(currentPos, moveToPos);
        if (dist < Speed * Time.deltaTime)
        {
            newPos = currentPos + direction * dist * Time.deltaTime;
        }
        else
        {
            newPos = currentPos + direction * Speed * Time.deltaTime;
        }
        newPos.x = Math.Clamp(newPos.x, -BoxLimits, BoxLimits);
        newPos.y = Math.Clamp(newPos.y, -BoxLimits, BoxLimits);
        transform.position = newPos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TelegramBullet" && InvCurr <= 0)
        {
            Score -= TelegramDamage;
            HitText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            FinalText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
        }
        if (other.gameObject.tag == "TikTokBullet" && InvCurr <= 0)
        {
            Score -= TikTokDamage;
            HitText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            FinalText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames/4;
            soundPlayer.PlaySFX(0);
        }
        if (other.gameObject.tag == "VKBullet" && InvCurr <= 0)
        {
            Score -= VKDamage;
            HitText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            FinalText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
        }
        if (other.gameObject.tag == "SnapChatBullet" && InvCurr <= 0)
        {
            Score -= SnapChatDamage;
            HitText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            FinalText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
        }
    }
    public bool Check_If_Started()
    {
        return hasStarted;
    }
}
