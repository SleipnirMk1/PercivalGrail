using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerModel : MonoBehaviour
{
    public List<CardController> cards {get; private set;} = new List<CardController>();

    public void AddCard(CardController newCard)
    {
        cards.Add(newCard);
    }
    public void RemoveCard(CardController card)
    {
        cards.Remove(card);
    }
    public void Clear()
    {
        cards.Clear();
    }
}
