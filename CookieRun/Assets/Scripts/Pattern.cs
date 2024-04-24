using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    private const float ENDPOSX = -40.0f;
    private float moveSpeed;

    public void Init(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        // 패턴을 이동시킴
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 패턴이 일정 위치(ENDPOSX) 이하로 이동하면 오브젝트를 파괴
        if (transform.position.x <= ENDPOSX)
        {
            Destroy(gameObject);
        }

        // 게임 종료 시 패턴을 멈춤
        if (GameManager.instance.gameOver)
        {
            moveSpeed = 0f;
        }
    }
}
