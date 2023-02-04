using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingRoots : MonoBehaviour
{
    public Slider slider;

    public void SetMinGrowth(float growth)
    {
        slider.minValue = growth;
        slider.value = growth;
    }

    public void SetMaxGrowth(float growth)
    {
        slider.maxValue = growth;
    }

    public void SetGrowth(float growth)
    {
        slider.value = growth;
    }
}
