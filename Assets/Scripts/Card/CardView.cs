using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text preferenceTitle;
    [SerializeField] private TMP_Text preferenceValue;
    [SerializeField] private Image cardImage;
    [SerializeField] private Slider progressSlider;
    
    public CardModel Model { get; private set; }

    public void Init(CardModel model)
    {
        Model = model;
        InitiateDisplay();
    }

    void InitiateDisplay()
    {
        cardName.text = Model.GetName();
        cardImage.color = Model.GetColor();

        progressSlider.value = 0;
     //   preferenceValue.text = ''
        
        if (Model.GetType() != CardTypesEnum.Production)
            progressSlider.gameObject.SetActive(false);

        if (Model.GetPreferences().Count == 0)
        {
            preferenceTitle.gameObject.SetActive(false);
            preferenceValue.gameObject.SetActive(false);
        }
    }

    public void UpdateProgress(float newValue)
    {
        progressSlider.value = newValue;
    }
    public void UpdatePreference(CardModel newPref)
    {
        preferenceValue.text = newPref.GetName();
    }
}
