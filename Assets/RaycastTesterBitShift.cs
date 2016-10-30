using System;
using UnityEngine;

public class RaycastTesterBitShift : RayCastTesterBase
{
	public string[] layerNamesToTest;
	public bool invert = false;

	[Header("Debugging")]
	public int[] layerValues;
	public string[] bitValues;
	public string layerMaskBits;

	void Update()
	{
		LayerMask toTest = 0;

		layerValues = new int[layerNamesToTest.Length];
		bitValues = new string[layerNamesToTest.Length];

		for (int i = 0; i < layerNamesToTest.Length; i++)
		{
			layerValues[i] = (1 << LayerMask.NameToLayer(layerNamesToTest[i]));
			bitValues[i] = Convert.ToString(layerValues[i], 2);
			toTest |= layerValues[i];
		}

		if (invert)
			toTest = ~toTest;

		layerMaskBits = Convert.ToString(toTest, 2);

		HandleRayCast(toTest);
	}

}
