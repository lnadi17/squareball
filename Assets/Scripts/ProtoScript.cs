//using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ProtoScript : MonoBehaviour {

	public GameObject wallObject;

	public Text problemText;
	public Text timeText;
	public Text leftText;
	public Text middleText;
	public Text rightText;

	public float timerMaximum;

	private SceneManagement sceneManagement;

	void Start () {
		timerMaximum += 1;
		CreateWalls ();
		LayoutGame (10);

		sceneManagement = new SceneManagement ();
	}
	
	void Update () {
		if (timerMaximum > 0f) {
			int roundTimer = (int)timerMaximum;
			timeText.text = "Time: " + roundTimer.ToString ();
			timerMaximum -= Time.deltaTime;
		} else {
			sceneManagement.ReplayScene ();
		}
	}

	void LayoutGame(int number){
		Text[] textArray = new Text[3] { leftText, middleText, rightText };

		int randomNumber1 = Random.Range (number - 10, number + 10);
		int randomNumber2 = Random.Range (number - 10, number + 10);

		bool isMinus = false;
		if (Random.Range (0, 2) == 0) {
			isMinus = true;
		}

		string problemString = randomNumber1.ToString () + MinusOrPlus (isMinus) + randomNumber2.ToString () + " = ?";
		problemText.text = problemString;

		foreach (Text txt in textArray){
			txt.text = Random.Range (number - 20, number + 5).ToString();
		}

		int correctAnswer = CorrectAnswer (randomNumber1, randomNumber2, isMinus);
		textArray [RandomCorrect ()].text = correctAnswer.ToString ();
	}

	void CreateWalls(){
		// Initialize values
		float screenHeightHalf = Camera.main.orthographicSize;
		float screenWidthHalf = screenHeightHalf * Camera.main.aspect;

		Vector2 leftPos = new Vector2 (-screenWidthHalf, 0);
		Vector2 rightPos = new Vector2 (screenWidthHalf, 0);
		Vector2 topPos = new Vector2 (0, screenHeightHalf);
		Vector2 bottomPos = new Vector2 (0, -screenHeightHalf);

		Vector2 topBottom = new Vector2 (screenWidthHalf * 2, 1);
		Vector2 leftRight = new Vector2 (1, screenHeightHalf * 2);

		Vector2 leftOff = new Vector2 (-0.5f, 0);
		Vector2 rightOff = new Vector2 (0.5f, 0);
		Vector2 topOff = new Vector2 (0, 0.5f);
		Vector2 bottomOff = new Vector2 (0, -0.5f);

		Vector2[] vectorList = new Vector2[] {
			leftPos,
			rightPos,
			topPos,
			bottomPos
		};
			
		Vector2[] sizeList = new Vector2[] {
			leftRight,
			leftRight,
			topBottom,
			topBottom
		};

		Vector2[] offsetList = new Vector2[] {
			leftOff,
			rightOff,
			topOff,
			bottomOff
		};

		// Instantiate walls with corresponding position, size and offset
		for(int x = 0; x < 4; x++){
			GameObject wall = Instantiate (wallObject, vectorList [x], Quaternion.identity);
			BoxCollider2D boxcol = wall.GetComponent<BoxCollider2D> ();
			boxcol.size = sizeList [x];
			boxcol.offset = offsetList [x];
		}
	}

	int CorrectAnswer(int num1, int num2, bool isMinus){
		int answer = num1 + num2;
		if (isMinus){
			answer = num1 - num2;
		}
		return answer;
	}
		
	int RandomCorrect(){
		int toCompare = Random.Range (0, 3);
		if(toCompare == 0){
			return 0;
		}
		if(toCompare == 1){
			return 1;
		}
		if(toCompare == 2){
			return 2;
		}
		return 0;
	}

	string MinusOrPlus(bool isMinus){
		if (isMinus){
			return " - ";
		}else{
			return " + ";
		}
	}
}
