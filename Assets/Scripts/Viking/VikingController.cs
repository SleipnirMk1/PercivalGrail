using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingController : MonoBehaviour
{
    public VikingModel Model { get; private set; }
    public VikingView View { get; private set; }

    private float timer;

    public VikingController() { }
    public VikingController(VikingModel model, VikingView view)
    {
        Model = model;
        View = view;
    }

    public void Init(VikingModel model, VikingView view)
    {
        Model = model;
        View = view;
        View.Init(Model);

        timer = 0;
    }

    void Update()
    {
        UpdateProgress();
    }

    void UpdateProgress()
    {
        timer += Time.deltaTime;
        View.UpdateProgress(timer / Model.duration);

        if (timer >= Model.duration)
        {
            DestroyArmy();
            timer = 0;
        }
        
    }
    void DestroyArmy()
    {
        List<CardController> cards = ContainerController.Instance.GetCertainTypeCards(CardTypesEnum.Army);
        int idx = UnityEngine.Random.Range(0, cards.Count);

        ContainerController.Instance.DestroyCard(cards[idx]);
    }
}
