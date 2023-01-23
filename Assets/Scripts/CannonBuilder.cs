using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBuilder : MonoBehaviour
{
	public static CannonBuilder instance;
	private void Awake()
	{
		instance = this;
	}


	[SerializeField] Transform Layer1;
	[SerializeField] Transform Layer2;
	[SerializeField] Transform Layer3;
	[SerializeField] Transform Layer4;
	[SerializeField] Transform Layer5;
	[SerializeField] GameObject buildingLocator;

	public struct Cost
	{
		public int metalCost;
		public int gearCost;
		public int stoneCost;

		public Cost(int metal, int gear, int stone)
		{
			metalCost = metal;
			gearCost = gear;
			stoneCost = stone;
		}

		public void Add(Cost c)
		{
			metalCost += c.metalCost;
			gearCost += c.gearCost;
			stoneCost += c.stoneCost;
		}
	}

	public void CycleLayer(int layer)
	{
		Transform structureSelection;
		switch (layer)
		{
			case 1:
				structureSelection = Layer1.Find("StructureSelection");
				break;
			case 2:
				structureSelection = Layer2.Find("StructureSelection");
				break;
			case 3:
				structureSelection = Layer3.Find("StructureSelection");
				break;
			case 4:
				structureSelection = Layer4.Find("StructureSelection");
				break;
			case 5:
				structureSelection = Layer5.Find("StructureSelection");
				break;
			default:
				structureSelection = Layer1.Find("StructureSelection");
				break;
		}

		for (int i = 0; i < structureSelection.childCount; i++)
		{
			Transform t = structureSelection.GetChild(i);
			if (t.gameObject.activeInHierarchy)
			{
				t.gameObject.SetActive(false);
				structureSelection.GetChild( (i + 1) % structureSelection.childCount).gameObject.SetActive(true);
				break;
			}
		}
	}

	public Cost GetCost()
	{
		Cost cost = new Cost(0, 0, 0);

		if (Layer1.gameObject.activeInHierarchy)
		{
			cost.Add(GetCostOfLayer(Layer1));
		}
		if (Layer2.gameObject.activeInHierarchy)
		{
			cost.Add(GetCostOfLayer(Layer2));
		}
		if (Layer3.gameObject.activeInHierarchy)
		{
			cost.Add(GetCostOfLayer(Layer3));
		}
		if (Layer4.gameObject.activeInHierarchy)
		{
			cost.Add(GetCostOfLayer(Layer4));
		}
		if (Layer5.gameObject.activeInHierarchy)
		{
			cost.Add(GetCostOfLayer(Layer5));
		}

		return cost;
	}

	private Cost GetCostOfLayer(Transform t)
	{
		Transform trans = t.Find("StructureSelection");
		foreach (Transform active in trans)
		{
			if (active.gameObject.activeInHierarchy)
			{
				StructureLayer layer = active.GetComponent<StructureLayer>();
				return new Cost(layer.metalCost, layer.gearCost, layer.stoneCost);
			}
		}
		return new Cost(0, 0, 0);
	}

	private GameObject GetInstantiatedOfLayer(Transform t)
	{
		Transform trans = t.Find("StructureSelection");
		foreach (Transform active in trans)
		{
			if (active.gameObject.activeInHierarchy)
			{
				StructureLayer layer = active.GetComponent<StructureLayer>();
				return layer.CreatePrefab();
			}
		}

		return null;
	}

	public void Build()
	{
		GameObject l1 = GetInstantiatedOfLayer(Layer1);
		GameObject l2 = GetInstantiatedOfLayer(Layer2);
		GameObject l3 = GetInstantiatedOfLayer(Layer3);
		GameObject l4 = GetInstantiatedOfLayer(Layer4);
		GameObject l5 = GetInstantiatedOfLayer(Layer5);

		if (l5)
		{
			l5.transform.parent = l4.transform;
			l5.transform.localScale = Vector3.one * 0.85f;
			l5.transform.localPosition = Vector3.zero;
			l5.GetComponent<SpriteRenderer>().sortingOrder = 10;
		}
		if (l4)
		{
			l4.transform.parent = l3.transform;
			l4.transform.localScale = Vector3.one * 0.85f;
			l4.transform.localPosition = Vector3.zero;
			l4.GetComponent<SpriteRenderer>().sortingOrder = 8;
		}
		if (l3)
		{
			l3.transform.parent = l2.transform;
			l3.transform.localScale = Vector3.one * 0.85f;
			l3.transform.localPosition = Vector3.zero;
			l3.GetComponent<SpriteRenderer>().sortingOrder = 6;
		}
		if (l2)
		{
			l2.transform.parent = l1.transform;
			l2.transform.localScale = Vector3.one * 0.85f;
			l2.transform.localPosition = Vector3.zero;
			l2.GetComponent<SpriteRenderer>().sortingOrder = 4;
		}


		Cost c = GetCost();
		EcoController.instance.metalCount -= c.metalCost;
		EcoController.instance.gearCount -= c.gearCost;
		EcoController.instance.stoneCount -= c.stoneCost;

		l1.transform.position = buildingLocator.transform.position;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if (hit.collider != null)
			{
				buildingLocator.transform.position = hit.point;
			}
		}
	}
}
