using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardModel", menuName = "ScriptableObjects/Card Model")]
public class CardModel : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string cardName;
    [SerializeField] private CardTypesEnum cardType;
    [SerializeField] private Color cardColor;

    [Header("For Production Cards")]
    [SerializeField] private List<ProductionRecipe> recipes;
    [SerializeField] private List<CardModel> possiblePreference;
    [SerializeField] private float prefChangeDuration;

    // public CardModel() {}
    // public CardModel(CardModel model) 
    // {
    //     cardName = model.GetName();
    //     cardType = model.GetType();
    //     cardColor = model.GetColor();
    //     recipes = model.GetRecipe();
    // }

    public string GetName() {
        return cardName;
    }
    public CardTypesEnum GetType() {
        return cardType;
    }
    public Color GetColor() {
        return cardColor;
    }
    public List<ProductionRecipe> GetRecipe() {
        return recipes;
    }
    public List<CardModel> GetPreferences() {
        return possiblePreference;
    }
    public float GetPrefChangeDuration() {
        return prefChangeDuration;
    }
}
