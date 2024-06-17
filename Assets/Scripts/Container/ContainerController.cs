using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    private static ContainerController instance;

    public static ContainerController Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public ContainerModel Model {get; set;}
    public Transform containerTransform {get; private set;}
    public GameObject cardTemplate {get; private set;}

    public void Init(ContainerModel model, Transform container, GameObject template)
    {
        Model = model;
        containerTransform = container;
        cardTemplate = template;
    }

    public void CreateCard(CardModel card)
    {
        CardModel newModel = Instantiate(card);
        GameObject newCard = Instantiate(cardTemplate, containerTransform);

        CardView newView = newCard.GetComponent<CardView>();
        CardController newController = newCard.GetComponent<CardController>();
        newController.Init(newModel, newView);

        Model.AddCard(newController);
    }

    public void DestroyCard(CardController card)
    {
        Model.RemoveCard(card);
        card.DestroyCard();
    }

    public List<CardController> GetCardsList()
    {
        return Model.cards;
    }
    public List<CardController> GetCertainTypeCards(CardTypesEnum type)
    {
        List<CardController> ret = new List<CardController>();

        foreach(CardController card in Model.cards)
        {
            if (card.Model.GetType() == type)
                ret.Add(card);
        }

        return ret;
    }
}
