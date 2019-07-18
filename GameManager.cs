using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;
	
	// Reference to UI panel that is our pause menu
	public GameObject pauseMenuPanel;
  	// Reference to panel's script object 
    PauseMenuManager pauseMenu;
	public Player player;

	private static Scene currentScene;
    private static string sceneName;

	// Used for scene fading transitions
	public Image fadingTransition;
	// This is for the ending scene, hopefully not needed for every scenes
	public Image fadingEndTransition;
	public Animator animator;

	public Animator animatorEnd;
	private bool pause = false;
	private int prisoner1;
    private int prisoner2;
    private int prisoner3;
    private int prisoner4;
    private int prisoner5;
    private int level;

	private AudioSource levelMusic;

	// initialization of the scene
	void Start () {
		levelMusic = GetComponent<AudioSource>();
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;

		// Initialise the reference to the script object, which is a
      	// component of the pause menu panel game object
		pauseMenu = pauseMenuPanel.GetComponent<PauseMenuManager>();
		//pauseMenu = gameManager.pauseMenuPanel.GetComponent<PauseMenuManager>();
      	pauseMenu.Hide();
		
		if (Total.total.prisoner1 == false) {
			prisoner1 = 0;
		} else {
			prisoner1 = 1;
		}
		PlayerPrefs.SetInt("Prisoner1", prisoner1);

		if (Total.total.prisoner2 == false) {
			prisoner2 = 0;
		} else {
			prisoner2 = 1;
		}
		PlayerPrefs.SetInt("Prisoner2", prisoner2);

		if (Total.total.prisoner3 == false) {
			prisoner3 = 0;
		} else {
			prisoner3 = 1;
		}
		PlayerPrefs.SetInt("Prisoner3", prisoner3);

		if (Total.total.prisoner4 == false) {
			prisoner4 = 0;
		} else {
			prisoner4 = 1;
		}
		PlayerPrefs.SetInt("Prisoner4", prisoner4);

		if (Total.total.prisoner5 == false) {
			prisoner5 = 0;
		} else {
			prisoner5 = 1;
		}
		PlayerPrefs.SetInt("Prisoner5", prisoner5);

		PlayerPrefs.SetInt("Level", 0);
		if (sceneName == "L1-1") {
			PlayerPrefs.SetInt("Level", 1);
		} else if (sceneName == "L2-1") {
			PlayerPrefs.SetInt("Level", 2);
		} else if (sceneName == "L2-2") {
			PlayerPrefs.SetInt("Level", 3);
		} else if (sceneName == "L3-1") {
			PlayerPrefs.SetInt("Level", 4);
		} else if (sceneName == "L4-1") {
			PlayerPrefs.SetInt("Level", 5);
		} else if (sceneName == "L5-1") {
			PlayerPrefs.SetInt("Level", 6);
		}
	}
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)) {
        	 // If user presses ESC, show the pause menu
         	if (pause == false) {
                pauseMenu.ShowPause();
				pause = true;
			} else {
                 pauseMenu.Hide(); 
				 pause = false;
            }
      	}

		if (player == null) {
			SceneManager.LoadScene(sceneName);
		}

		 // If the player presses the p key, restart the level
        if (Input.GetButtonDown("Restart")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

	public void fading(){
		animator.SetBool("fade", true);
		InvokeRepeating("NextLevel", 1.01f, 0.01f);
	}

	public void endFading(){
		animatorEnd.SetBool("fade", true);
		InvokeRepeating("EndGame", 4.02f, 0.1f);
	}

	public void NextLevel(){
		if (fadingTransition.color.a == 1){
			if (sceneName == "Beginning"){
				SceneManager.LoadScene("L1-Tutorial");
			} else if(sceneName == "L1-Tutorial"){
				SceneManager.LoadScene("L1-Tutorial2");
			} else if (sceneName == "L1-Tutorial2"){
				SceneManager.LoadScene("L1-Tutorial3");
			} else if (sceneName == "L1-Tutorial3"){
				SceneManager.LoadScene("L1-1");
			} else if (sceneName == "L1-1"){
				SceneManager.LoadScene("L2-Tutorial");
			} else if (sceneName == "L2-Tutorial"){
				SceneManager.LoadScene("L2-1");
			} else if (sceneName == "L2-1"){
				SceneManager.LoadScene("L2-2");
			} else if (sceneName == "L2-2"){
				SceneManager.LoadScene("L3-Tutorial");
			} else if (sceneName == "L3-Tutorial"){
				SceneManager.LoadScene("L3-1");
			} else if (sceneName == "L3-1"){
				SceneManager.LoadScene("L4-Tutorial");
			} else if (sceneName == "L4-Tutorial"){
				SceneManager.LoadScene("L4-1");
			} else if (sceneName == "L4-1"){
				SceneManager.LoadScene("L5-1");
			} else if (sceneName == "L5-1"){
				SceneManager.LoadScene("Ending");
			}
		}
	}
	public void EndGame(){
		if (fadingEndTransition.color.a == 1){
			SceneManager.LoadScene("GameOver");
		}
	}
}
