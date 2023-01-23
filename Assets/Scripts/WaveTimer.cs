using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    private Text timerText;

	private void Start()
	{
		timerText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
    {
        timerText.text = ((int) WaveController.instance.timeUntilWave).ToString();
    }
}
