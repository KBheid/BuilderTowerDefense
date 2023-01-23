using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
	public bool isAir = false;
	
	private AIPath path;

	[SerializeField]
	private float health;
	public float Health
	{
		set
		{
			if (value < 0)
				Destroy(gameObject);

			health = value;
		}
		get
		{
			return health;
		}
	}

	[SerializeField]
	float speed;
    public float Speed
	{
		set
		{
			path.maxSpeed = value;
			speed = value;
		}
		get
		{
			return speed;
		}
	}

	private void Start()
	{
		path = GetComponent<AIPath>();
		Speed = speed;
		Health = health;
	}

	private void OnDestroy()
	{
		int rand = Random.Range(0, 3);

		switch (rand)
		{
			case 0:
				EcoController.instance.metalCount += 1;
				break;
			case 1:
				EcoController.instance.gearCount += 1;
				break;
			case 2:
				EcoController.instance.stoneCount += 1;
				break;
		}
	}
}
