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
