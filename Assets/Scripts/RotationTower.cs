using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTower : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0)
		{
            transform.rotation = Quaternion.Euler(Vector3.zero);
            return;
		}

        Enemy target = null;
        float distance = 10000;
        foreach (Enemy e in enemies)
		{
            float curDistance = (e.transform.position - transform.position).magnitude;
            if ( curDistance < distance)
			{
                distance = curDistance;
                target = e;
			} 
		}

        float angle = Mathf.Atan2(transform.position.y - target.transform.position.y, transform.position.x - target.transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
