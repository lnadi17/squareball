using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public void ReplayScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	
	public void MenuToGame(){
		SceneManager.LoadScene("Game");
	}

}