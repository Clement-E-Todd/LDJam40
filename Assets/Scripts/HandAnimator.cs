using UnityEngine;

public class HandAnimator : MonoBehaviour
{
	[System.Serializable]
	public struct FingerBones
	{
		public Transform baseBone;
		public Vector3 baseEuler;

		public Transform middleBone;
		public Vector3 middleEuler;

		public Transform tipBone;
		public Vector3 tipEuler;
	}

	Hand hand;

	public Transform armBone;
	Vector3 armEuler;

	public Transform wristBone;
	Vector3 wristEuler;

	public FingerBones[] fingerBones;
	public FingerBones thumbBones;

	const float grabSpeed = 2f;

	float retreatStartTime = -1f;

	private void Awake()
	{
		hand = GetComponent<Hand>();

		armEuler = armBone.transform.localEulerAngles;
		wristEuler = wristBone.transform.localEulerAngles;

		for (int i = 0; i < fingerBones.Length; i++)
		{
			fingerBones[i].baseEuler = fingerBones[i].baseBone.transform.localEulerAngles;
			fingerBones[i].middleEuler = fingerBones[i].middleBone.transform.localEulerAngles;
			fingerBones[i].tipEuler = fingerBones[i].tipBone.transform.localEulerAngles;
		}

		thumbBones.baseEuler = thumbBones.baseBone.transform.localEulerAngles;
		thumbBones.middleEuler = thumbBones.middleBone.transform.localEulerAngles;
		thumbBones.tipEuler = thumbBones.tipBone.transform.localEulerAngles;
	}

