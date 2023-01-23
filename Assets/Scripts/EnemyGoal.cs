using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<Enemy>(out _))
		{
			Destroy(collision.gameObject);
			//TODO: Deal player damage
		}
	}

}
