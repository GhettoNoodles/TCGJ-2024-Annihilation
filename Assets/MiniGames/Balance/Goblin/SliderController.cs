using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

    public RotateArm rotateArm;
    [SerializeField]
    private Slider slider;

   // public Text valueText;


    private void Start()
    {
        slider.maxValue = rotateArm.MaxPower;
        slider.minValue = rotateArm.MinPower;
    }

    private void Update()
    {
        slider.value = rotateArm.CurrentPower;
    }
}
