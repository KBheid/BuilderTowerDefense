using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    public float attackInterval = 1f;
    public bool active = true;
    public bool hitsAir = false;
    public bool hitsGround = true;

    private float currentAttackInterval = 0;


	private void Start()
	{
        int depth = 0;
        Transform t = transform;
        while (t != null)
		{
            t = t.parent;
            depth++;
		}

        if (depth >= 5)
		{
            hitsAir = true;
            hitsGround = false;
		}
	}

	// Update is called once per frame
	void Update()
    {
        if (currentAttackInterval >= attackInterval)
		{
            Shoot();
            currentAttackInterval = 0;
		}
        else
		{
            currentAttackInterval += Time.deltaTime;
		}
    }


    void Shoot()
	{
        foreach (Transform t in transform.Find("Cannons"))
		{
            if (t.gameObject.activeInHierarchy)
			{
                // Create and launch projectile with properties
                Projectile p = Instantiate(bulletPrefab).GetComponent<Projectile>();

                p.transform.position = t.position;
                p.speed = 2.5f;
                p.damage = 1;
                p.maxDistance = 10f;
                p.hitsAir = hitsAir;
                p.hitsAll = hitsAir && hitsGround;
                p.direction = t.position - transform.position;
			}
		}
	}
}
