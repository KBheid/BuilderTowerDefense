using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBase : StructuralBase
{
    public bool leftCannonEnabled;
    public bool rightCannonEnabled;
    public bool topCannonEnabled;
    public bool topLeftCannonEnabled;
    public bool topRightCannonEnabled;
    public bool bottomCannonEnabled;
    public bool bottomLeftCannonEnabled;
    public bool bottomRightCannonEnabled;

    public void ToggleCannon(int cannonId)
	{
        switch (cannonId)
		{
            case 0:
                leftCannonEnabled = !leftCannonEnabled;
                metalCost += (leftCannonEnabled) ? 1 : -1;
                break;
            case 1:
                rightCannonEnabled = !rightCannonEnabled;
                metalCost += (rightCannonEnabled) ? 1 : -1;
                break;
            case 2:
                topCannonEnabled = !topCannonEnabled;
                metalCost += (topCannonEnabled) ? 1 : -1;
                break;
            case 3:
                topLeftCannonEnabled = !topLeftCannonEnabled;
                metalCost += (topLeftCannonEnabled) ? 1 : -1;
                break;
            case 4:
                topRightCannonEnabled = !topRightCannonEnabled;
                metalCost += (topRightCannonEnabled) ? 1 : -1;
                break;
            case 5:
                bottomCannonEnabled = !bottomCannonEnabled;
                metalCost += (bottomCannonEnabled) ? 1 : -1;
                break;
            case 6:
                bottomLeftCannonEnabled = !bottomLeftCannonEnabled;
                metalCost += (bottomLeftCannonEnabled) ? 1 : -1;
                break;
            case 7:
                bottomRightCannonEnabled = !bottomRightCannonEnabled;
                metalCost += (bottomRightCannonEnabled) ? 1 : -1;
                break;
		}

	}

    override public void ResetLayer()
	{
        leftCannonEnabled = false;
        rightCannonEnabled = false;
        topCannonEnabled = false;
        topLeftCannonEnabled = false;
        topRightCannonEnabled = false;
        bottomCannonEnabled = false;
        bottomLeftCannonEnabled = false;
        bottomRightCannonEnabled = false;

        metalCost = 0;


        foreach (Transform t in transform.Find("CannonButtons"))
        {
            Button b = t.GetComponent<Button>();
            var colors = b.colors;
            colors.normalColor = Color.gray;
            b.colors = colors;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform.Find("CannonButtons"))
		{
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener( () => {
                ToggleCannon(t.GetSiblingIndex());

                var colors = b.colors;
                colors.normalColor = (colors.normalColor == Color.white) ? Color.gray : Color.white ;
                b.colors = colors;
            });

		}
    }

	public override GameObject CreatePrefab()
	{
		GameObject go = base.CreatePrefab();

        Transform cannons = go.transform.Find("Cannons");
        cannons.GetChild(0).gameObject.SetActive(leftCannonEnabled);
        cannons.GetChild(1).gameObject.SetActive(rightCannonEnabled);
        cannons.GetChild(2).gameObject.SetActive(topCannonEnabled);
        cannons.GetChild(3).gameObject.SetActive(bottomCannonEnabled);
        cannons.GetChild(4).gameObject.SetActive(topLeftCannonEnabled);
        cannons.GetChild(5).gameObject.SetActive(topRightCannonEnabled);
        cannons.GetChild(6).gameObject.SetActive(bottomLeftCannonEnabled);
        cannons.GetChild(7).gameObject.SetActive(bottomRightCannonEnabled);


        return go;
	}
}
