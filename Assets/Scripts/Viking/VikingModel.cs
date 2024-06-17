using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingModel : MonoBehaviour
{
    public float duration {get; set;}

    public VikingModel() {}
    public VikingModel(float initDuration)
    {
        duration = initDuration;
    }
}
