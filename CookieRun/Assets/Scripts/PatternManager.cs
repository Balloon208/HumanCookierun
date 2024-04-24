using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    private List<GameObject> availablePatterns = new List<GameObject>();
    private List<GameObject> usedPatterns = new List<GameObject>();

    public GameObject StartPattern;
    public List<GameObject> patternList = new List<GameObject>();

    public float Speed = 0.0f;
    public float delayTime = 0.0f;
    public float posX = 0.0f;

    private void Start()
    {
        availablePatterns.AddRange(patternList);
        StartCoroutine("CreatePattern");
    }

    IEnumerator CreatePattern()
    {
        GameObject s_pattern = Instantiate(StartPattern, new Vector2(0.0f, 0.0f), Quaternion.identity);
        s_pattern.GetComponent<Pattern>().Init(Speed);

        while (!GameManager.instance.gameOver)
        {
            // 패턴이 모두 사용되면 다시 초기화
            if (availablePatterns.Count == 0)
                availablePatterns.AddRange(usedPatterns);

            // 사용 가능한 패턴 중에서 랜덤하게 선택
            int index = Random.Range(0, availablePatterns.Count);
            GameObject selectedPattern = availablePatterns[index];

            // 선택된 패턴을 사용한 목록으로 이동하고 사용 가능한 목록에서 제거
            usedPatterns.Add(selectedPattern);
            availablePatterns.RemoveAt(index);

            // 패턴을 생성하고 초기화
            GameObject t_pattern = Instantiate(selectedPattern, new Vector2(posX, 0.0f), Quaternion.identity);
            t_pattern.GetComponent<Pattern>().Init(Speed);

            yield return new WaitForSeconds(delayTime);
        }
    }
}
