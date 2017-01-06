using UnityEngine;

public class PlayerControl : MonoBehaviour {

	[Range(0, 5)]public float speed;
	public bool debugMode;

	private float xInput;
	private float yInput;
	private Rigidbody2D rb2d;

	private SceneManagement sceneManagement;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		sceneManagement = new SceneManagement ();
	}
	
	void Update () {
		if (!debugMode){
			xInput = Input.acceleration.x;
			yInput = Input.acceleration.y;
		} else {
			xInput = Input.GetAxis ("Horizontal");
			yInput = Input.GetAxis ("Vertical");
		}
		Vector3 movementVector = new Vector3 (xInput * speed, yInput * speed, 0);
		rb2d.AddForce (movementVector, ForceMode2D.Force);
	}

	//Doesn't matter if the answer is correct or not, restart scene.
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Choice") {
			sceneManagement.ReplayScene ();
			Stats.sessionIncorrect += 1;
		}
			
		if (other.tag == "Correct") {
			sceneManagement.ReplayScene ();
			Stats.sessionCorrect += 1;
		}
	}
}
