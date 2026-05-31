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
    public int HitTimes = 0;
    public float InvFrames = 1f;
    private float InvCurr = 0f;
    private float blink = 0f;
    public int blinkAmount = 6;
    public Renderer currentSprite;

    void Start()
    {
        battleBox.transform.localScale=transform.localScale/5*BoxLimits/50*BoxScale;
        BoxLimits-=transform.localScale.y/2;
        Collider = GetComponent<CircleCollider2D>();
        currentSprite = GetComponent<Renderer>();
        Instantiate(battleBox);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;
            moveToPos = mouseWorldPos;
        }
        if (InvCurr > 0)
        {
            InvCurr -= Time.deltaTime;
            blink = Mathf.Cos(InvCurr*4*blinkAmount);
            currentSprite.material.color = new Color((191+64*blink)/255, 0f, 0f);
        }
        else
        {
            InvCurr = 0f;
            blink = 1f;
            currentSprite.material.color = new Color(1f, 0f, 0f);
        }
        currentPos = transform.position;
        Vector2 direction = (moveToPos - currentPos).normalized;
        Vector2 newPos;
        float dist;
        dist = Vector2.Distance(currentPos, moveToPos);
        if (dist < Speed*Time.deltaTime)
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
        if (other.gameObject.tag == "Bullet" && InvCurr <= 0)
        {
            HitTimes += 1;
            HitText.text = $"Times hit: {HitTimes}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
        }
    }
}
