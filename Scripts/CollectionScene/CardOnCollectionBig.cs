using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardOnCollectionBig : MonoBehaviour
{
    public CardSO card;
    public float mana;
    // Minion ise
    public float attack;
    public float hp;
    public float maxHP;

    [HideInInspector]
    public CollectionManager cm;

    private void Awake()
    {
        cm = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
    }

    private void Update()
    {
        transform.Find("CardImage").GetComponent<RawImage>().texture = card.cardSprite.texture;
        transform.Find("Mana").GetComponent<TextMeshProUGUI>().text = card.mana.ToString();
        transform.Find("Attack").gameObject.SetActive(card.cardType == CardType.MINION);
        transform.Find("Health").gameObject.SetActive(card.cardType == CardType.MINION);
        transform.Find("Mana").GetComponent<TextMeshProUGUI>().GetComponent<RectTransform>().anchoredPosition = card.legendary ? new Vector3(-53.7f, 81, 0) : new Vector3(-53.7f, 90, 0);

        if (card.cardType != CardType.MINION) return;

        transform.Find("Attack").GetComponent<TextMeshProUGUI>().text = card.attack.ToString();
        transform.Find("Health").GetComponent<TextMeshProUGUI>().text = card.hp.ToString();
    }

    public void CloseBigCard()
    {
        cm.cardOnCollectionBigPrefab.SetActive(false);
    }
}
