using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

    void Start()
    {
        anim.SetBool("isEnd", false);
    }

    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            anim.SetBool("isEnd", true);
        }
    }
}
