using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoController : MonoBehaviour
{
    public static EcoController instance;
	private void Awake()
	{
        instance = this;
	}

    public int metalCount;
    public int gearCount;
    public int stoneCount;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMaterials(int metal, int gear, int stone)
	{
        metalCount += metal;
        gearCount += gear;
        stoneCount += stone;
	}
}
