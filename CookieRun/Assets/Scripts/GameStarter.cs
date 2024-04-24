using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(Wait1s());
    }

    IEnumerator Wait1s()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
    }
}
