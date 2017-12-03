using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
	public Hand target;
	public RectTransform needle;
	public Text leftKeyText;
	public Text rightKeyText;

	const float needleDistMax = 90f;

	private void Awake()
	{
		leftKeyText.text = target.controlKeyLeft.ToString();
		rightKeyText.text = target.controlKeyRight.ToString();

		// If using alpha keys, remove "Alpha" from the text
		leftKeyText.text = leftKeyText.text.Replace("Alpha", "");
		rightKeyText.text = rightKeyText.text.Replace("Alpha", "");
	}

	private void Update()
	{
		Vector2 position = needle.anchoredPosition;
		position.x = needleDistMax * (target.control - 0.5f) * 2;
		needle.anchoredPosition = position;
	}
}
