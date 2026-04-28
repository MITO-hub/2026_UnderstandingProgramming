using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("설정")]
    public int pairCount = 4;
    public Card cardPrefab;
    public Sprite[] cardSprites;

    [Header("배치")]
    public int column = 4;
    public float xSpacing = 3.0f;
    public float ySpacing = 4.0f;
    public Vector2 startPosition = new Vector2(-6f, 3f);

    private Card firstCard;
    private Card secondCard;

    private bool isChecking = false;
    private int matchedCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CreateCards();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CreateCards()
    {
        List<int> ids = new List<int>();

        // 페어 생성
        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        // 섞기
        for (int i = 0; i < ids.Count; i++)
        {
            int rand = Random.Range(i, ids.Count);

            int temp = ids[i];
            ids[i] = ids[rand];
            ids[rand] = temp;
        }

        // 카드 생성
        for (int i = 0; i < ids.Count; i++)
        {
            int id = ids[i];

            float x = startPosition.x + (i % column) * xSpacing;
            float y = startPosition.y - (i / column) * ySpacing;

            Vector3 pos = new Vector3(x, y, 0);

            Card card = Instantiate(cardPrefab, pos, Quaternion.identity);

            card.Init(id, cardSprites[id]);
        }
    }
    public void SelectCard(Card selectedCard)
    {
        if (isChecking)
            return;

        if (firstCard == null)
        {
            firstCard = selectedCard;
        }
        else if (secondCard == null)
        {
            secondCard = selectedCard;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;

        yield return new WaitForSeconds(1f);

        if (firstCard.cardId == secondCard.cardId)
        {
            firstCard.SetMatched();
            secondCard.SetMatched();

            matchedCount++;

            if (matchedCount == pairCount)
            {
                Debug.Log("게임 클리어!");
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

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
