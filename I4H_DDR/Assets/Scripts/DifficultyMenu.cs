using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenu : MonoBehaviour
{
    public GameObject[] labelArray;
    int activeLabel = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject label in labelArray)
        {
            label.SetActive(false);
        }
        labelArray[activeLabel].SetActive(true);
    }

    public void Changelabel(Slider slider)
    {
        labelArray[activeLabel].SetActive(false);
        labelArray[(int)slider.value - 1].SetActive(true);
        activeLabel = (int)slider.value - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
