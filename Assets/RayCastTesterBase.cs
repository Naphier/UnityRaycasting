using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class RayCastTesterBase : MonoBehaviour
{
	public float distance = 50;
	public Vector2 direction = Vector2.right;

	public LineRenderer lineRenderer;
	public Color noHitColor = Color.white;
	public Color hitColor = Color.red;

	protected Dictionary<int, SpriteRendererBool> spriteRenderers =
		new Dictionary<int, SpriteRendererBool>();

	protected class SpriteRendererBool
	{
		public SpriteRenderer spriteRenderer;
		public bool value;
		public Color initialColor;
		public SpriteRendererBool(SpriteRenderer spriteRenderer, bool value, Color initialColor)
		{
			this.spriteRenderer = spriteRenderer;
			this.value = value;
			this.initialColor = initialColor;
		}
	}

	protected void Reset()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}


	private void ResetHitSpriteRenderersColor()
	{
		foreach (var item in spriteRenderers)
		{
			if (item.Value.spriteRenderer != null)
			{
				item.Value.spriteRenderer.color = item.Value.initialColor;
				item.Value.value = false;
			}
		}
	}

	protected void SetHitSpriteRenderersColor()
	{
		foreach (var item in spriteRenderers)
		{
			if (item.Value.value && item.Value.spriteRenderer != null)
			{
				item.Value.spriteRenderer.color = hitColor;
			}
		}
	}

	protected void SetLineRendererValues(Vector2 start, Vector2 end, Color color)
	{
		lineRenderer.SetPositions(new Vector3[2] { start, end });
		lineRenderer.SetColors(color, color);
	}

	protected void HandleRayCast(LayerMask layerMask)
	{
		ResetHitSpriteRenderersColor();

		RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, distance, layerMask);
		if (raycastHit2D.collider != null)
		{
			int id = raycastHit2D.collider.gameObject.GetInstanceID();
			if (!spriteRenderers.ContainsKey(id))
			{
				SpriteRenderer sr = raycastHit2D.collider.gameObject.GetComponent<SpriteRenderer>();
				if (sr)
				{
					spriteRenderers.Add(id, new SpriteRendererBool(sr, true, sr.color));
				}
			}
			else
			{
				spriteRenderers[id].value = true;
			}

			SetHitSpriteRenderersColor();

			SetLineRendererValues(transform.position, raycastHit2D.point, hitColor);
		}
		else
			SetLineRendererValues(transform.position, ((Vector2)transform.position + distance * direction), noHitColor);
	}
}
