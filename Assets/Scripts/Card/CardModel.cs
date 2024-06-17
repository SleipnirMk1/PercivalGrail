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

    [Header("For Boosters")]
    [SerializeField] private List<CardModel> output;
    [SerializeField] private int count;

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
    public List<CardModel> GetOutput() {
        return output;
    }
    public int GetOutputCount() {
        return count;
    }
}
