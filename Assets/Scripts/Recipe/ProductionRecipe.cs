using System.Collections.Generic;

[System.Serializable]
public struct ProductionRecipe
{
    public float productionDuration;
    public bool allMaterialsRequired;
    public bool consumeMaterials;

    public List<RecipeMaterial> materials;
    public List<CardModel> cardsCreated;
    
    public int createCount;
}
