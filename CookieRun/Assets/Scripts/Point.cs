using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Slider hp;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text coin;
    [SerializeField]
    private Animator anim;

    public float decreaseRate = 0.02f;

    private void Start()
    {
        // 슬라이더의 값을 1로 초기화
        hp.value = 1f;
        // 1초마다 DecreaseSlider 함수를 호출
        InvokeRepeating("DecreaseSlider", 0f, 1f);
    }

    private void DecreaseSlider()
    {
        // 슬라이더의 값을 감소시킴
        hp.value -= decreaseRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 레이어 이름을 가져옴
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        // 충돌한 오브젝트가 player 오브젝트인지 레이어가 일치하는지 확인
        switch (layerName)
        {
            case "Jelly":
                AddScore(385, other);
                break;
            case "YellowBear":
                AddScore(2600, other);
                break;
            case "PinkBear":
                AddScore(4680, other);
                break;
            case "Sliver":
                AddScore(260, other);
                AddCoin(1);
                break;
            case "SmallGold":
                AddScore(1000, other);
                AddCoin(10);
                break;
            case "BigGold":
                AddScore(5590, other);
                AddCoin(100);
                break;
            case "obstacle":
                StartCoroutine(nameof(Hit));
                break;
            case "bigHeart":
                hp.value += 0.3f;
                Destroy(other.gameObject);
                break;
            case "smallHeart":
                hp.value += 0.1f;
                Destroy(other.gameObject);
                break;
        }
    }
    IEnumerator Hit()
    {
        if (0 >= hp.value - 0.05f)
        {
            anim.SetBool("isHitDying", true);
            hp.value -= 0.05f;
            GameManager.instance.gameOver = true;
            Invoke("StopAnim", 1f);
        }
        else
        {
            anim.SetBool("isHiting", true);
            hp.value -= 0.05f;
            yield return new WaitForSeconds(0.1f);
            anim.SetBool("isHiting", false);
        }
    }
    void StopAnim()
    {
        anim.speed = 0.0f;
    }
    // 점수를 추가하는 함수
    private void AddScore(int points, Collider2D other)
    {
        // 현재 점수를 가져와서 추가된 점수를 더하고, 텍스트로 업데이트
        int currentScore = int.Parse(score.text);
        int newScore = currentScore + points;
        score.text = newScore.ToString();

        // 충돌한 오브젝트 제거
        Destroy(other.gameObject);
    }

    private void AddCoin(int coins)
    {
        int currentCoin = int.Parse(coin.text);
        int newCoin = currentCoin + coins;
        coin.text = newCoin.ToString();
    }
}
