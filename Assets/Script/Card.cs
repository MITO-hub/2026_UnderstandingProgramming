using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId;                  // 카드 번호
    public Sprite frontSprite;          // 앞면 그림
    public Sprite backSprite;           // 뒷면 그림

    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;
    private bool isMatched = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.PlayBGM();

        Debug.Log(gameObject.name + " 시작됨");
        spriteRenderer.sprite = backSprite;   // 시작할 때는 뒷면
    }

    public void Init(int id, Sprite sprite)
    {
        cardId = id;
        frontSprite = sprite;
        CloseCard();
    }

    void OnMouseDown()
    {
        Debug.Log("카드 클릭됨");
        
        // 이미 열린 카드거나, 맞춘 카드면 클릭 무시
        if (isOpen || isMatched)
            return;

        // 카드 열기
        OpenCard();

        // 게임 매니저에게 "이 카드 클릭됐어요" 알리기
        GameManager.instance.SelectCard(this);
        
    }

    public void OpenCard()
    {
        isOpen = true;
        spriteRenderer.sprite = frontSprite;
    }

    public void CloseCard()
    {
        isOpen = false;
        spriteRenderer.sprite = backSprite;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    void Update()
    {

    }
}
