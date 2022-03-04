using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public List<Deck> decks;

    public GameObject background;
    public Sprite collectionBackground, newDeckBackground;

    public GameObject newDeck;
    public GameObject createNewDeck;

    public bool creatingDeck;

    public string currentDeckName;
    public List<CardOnDeck> currDeck;
    public List<CardSO> currDeckAsCards;

    public TextMeshProUGUI deckCount;

    public List<CardSO> cards = new List<CardSO>();
    public GameObject cardOnCollection;
    public int currentPage;

    public int MaxPage() => (int)(cards.Count / 8);

    public GameObject goLeft, goRight;
    public TextMeshProUGUI currentPageText;

    public GameObject cardOnCollectionBigPrefab;

    public int DeckCount()
    {
        return currDeck.Sum(a => a.count);
    }

    private void Awake()
    {
        Setup();
    }

    void Setup()
    {
        CardSO[] _cards = Resources.LoadAll("Database", typeof(CardSO)).Cast<CardSO>().ToArray();
        IEnumerable<CardSO> cardsInGame =
            from _card in _cards
            where !_card.isToken
            select _card;
        cards.AddRange(cardsInGame);

        decks = PlayerDatabase.decks;
        cards = cards.OrderBy(c => c.mana).ThenBy(c => c.cardName).ToList();
        currentPage = 0;

        for (int i = 0; i < (currentPage == MaxPage() ? cards.Count % 8 : 8); i++)
        {
            GameObject card = Instantiate(cardOnCollection, GameObject.Find("Cards").transform);
            card.GetComponent<CardOnCollection>().card = cards[currentPage * 8 + i];
            card.tag = "CardOnDeck";
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2((-706 + i % 4 * 300), i < 4 ? 220 : -166);
        }
    }

    public void GoToNextPage()
    {
        if (currentPage < MaxPage())
        {
            currentPage++;
        }
        foreach (GameObject _coD in GameObject.FindGameObjectsWithTag("CardOnDeck"))
        {
            Destroy(_coD);
        }
        for (int i = 0; i < (currentPage == MaxPage() ? cards.Count % 8 : 8); i++)
        {
            GameObject card = Instantiate(cardOnCollection, GameObject.Find("Cards").transform);
            card.GetComponent<CardOnCollection>().card = cards[currentPage * 8 + i];
            card.tag = "CardOnDeck";
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2((-706 + i % 4 * 300), i < 4 ? 220 : -166);
        }
    }

    public void GoToPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
        foreach (GameObject _coD in GameObject.FindGameObjectsWithTag("CardOnDeck"))
        {
            Destroy(_coD);
        }
        for (int i = 0; i < (currentPage == MaxPage() ? cards.Count % 8 : 8); i++)
        {
            GameObject card = Instantiate(cardOnCollection, GameObject.Find("Cards").transform);
            card.GetComponent<CardOnCollection>().card = cards[currentPage * 8 + i];
            card.tag = "CardOnDeck";
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2((-706 + i % 4 * 300), i < 4 ? 220 : -166);
        }
    }

    private void Update()
    {
        if (creatingDeck) deckCount.text = DeckCount() + "/30\nCards";
        goLeft.SetActive(currentPage > 0);
        goRight.SetActive(currentPage < MaxPage());
        SortCardsInDeck();
        currentPageText.text = currentPage.ToString();
    }

    public void CreateNewDeck()
    {
        background.GetComponent<Image>().sprite = newDeckBackground;
        newDeck.SetActive(true);
        createNewDeck.SetActive(false);
        creatingDeck = true;
    }

    public void ReturnToCollection()
    {
        background.GetComponent<Image>().sprite = collectionBackground;
        newDeck.SetActive(false);
        createNewDeck.SetActive(true);
        creatingDeck = false;
        IEnumerable<CardSO> _cards =
            from _card in currDeck
            select _card.card;
        currDeckAsCards.AddRange(_cards.ToList());
        Deck deck = new Deck();
        deck.deckName = currentDeckName;
        deck.cards = currDeckAsCards;
        deck.deckNumber = PlayerDatabase.decks.Count;
        PlayerDatabase.decks.Add(deck);
        PlayerDatabase.currentDeck = currDeckAsCards;
    }

    public void ReturnToMainMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Collection_Mobile" ? "MainMenu_Mobile" : "MainMenu_PC");

    public void SortCardsInDeck()
    {
        currDeck = currDeck
            .OrderBy(c => c.mana)
            .ThenBy(c => c.cardName)
            .ToList();
        foreach (CardOnDeck _card in currDeck)
        {
            _card.numberOnDeck = currDeck.IndexOf(_card); // belki sonra tweening kullanabilirim kartlarýn kaymasý için
        }
    }
}