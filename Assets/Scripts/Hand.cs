using UnityEngine;

public class Hand : MonoBehaviour
{
	public enum MovementMode
	{
		Wrestling,
		Grabbing,
		Retreating
	}

	public KeyCode controlKeyLeft;
	public KeyCode controlKeyRight;

	public Vector3 retreatDestination;

	const float handMoveSpeedMax = 1f;
	const float handMoveSpeedMin = -0.5f;

	const float grabDelay = 0.15f;
	const float grabSpeed = 3f;
	const float retreatSpeed = 5f;

	Transform targetTreat;
	Hand oppositeHand;
	public MovementMode mode { get; private set; }

	const float minDistanceFromOtherHand = 1.5f;
	const float maxDistanceBeforeGrabbing = 1.1f;

	public float control { get; private set; }
	float controlRateOfChange = 0f;

	private void Awake()
	{
		mode = MovementMode.Wrestling;
		control = 0.5f;

		RandomizeControl();
	}

	private void Update()
	{
		UpdateControl();
		Move();
	}

	private void UpdateControl()
	{
		// Maintain perfect control while round is not in progress
		if (!GameController.instance.roundInProgress)
		{
			control = 0.5f;
		}

		// Update via temptation
		control += controlRateOfChange * Time.deltaTime;

		// Update via player input
		if (Input.GetKey(controlKeyLeft))
		{
			control -= Time.deltaTime;
		}
		if (Input.GetKey(controlKeyRight))
		{
			control += Time.deltaTime;
		}

		// Clamp the control between 0 and 1
		control = Mathf.Clamp01(control);
	}

	private void Move()
	{
		if (!GameController.instance.roundInProgress && mode == MovementMode.Grabbing)
		{
			mode = MovementMode.Wrestling;
		}

		Vector3 destination;
		switch (mode)
		{
			case MovementMode.Wrestling:
				if (!targetTreat)
				{
					GameObject[] treats = GameObject.FindGameObjectsWithTag("Treat");
					if (treats.Length > 0)
					{
						targetTreat = treats[Random.Range(0, treats.Length)].transform;
					}
					else
					{
						mode = MovementMode.Retreating;
						return;
					}
				}

				bool shouldMoveTowardTreat = true;

				if (Mathf.Abs(transform.position.z) > Mathf.Abs(oppositeHand.transform.position.z) &&
					Vector3.Distance(transform.position, oppositeHand.transform.position) < minDistanceFromOtherHand)
				{
					shouldMoveTowardTreat = false;
				}

				if (shouldMoveTowardTreat)
				{
					destination = targetTreat.position + new Vector3(0, 1f, 0);
					Vector3 directionToMove = Vector3.Normalize(destination - transform.position);

					float handSpeed = 0f;

					if (GameController.instance.roundInProgress)
					{
						handSpeed = Mathf.Lerp(
							Vector3.Distance(transform.position, targetTreat.position) < 3 ? handMoveSpeedMin : 0.2f,
							handMoveSpeedMax,
							Mathf.Abs(control - 0.5f) * 2
						);
					}
					else
					{
						handSpeed = Vector3.Distance(transform.position, targetTreat.position) < 5 ? -1f : 0f;
					}

					transform.position += directionToMove * Time.deltaTime * handSpeed;
				}

				if (Vector3.Distance(transform.position, targetTreat.transform.position) < maxDistanceBeforeGrabbing)
				{
					mode = MovementMode.Grabbing;
					Invoke("Retreat", grabDelay);
				}

				break;

			case MovementMode.Grabbing:
				destination = targetTreat.position + new Vector3(0, 0.25f, 0);
				if (targetTreat != null && Vector3.Distance(transform.position, destination) > 0.05f)
				{
					Vector3 directionToMove = Vector3.Normalize(destination - transform.position);
					transform.position += directionToMove * Time.deltaTime * grabSpeed;
				}
				break;

			case MovementMode.Retreating:
				if (Vector3.Distance(transform.position, retreatDestination) > 0.05f)
				{
					Vector3 directionToMove = Vector3.Normalize(retreatDestination - transform.position);
					transform.position += directionToMove * Time.deltaTime * retreatSpeed;
				}
				else if (GameObject.FindGameObjectsWithTag("Treat").Length > 0)
				{
					mode = MovementMode.Wrestling;
				}
				break;
		}
	}

	void RandomizeControl()
	{
		controlRateOfChange = Random.Range(0.1f, 0.5f) * (Random.Range(0, 2) == 0 ? 1 : -1);
		Invoke("RandomizeControl", Random.Range(0.5f, 2f));
	}

	public void SetOppositeHand(Hand oppositeHand)
	{
		this.oppositeHand = oppositeHand;
	}

	private void Retreat()
	{
		mode = MovementMode.Retreating;
		Destroy(targetTreat.gameObject);
		targetTreat = null;
	}
}
