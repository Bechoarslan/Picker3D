using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    
    
    [Range(0, 300)]
    [BoxGroup("Stacked Health"), HideLabel]
    public float StackedHealth = 150;

  

    [SerializeField] private Image progressBar;
    [HideLabel, ShowInInspector]
    [ProgressBar(0, 100, ColorGetter = "GetStackedHealthColor", BackgroundColorGetter = "GetStackHealthBackgroundColor", DrawValueLabel = false)]
    [BoxGroup("Stacked Health")]
    private float StackedHealthProgressBar
    {
        get { return this.StackedHealth % 100.01f; }
    }

    private Color GetStackedHealthColor()
    {
        return
            this.StackedHealth > 200 ? Color.white :
            this.StackedHealth > 100 ? Color.green :
            Color.red;
    }

    private Color GetStackHealthBackgroundColor()
    {
        return
            this.StackedHealth > 200 ? Color.green :
            this.StackedHealth > 100 ? Color.red :
            new Color(0.16f, 0.16f, 0.16f, 1f);
    }
    [Button  ]
    private void ChangeColor()
    {
        progressBar.color = GetStackHealthBackgroundColor();
    }
}
