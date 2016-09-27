using UnityEngine.SceneManagement;

public class SceneManagement{

	public void ReplayScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
