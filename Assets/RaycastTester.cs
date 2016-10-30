using UnityEngine;


public class RaycastTester : RayCastTesterBase
{
	public LayerMask testedLayers;

	void Update()
	{
		HandleRayCast(testedLayers);
	}

}
