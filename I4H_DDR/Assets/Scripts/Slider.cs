using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public static int difficulty;
    public Slider mySlider;

    void Update()
    {
        difficulty = (int)Math.Round(mySlider.GetComponent<Slider>().value);
    }

}