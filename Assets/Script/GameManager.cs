using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Card firstCard;
    private Card secondCard;

    private bool isChecking = false;
    private int matchedCount = 0;
    public int totalPairCount = 4;   // 카드 쌍 개수

    public GameObject clearText;
    public Card[] cards;

    void Awake()
    {
        instance = this;
    }

    public void SelectCard(Card selectedCard)
    {
        // 비교 중이면 클릭 막기
        if (isChecking)
            return;

        // 첫 번째 카드 선택
        if (firstCard == null)
        {
            firstCard = selectedCard;
        }
        // 두 번째 카드 선택
        else if (secondCard == null)
        {
            secondCard = selectedCard;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;

        // 잠깐 기다려서 플레이어가 카드 볼 시간 주기
        yield return new WaitForSeconds(1f);

        if (firstCard.cardId == secondCard.cardId)
        {
            // 같은 카드면 맞춘 처리
            firstCard.SetMatched();
            secondCard.SetMatched();
            matchedCount++;

            Debug.Log("짝 맞춤!");

            if (matchedCount == totalPairCount)
            {
                Debug.Log("게임 클리어!");
                clearText.SetActive(true);
            }
        }
        else
        {
            // 다르면 다시 닫기
            firstCard.CloseCard();
            secondCard.CloseCard();

            Debug.Log("틀렸습니다!");
        }

        // 다시 선택할 수 있게 초기화
        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShuffleCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            int randomIndex = Random.Range(i, cards.Length);

            Vector3 tempPosition = cards[i].transform.position;
            cards[i].transform.position = cards[randomIndex].transform.position;
            cards[randomIndex].transform.position = tempPosition;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShuffleCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Card card = hit.collider.GetComponent<Card>();

                if (card != null)
                {
                    Debug.Log("카드 클릭 성공: " + card.name);
                    card.OpenCard();
                }
            }
        }
    }
}
