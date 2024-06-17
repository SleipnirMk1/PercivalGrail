using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingView : MonoBehaviour
{
    public VikingModel Model { get; private set; }

    [SerializeField] private Slider progressSlider;

    public void Init(VikingModel model)
    {
        Model = model;
        progressSlider.value = 0;
    }

    public void UpdateProgress(float newValue)
    {
        progressSlider.value = newValue;
    }
}
