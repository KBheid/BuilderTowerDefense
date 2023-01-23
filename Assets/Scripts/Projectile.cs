using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxDistance;
    public float damage;
    public float speed;
    public Vector3 direction;
    public bool hitsAir = false;
    public bool hitsAll = false;

    private float distanceTravelled = 0f;


    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;

        transform.position += direction.normalized * speed * Time.deltaTime;

        if (distanceTravelled >= maxDistance)
		{
            Destroy(gameObject);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Enemy e))
		{
            if (hitsAir == e.isAir || hitsAll)
            {
                e.Health -= damage;
                Destroy(gameObject);
            }
		}
	}
}
