using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardModel Model { get; private set; }
    public CardView View { get; private set; }

    public ProductionRecipe selectedRecipe { get; set; }
    public List<string> cardsNameStack { get; private set; } = new List<string>();
    
    private bool isProducing;
    private float timer;
    private float prefTimer;
    private CardModel preference;

    public CardController() { }
    public CardController(CardModel model, CardView view)
    {
        Model = model;
        View = view;
    }

    public void Init(CardModel model, CardView view)
    {
        Model = model;
        View = view;
        View.Init(Model, OnClick);

        isProducing = false;
        timer = 0f;
        prefTimer = 0f;

        ResetStack();

        if (Model.GetPreferences().Count > 0)
            SetNewPreference();

        if (Model.GetType() == CardTypesEnum.Quest)
        {
            // check win
            GameLauncher.Instance.WinGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isProducing)
            ProducingCard();

        if (Model.GetPreferences().Count > 0)
            PreferenceTimer();
    }
    
    void ProduceCard(ProductionRecipe recipe) {
        isProducing = true;
        selectedRecipe = recipe;
    }
    void ProducingCard()
    {
        timer += Time.deltaTime;

        float waitTime = selectedRecipe.productionDuration;

        if (preference != null)
        {
            if (cardsNameStack.Contains(preference.GetName()))
                waitTime /= 2;
        }
        

        View.UpdateProgress(timer / waitTime);
        if (timer >= waitTime)
        {
            // Remove child before instantiating anything 
            foreach (CardController c in transform.GetComponentsInChildren<CardController>())
            {
                if (c == this)
                    continue;

                c.gameObject.transform.SetParent(transform.parent, true);
                c.OnStackLifted();
                if (selectedRecipe.consumeMaterials)
                    ContainerController.Instance.DestroyCard(c);
            }
            
            int idx = 0;
            for (int i = 0; i < selectedRecipe.createCount; i++)
            {
                idx = UnityEngine.Random.Range(0, selectedRecipe.cardsCreated.Count);
                CardModel c = selectedRecipe.cardsCreated[idx];

                ContainerController.Instance.CreateCard(c);
            }

            timer = 0;
            View.UpdateProgress(timer);
            OnStackLifted();
        }

    }

    public void OnGetStacked()
    {
        UpdateStack();

        if (Model.GetType() == CardTypesEnum.Production)
        {
            bool valid = true;
            foreach (ProductionRecipe r in Model.GetRecipe())
            {
                valid = true;
                foreach (RecipeMaterial m in r.materials)
                {
                    if( cardsNameStack.Contains(m.cardMaterial.GetName()) );
                        valid = ( m.count == GetItemCountInList(cardsNameStack, m.cardMaterial.GetName()) );

                    if (!r.allMaterialsRequired && valid)
                        break;
                    else if (r.allMaterialsRequired)
                        break;
                }
                
                if (valid)
                {
                    ProduceCard(r);
                }
            }
        }

        // DO THE CHECK FOR PARENTS
        CardController parentCard = this.transform.parent.GetComponent<CardController>();
        if (parentCard != null)
            parentCard.OnGetStacked();

        //Debug.Log("stacked");
    }

    public void OnStackLifted()
    {
        //Debug.Log("lifted");
        isProducing = false;
        ResetStack();
    }

    private int GetItemCountInList(List<string> list, string item)
    {
        int count = 0;
        foreach (var i in list)
        {
            if (i == item)
                count++;
        }

        //Debug.Log(count);
        return count;
    }

    public void DestroyCard()
    {
        Destroy(Model);
        Destroy(View);
        Destroy(gameObject);
    }

    void ResetStack()
    {
        cardsNameStack.Clear();
        cardsNameStack.Add(Model.GetName());
    }
    void UpdateStack()
    {
        ResetStack();

        foreach (CardController c in transform.GetComponentsInChildren<CardController>())
        {
            if (c != this)
                cardsNameStack.Add(c.Model.GetName());
        }
    }

    void PreferenceTimer()
    {
        prefTimer += Time.deltaTime;

        if (prefTimer >= Model.GetPrefChangeDuration())
        {
            SetNewPreference();
            prefTimer = 0;
        }
    }
    void SetNewPreference()
    {
        List<CardModel> possiblePreference = new List<CardModel>(Model.GetPreferences());
        if (preference != null)
            possiblePreference.Remove(preference);

        int idx = UnityEngine.Random.Range(0, possiblePreference.Count);
        preference = possiblePreference[idx];
        View.UpdatePreference(preference);
    }

    void OnClick()
    {
        if (Model.GetOutput().Count > 0)
        {
            int idx = 0;
            for (int i = 0; i < Model.GetOutputCount(); i++)
            {
                idx = UnityEngine.Random.Range(0, Model.GetOutput().Count);
                CardModel c = Model.GetOutput()[idx];

                ContainerController.Instance.CreateCard(c);
            }

            ContainerController.Instance.DestroyCard(this);
        }
    }
}
