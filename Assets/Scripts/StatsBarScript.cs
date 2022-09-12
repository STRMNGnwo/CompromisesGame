using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBarScript : MonoBehaviour
{
    public Slider slider; // a reference to the Slider component of the status bars
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to set value of the slider that can be used by other scripts.
    public void setValue(double VALUE)
    {
        slider.value=(float)VALUE;
    }
}
