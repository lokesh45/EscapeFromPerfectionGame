using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField]
    private Image content;

    private float fill;

    [SerializeField]
    private Color lowColor,highColor;

    [SerializeField]
    private float lerpSpeed;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fill = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Update ()
    {
        if(fill != content.fillAmount)
            content.fillAmount = Mathf.Lerp(content.fillAmount,fill,Time.deltaTime * lerpSpeed);
        content.color = Color.Lerp(lowColor, highColor, fill);       
	}

    private float Map(float value,float inMin,float inMax,float outMin,float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
