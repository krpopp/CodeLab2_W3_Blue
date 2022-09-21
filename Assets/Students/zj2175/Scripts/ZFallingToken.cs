using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZFallingToken : MonoBehaviour
{
    public bool falling;

    float speed = 16f;
    float gravity = 32f;
    Vector2 moveDir;
    RectTransform rect;

    public void Initialize(Sprite piece, Vector2 start)
    {
        falling = true;

        moveDir = Vector2.up;
        moveDir.x = Random.Range(-1f, 1f);
        moveDir *= speed / 2;

        rect = GetComponent<RectTransform>();
        
    }

    //called once per frame
    void Update()
    {
        if(!falling)
        {
            return;
        }

        moveDir.y -= Time.deltaTime * gravity;
        moveDir.x = Mathf.Lerp(moveDir.x, 0, Time.deltaTime);
        rect.anchoredPosition += moveDir * Time.deltaTime * speed;
        
        if(rect.position.x < -32f || rect.position.x > Screen.width + 32f || 
        rect.position.y < -32f || rect.position.y > Screen.height + 32f)
        {
            falling = false;
        }
    }
}
