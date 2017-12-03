using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	
	Plate currentPlate;
	public GameObject[] platePrefabs;
	int round = -1;

	int timer = 100;
	public Text timerText;

	bool movingPlateIn = false;
	bool movingPlateOut = false;

	const float moveInSpeed = 3f;
	const float moveOutSpeed = 6f;

	public bool roundInProgress
	{
		get
		{
			return currentPlate != null && !movingPlateIn;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Invoke("MoveNextPlateIn", 0.5f);
	}

	private void MoveNextPlateIn()
	{
		round++;

		if (currentPlate)
		{
			movingPlateOut = false;
			Destroy(currentPlate.gameObject);
		}

		if (round < platePrefabs.Length)
		{
			currentPlate = Instantiate(platePrefabs[round]).GetComponent<Plate>();
			currentPlate.transform.position = transform.position - new Vector3(6, 0, 0);

			movingPlateIn = true;
			Invoke("StartRound", 3f);
		}
		else
		{
			Debug.Log("TODO: Show win state");
		}
	}

	private void StartRound()
	{
		movingPlateIn = false;
		timer = currentPlate.roundTime;
		timerText.text = timer.ToString();
		Invoke("CountDown", 1);
	}

	private void CountDown()
	{
		timer -= 1;

		if (timer > 0)
		{
			timerText.text = timer.ToString();
			Invoke("CountDown", 1);
		}
		else
		{
			timerText.text = "";
			movingPlateOut = true;
			Invoke("MoveNextPlateIn", 1f);
		}
	}

	private void Update()
	{
		if (movingPlateIn)
		{
			currentPlate.transform.position += (transform.position - currentPlate.transform.position) * moveInSpeed * Time.deltaTime;
		}
		else if (movingPlateOut)
		{
			currentPlate.transform.position += new Vector3(moveOutSpeed * Time.deltaTime, 0, 0);
		}
	}
}
