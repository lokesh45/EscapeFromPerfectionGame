using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    private BarControl bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            currentVal = Mathf.Clamp(value,0,MaxVal);
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public void Initialise()
    {
        MaxVal = maxVal;
        CurrentVal = currentVal;
    }
}