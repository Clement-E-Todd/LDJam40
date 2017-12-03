using UnityEngine;

public class Player : MonoBehaviour {
	public Hand leftHand;
	public Hand rightHand;

	private void Awake()
	{
		leftHand.SetOppositeHand(rightHand);
		rightHand.SetOppositeHand(leftHand);
	}
}
