using UnityEngine;
using System.Collections;

public class RayCastTesterByName : RayCastTesterBase
{
	public string ignoredLayerName = "Default";

	void Update()
	{
		

		LayerMask layerMask = ~LayerMask.GetMask(ignoredLayerName);

		HandleRayCast(layerMask);
	}
}
