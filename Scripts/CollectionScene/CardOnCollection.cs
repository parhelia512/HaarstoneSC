using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardOnCollection : MonoBehaviour
{
    public CardSO card;
    public float mana;
    // Minion ise
    public float attack;
    public float hp;
    public float maxHP;

    [HideInInspector]
    public CollectionManager cm;

    private void Start()
    {
        cm = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
        mana = card.mana;
        attack = card.attack;
        hp = card.hp;
        maxHP = card.hp;
        transform.localScale = new Vector3(1.65f, 1.65f, 1.65f);
    }

    private void Update()
    {
        transform.Find("CardImage").GetComponent<RawImage>().texture = card.cardSprite.texture;
        transform.Find("Mana").GetComponent<TextMeshProUGUI>().text = mana.ToString();
        transform.Find("Attack").gameObject.SetActive(card.cardType == CardType.MINION);
        if (card.cardType == CardType.MINION) transform.Find("Attack").GetComponent<TextMeshProUGUI>().text = attack.ToString();
        transform.Find("Health").gameObject.SetActive(card.cardType == CardType.MINION);
        if (card.cardType == CardType.MINION) transform.Find("Health").GetComponent<TextMeshProUGUI>().text = hp.ToString();
        transform.Find("Mana").GetComponent<TextMeshProUGUI>().GetComponent<RectTransform>().anchoredPosition = card.legendary ? new Vector3(-53.7f, 81, 0) : new Vector3(-53.7f, 90, 0);
    }

    public int CardInDeckCount(CardSO thisCard, int count = 0)
    {
        if (cm.currDeck.Count > 0)
        {
            IEnumerable<CardOnDeck> data =
                from _card in cm.currDeck
                where (thisCard == _card.card)
                select _card;
            count = data.ToList().Count;
        }
        return count;
    }

    public GameObject cardOnDeckPrefab;

    public void AddToDeck()
    {
        if (PlayerDatabase.currentDeck.Count < 30)
        {
            if (cm.creatingDeck && ((card.legendary && CardInDeckCount(card) < 1) || (!card.legendary && CardInDeckCount(card) < 2)))
            {
                PlayerDatabase.currentDeck.Add(card);
                if (CardInDeckCount(card) != 1)
                {
                    GameObject cardOnDeck = Instantiate(cardOnDeckPrefab, GameObject.Find("NewDeck").transform);
                    cm.currDeck.Add(cardOnDeck.GetComponent<CardOnDeck>());
                    cardOnDeck.GetComponent<CardOnDeck>().card = card;
                }
                else
                {
                    CardOnDeck _card = cm.currDeck.FirstOrDefault(a => a.card == card);
                    if (_card != null) _card.count = 2;
                }
                cm.SortCardsInDeck();
            }
        }
    }

    public void CheckCard()
    {
        if (cm.creatingDeck) return;

        cm.cardOnCollectionBigPrefab.GetComponent<CardOnCollectionBig>().card = card;
        cm.cardOnCollectionBigPrefab.gameObject.SetActive(true);
    }
}
