using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public float currentValue;
    public float maxValue;
    private Image imageComponent;
    public Color barColor = Color.green;
    public Color barCriticalColor = Color.cyan;
    public float criticalLevel = 0.25f;

    public Image.FillMethod fillMethod = Image.FillMethod.Horizontal;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.type = Image.Type.Filled;
            imageComponent.fillMethod = fillMethod;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float percentOfMax = currentValue / maxValue;

        imageComponent.fillAmount = percentOfMax;
        if (percentOfMax > criticalLevel)
        {
            imageComponent.color = barColor;
        }
        else
        {
            imageComponent.color = barCriticalColor;
        }
    }
}
