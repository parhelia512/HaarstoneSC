using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { MINION, SPELL, HERO }
public enum MinionAbility { Battlecry, Deathrattle, AttackEffect, Charge, Taunt, CostsLess, DivineShield, Rush, EndOfYourTurn, EndOfEveryTurn, Tradeable, Awaken }
public enum CardTarget { SELF, PLAYER, OPPONENT, HEROES, FRIENDLYMINION, FRIENDLYALL, OPPONENTMINION, OPPONENTALL, MINIONS, ALL }

public enum Corruptable { FALSE, TRUE}
public enum CanSpawnTokens { FALSE, TRUE}

[System.Serializable] public struct MinionAbilites
{
    public MinionAbility minionAbility;
    public string minionEffect;
}

[CreateAssetMenu(fileName ="New Card",menuName = "New Card", order =51)]
public class CardSO : ScriptableObject
{
    [Header("Card Settings")]
    public string cardName;
    public CardType cardType;

    public Sprite cardSprite;
    //public bool hasDoubleClass;
    //public List<ClassSO> cardClass;

    public bool legendary; // legendary ise her desteye bir tane yerleþtirebiliyorsun.

    public bool isToken = false;

    [Header("Script Related")]
    public bool canTarget;
    public CardTarget cardTarget;
    public bool quest;
    public bool hasCardDrawnThisTurnEffect;

    [Header("Stats")]
    public float mana;

    // Spell ise
    public string spellEffect;
    public bool castsWhenDrawn;

    // Minion ise
    public float attack;
    public float hp;
    public List<MinionAbilites> minionAbilites;

    // Hero ise
    public HeroPowerSO heroPower;
    public Sprite portrait;
    public float armorGiven;

    [Header("Additional Keywords")]
    public bool tradeable;

    public Corruptable corruptable;
    public CardSO corruptedVersion;

    public List<CardSO> tokens;
}
