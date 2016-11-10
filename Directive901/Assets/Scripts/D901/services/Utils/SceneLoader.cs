using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	private bool loadScene = false;
	private int runningProcesses = 0;

	[SerializeField] Text loadingText;

	public GameObject loadingScreen;

	private static SceneLoader instance;


	void Awake(){
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public static SceneLoader getInstance()
	{
		return instance;
	}

	public void showLoading (){
		loadingScreen.SetActive (true);
	}

	public void hideLoading (){
		loadingScreen.SetActive (false);
	}


	public void loadSceneById(int sceneNumber) {
		Debug.Log ("loadSceneById " + sceneNumber);
		if (!loadScene) {
			StartCoroutine(LoadNewScene(sceneNumber));
		}
	}

	public void addRunningProcess(){
		Debug.Log ("addRunningProcess");
		runningProcesses++;
	}

	public void removeRunningProcess(){
		Debug.Log ("removeRunningProcess");
		runningProcesses--;
	}

	// Updates once per frame
	/*void Update() {

		// If the player has pressed the space bar and a new scene is not loading yet...
		if (Input.GetKeyUp(KeyCode.Space) && loadScene == false) {

			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
			loadScene = true;

			// ...change the instruction text to read "Loading..."
			loadingText.text = "Loading...";

			// ...and start a coroutine that will load the desired scene.
			StartCoroutine(LoadNewScene());

		}

		// If the new scene has started loading...
		if (loadScene == true) {

			// ...then pulse the transparency of the loading text to let the player know that the computer is still working.
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

		}

	}*/


	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene(int sceneNumber) {
		showLoading ();
		loadScene = true;
		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(3);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneNumber);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

		Debug.Log ("Loading done, waiting for all responses received...");

		DeckBuilderService.getInstance().GetResearchList();
		SceneLoader.getInstance().addRunningProcess();
		DeckBuilderService.getInstance().GetDeckList();
		SceneLoader.getInstance().addRunningProcess();


		while (!(runningProcesses <= 0)) {
			yield return null;
		}

		Debug.Log ("Loading done, all processes finished");
		loadScene = false;
		hideLoading ();

	}

}