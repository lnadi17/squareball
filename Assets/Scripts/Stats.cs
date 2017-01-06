using UnityEngine.UI;
using UnityEngine;

public class Stats : MonoBehaviour {

	public static Stats instance = null;

	public static string[] possibleOperators = new string[] {
		"+",
		"-",
	}; 

	public static int sessionCorrect;
	public static int sessionIncorrect;

	public Text incorrectText;
	public Text correctText;

	void Start(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
		
		incorrectText.text = sessionIncorrect.ToString ();
		correctText.text = sessionCorrect.ToString ();
	}
}