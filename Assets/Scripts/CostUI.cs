using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
	[SerializeField]
	Text metalCostText;
	[SerializeField]
	Text gearCostText;
	[SerializeField]
	Text stoneCostText;


	private void Update()
	{
		CannonBuilder.Cost cost = CannonBuilder.instance.GetCost();
		metalCostText.text = cost.metalCost.ToString();
		gearCostText.text = cost.gearCost.ToString();
		stoneCostText.text = cost.stoneCost.ToString();
	}
}
