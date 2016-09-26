using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ProtoScript : MonoBehaviour {

	public Text problemText;
	public Text timeText;

	public Text leftText;
	public Text middleText;
	public Text rightText;

	public float timerMaximum;

	void Start () {
		timerMaximum += 1;
		CreateWalls ();
		LayoutGame (10);
	}
	
	void Update () {
		if (timerMaximum > 0f){
			int roundTimer = (int)timerMaximum;
			timeText.text = roundTimer.ToString();
			timerMaximum -= Time.deltaTime;
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

	int CorrectAnswer(int num1, int num2, bool isMinus){
		int answer = num1 + num2;
		if (isMinus){
			answer = num1 - num2;
		}
		return answer;
	}

	

	void CreateWalls(){
		//In unity, center (0, 0, 0) is the middle point.

		float screenHeightHalf = Camera.main.orthographicSize * 2;
		float screenWidthHalf = screenHeightHalf * Camera.main.aspect;
		print (screenWidthHalf);

		Vector2 leftPos = new Vector2 (-screenWidthHalf, 0);
		Vector2 rightPos = new Vector2 (screenWidthHalf, 0);
		Vector2 topPos = new Vector2 (0, screenHeightHalf);
		Vector2 bottomPos = new Vector2 (0, -screenWidthHalf);

		Vector2 topBottom = new Vector2 (screenWidthHalf * 2, 5);
		Vector2 leftRight = new Vector2 (5, screenHeightHalf * 2);

		//From 0 to 3 || Left, Right, Top, Bottom.
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
			
		for(int x = 0; x < 4; x++){
			GameObject Wall = new GameObject ();
			BoxCollider2D boxcol = Wall.AddComponent<BoxCollider2D> ();
			Wall.transform.position = vectorList [x];
			boxcol.size = sizeList [x];
		}
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
