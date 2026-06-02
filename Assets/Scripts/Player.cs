using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    private float TimeElapsed = 0f;
    public int Score = 100;
    public float InvFrames = 1f;
    private float InvCurr = 0f;
    private float blink = 0f;
    public int blinkAmount = 6;
    public SpriteRenderer currentSprite;
    private bool hasStarted = false;
    private int maxTime = 15;

    void Start()
    {
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
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            TimeText.text = $"Time left: {maxTime - seconds - 1}";
        }
        if (Input.GetMouseButton(0))
        {
            hasStarted = true;
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
            Score -= 26;
            HitText.text = $"Score: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
        }
        if (other.gameObject.tag == "TikTokBullet" && InvCurr <= 0)
        {
            Score -= 7;
            HitText.text = $"Score: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames/4;
        }
        if (other.gameObject.tag == "VKBullet" && InvCurr <= 0)
        {
            Score -= 19;
            HitText.text = $"Score: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
        }
        if (other.gameObject.tag == "SnapChatBullet" && InvCurr <= 0)
        {
            Score -= 14;
            HitText.text = $"Score: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
        }
    }
    public bool Check_If_Started()
    {
        return hasStarted;
    }
}
