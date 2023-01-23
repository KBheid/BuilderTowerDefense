using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoPanel : MonoBehaviour
{
    public Text metalText;
    public Text gearText;
    public Text stoneText;


    // Update is called once per frame
    void Update()
    {
        metalText.text = EcoController.instance.metalCount.ToString();
        gearText.text = EcoController.instance.gearCount.ToString();
        stoneText.text = EcoController.instance.stoneCount.ToString();
    }
}
