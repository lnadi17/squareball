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
			Stats.sessionIncorrect += 1;
		}
	}

	void LayoutGame(int number){
		Text[] textArray = new Text[3] { leftText, middleText, rightText };

		int randomNumber1 = Random.Range (number - 10, number + 10);
		int randomNumber2 = Random.Range (number - 10, number + 10);

		// Both multiplication and division need better logic
		//string myOperator = ChooseOperator ("-", "+", "*", "/");
		string myOperator = ChooseOperator("-", "+");

		string problemString = randomNumber1.ToString () + " " + myOperator + " " + randomNumber2.ToString () + " = ?";
		problemText.text = problemString;

		int correctAnswer = CorrectAnswer (randomNumber1, randomNumber2, myOperator);
		Text correctText = textArray [RandomCorrect ()];
		correctText.text = correctAnswer.ToString ();
		correctText.tag = "Correct";

		//textArray [RandomCorrect ()].text = correctAnswer.ToString ();

		int previousRandom = 0;
		int randomIncorrect = 0;
		foreach (Text txt in textArray){
			if(txt.tag != "Correct"){
				do {
					randomIncorrect = Random.Range (correctAnswer - 5, correctAnswer + 5);
				} while(randomIncorrect == previousRandom || randomIncorrect == correctAnswer);
				txt.text = randomIncorrect.ToString ();
				previousRandom = randomIncorrect;
			}
		}
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

	int CorrectAnswer(int num1, int num2, string operatorString){
		switch (operatorString) {
			case "+":
				return num1 + num2;
			case "-":
				return num1 - num2;
			case "*":
				return num1 * num2;
			case "/":
				return num1 / num2;
			default:
				return 0;
		}
	}
		
	int RandomCorrect(){
		int toCompare = Random.Range (0, 3);
		switch (toCompare) {
			case 1:
				return 1;
			case 2:
				return 2;
			case 3:
				return 3;
			default:
				return 0;
		}
	}

	string ChooseOperator (string a, string b, string x, string y){
		string[] opList = new string[] { a, b, x, y };
		return opList [Random.Range (0, 4)];
	}

	string ChooseOperator(string x, string y){
		string[] opList = new string[] { x, y };
		return opList [Random.Range (0, 2)];
	}

	string ChooseOperator (string x){
		return x;
	}

}
