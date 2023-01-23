using UnityEngine;

public class StructureLayer : MonoBehaviour
{
	public int stoneCost;
	public int gearCost;
	public int metalCost;


	public GameObject prefab;

	public virtual void ResetLayer()
	{

	}

	public virtual GameObject CreatePrefab()
	{
		return Instantiate(prefab);
	}
}