	private void Update()
	{
		if (hand.mode != Hand.MovementMode.Grabbing)
		{
			retreatStartTime = 0f;
		}

		switch (hand.mode)
		{
			case Hand.MovementMode.Wrestling:
				// Arm movement
				float angleOffset = Mathf.Sin(Time.time * 13) * 1;
				armBone.transform.localEulerAngles = armEuler + new Vector3(0, 0, angleOffset);

				// Wrist
				angleOffset = Mathf.Sin(Time.time) * 15;
				wristBone.transform.localEulerAngles = wristEuler + new Vector3(0, angleOffset, 0);

				// Thumb
				angleOffset = -20 + Mathf.Sin((Time.time) * 2) * 15;
				thumbBones.baseBone.transform.localEulerAngles = thumbBones.baseEuler + new Vector3(angleOffset, 0, 0);
				thumbBones.middleBone.transform.localEulerAngles = thumbBones.middleEuler + new Vector3(angleOffset, 0, 0);
				thumbBones.tipBone.transform.localEulerAngles = thumbBones.tipEuler + new Vector3(angleOffset, 0, 0);

				// Index Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.25f) * 5) * 20;
				fingerBones[0].baseBone.transform.localEulerAngles = fingerBones[0].baseEuler + new Vector3(0, angleOffset, 0);
				fingerBones[0].middleBone.transform.localEulerAngles = fingerBones[0].middleEuler + new Vector3(0, angleOffset, 0);
				fingerBones[0].tipBone.transform.localEulerAngles = fingerBones[0].tipEuler + new Vector3(0, 0, angleOffset);

				// Middle Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.5f) * 5) * 20;
				fingerBones[1].baseBone.transform.localEulerAngles = fingerBones[1].baseEuler + new Vector3(0, angleOffset, 0);
				fingerBones[1].middleBone.transform.localEulerAngles = fingerBones[1].middleEuler + new Vector3(0, angleOffset, 0);
				fingerBones[1].tipBone.transform.localEulerAngles = fingerBones[1].tipEuler + new Vector3(0, angleOffset, 0);

				// Ring Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.75f) * 5) * 20;
				fingerBones[2].baseBone.transform.localEulerAngles = fingerBones[2].baseEuler + new Vector3(0, angleOffset, 0);
				fingerBones[2].middleBone.transform.localEulerAngles = fingerBones[2].middleEuler + new Vector3(0, angleOffset, 0);
				fingerBones[2].tipBone.transform.localEulerAngles = fingerBones[2].tipEuler + new Vector3(0, 0, -angleOffset);

				// Pinkie Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 1) * 5) * 20;
				fingerBones[3].baseBone.transform.localEulerAngles = fingerBones[3].baseEuler + new Vector3(0, angleOffset, 0);
				fingerBones[3].middleBone.transform.localEulerAngles = fingerBones[3].middleEuler + new Vector3(0, angleOffset, 0);
				fingerBones[3].tipBone.transform.localEulerAngles = fingerBones[3].tipEuler + new Vector3(0, 0, -angleOffset);
				break;


			case Hand.MovementMode.Grabbing:
				// Arm movement
				armBone.transform.localEulerAngles = armEuler;

				// Wrist
				wristBone.transform.localEulerAngles = wristEuler;

				// Thumb
				thumbBones.baseBone.transform.localEulerAngles = thumbBones.baseEuler;
				thumbBones.middleBone.transform.localEulerAngles = thumbBones.middleEuler;
				thumbBones.tipBone.transform.localEulerAngles = thumbBones.tipEuler;

				// Index Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.25f) * 5) * 20;
				fingerBones[0].baseBone.transform.localEulerAngles = fingerBones[0].baseEuler;
				fingerBones[0].middleBone.transform.localEulerAngles = fingerBones[0].middleEuler;
				fingerBones[0].tipBone.transform.localEulerAngles = fingerBones[0].tipEuler;

				// Middle Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.5f) * 5) * 20;
				fingerBones[1].baseBone.transform.localEulerAngles = fingerBones[1].baseEuler;
				fingerBones[1].middleBone.transform.localEulerAngles = fingerBones[1].middleEuler;
				fingerBones[1].tipBone.transform.localEulerAngles = fingerBones[1].tipEuler;

				// Ring Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 0.75f) * 5) * 20;
				fingerBones[2].baseBone.transform.localEulerAngles = fingerBones[2].baseEuler;
				fingerBones[2].middleBone.transform.localEulerAngles = fingerBones[2].middleEuler;
				fingerBones[2].tipBone.transform.localEulerAngles = fingerBones[2].tipEuler;

				// Pinkie Finger
				angleOffset = 25f + Mathf.Sin((Time.time + 1) * 5) * 20;
				fingerBones[3].baseBone.transform.localEulerAngles = fingerBones[3].baseEuler;
				fingerBones[3].middleBone.transform.localEulerAngles = fingerBones[3].middleEuler;
				fingerBones[3].tipBone.transform.localEulerAngles = fingerBones[3].tipEuler;
				break;

			case Hand.MovementMode.Retreating:
				const float fingerCurl = 90f;
				const float retreatTimeMax = 2.5f;

				if (retreatStartTime < 0f)
				{
					retreatStartTime = Time.time;
				}

				float retreatTime = Time.time - retreatStartTime;
				float retreatProgress = Mathf.Clamp01(retreatTime / retreatTimeMax);

				// Arm movement
				armBone.transform.localEulerAngles = armEuler;

				// Wrist
				wristBone.transform.localEulerAngles = wristEuler;

				// Thumb
				thumbBones.baseBone.transform.localEulerAngles = Vector3.Slerp(thumbBones.baseEuler,
					thumbBones.baseEuler,
					retreatProgress);
				thumbBones.middleBone.transform.localEulerAngles = Vector3.Slerp(thumbBones.middleEuler,
					thumbBones.middleEuler,
					retreatProgress);
				thumbBones.tipBone.transform.localEulerAngles = Vector3.Slerp(thumbBones.tipEuler,
					thumbBones.tipEuler,
					retreatProgress);

				// Index Finger
				fingerBones[0].baseBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[0].baseEuler,
					fingerBones[0].baseEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[0].middleBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[0].middleEuler,
					fingerBones[0].middleEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[0].tipBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[0].tipEuler,
					fingerBones[0].tipEuler + new Vector3(0, 0, fingerCurl),
					retreatProgress);

				// Middle Finger
				fingerBones[1].baseBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[1].baseEuler,
					fingerBones[1].baseEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[1].middleBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[1].middleEuler,
					fingerBones[1].middleEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[1].tipBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[1].tipEuler,
					fingerBones[1].tipEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);

				// Ring Finger
				fingerBones[2].baseBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[2].baseEuler,
					fingerBones[2].baseEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[2].middleBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[2].middleEuler,
					fingerBones[2].middleEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[2].tipBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[2].tipEuler,
					fingerBones[2].tipEuler + new Vector3(0, 0, -fingerCurl),
					retreatProgress);

				// Pinkie Finger
				fingerBones[3].baseBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[3].baseEuler,
					fingerBones[3].baseEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[3].middleBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[3].middleEuler,
					fingerBones[3].middleEuler + new Vector3(0, fingerCurl, 0),
					retreatProgress);
				fingerBones[3].tipBone.transform.localEulerAngles = Vector3.Slerp(fingerBones[3].tipEuler,
					fingerBones[3].tipEuler + new Vector3(0, 0, -fingerCurl),
					retreatProgress);
				break;
		}
	}
}
