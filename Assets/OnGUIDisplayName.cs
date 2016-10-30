using UnityEngine;

public class OnGUIDisplayName : MonoBehaviour
{
	public enum Display { None, Name, Layer, Tag};
	public Display display = Display.Layer;
	private Vector3 offset;
	private new Renderer renderer = null;
	private string overrideValue = null;

	public void SetLabel(string value)
	{
		overrideValue = value;
	}

	void OnGUI()
	{
		string value = overrideValue;
		if (string.IsNullOrEmpty(value))
		{
			switch (display)
			{
				case Display.None:
					return;
				case Display.Name:
					value = gameObject.name;
					break;
				case Display.Layer:
					value = LayerMask.LayerToName(gameObject.layer);
					break;
				case Display.Tag:
					value = gameObject.tag;
					break;
				default:
					value = "ERROR";
					break;
			}
		}

		if (renderer == null)
			renderer = gameObject.GetComponent<Renderer>();

		if (renderer != null)
			offset.y = renderer.bounds.extents.y;

		GUI.backgroundColor = Color.black;
		var guiPosition = Camera.main.WorldToScreenPoint(transform.position + offset);
		guiPosition.y = Screen.height - guiPosition.y;

		

		GUIContent content = new GUIContent(value);
		Vector2 dimensions = GUI.skin.box.CalcSize(content);
		

		guiPosition.x -= 0.5f * dimensions.x;
		guiPosition.y -= dimensions.y;

		GUI.Box(new Rect(guiPosition, dimensions), content);
	}
}
