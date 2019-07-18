using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // References to text objects on the panel
    public Text playText = null;
    public Text continueText = null;
    public Text puzzleText = null;
    public Text quitText = null;

    private Scene currentScene;
    private string levelReturn;
    private int prisoner1;
    private int prisoner2;
    private int prisoner3;
    private int prisoner4;
    private int prisoner5;
    private int level;

    // Audio
    private AudioSource menuSound;

    public AudioClip buttonSound;

    // Array storing text objct with index
    // indicating the current selection
    int optionIdx = 0;
    Text[] optionArray;
   
    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene();
        optionArray = new Text[4]; 
        optionArray[0] = playText;
        optionArray[1] = continueText;
        optionArray[2] = puzzleText; 
        optionArray[3] = quitText;
        menuSound = GetComponent<AudioSource>();
        menuSound.Play();    
    }
   
   // Execute a command base on command string (that string
   // corresponds to text of the entered option
   void ExecuteCommand(string command) {
      
        switch(command) {
        // Start the new game at the first level        
        case "Play":
            SceneManager.LoadScene("Beginning");
            break;
        // For the continue option load where the player left off
        case "Continue":
            prisoner1 = PlayerPrefs.GetInt("Prisoner1");
            if (prisoner1 == 0) {
                Total.total.prisoner1 = false;
            } else {
                Total.total.prisoner1 = true;
            }
            prisoner2 = PlayerPrefs.GetInt("Prisoner2");
            if (prisoner2 == 0) {
                Total.total.prisoner2 = false;
            } else {
                Total.total.prisoner2 = true;
            }
            prisoner3 = PlayerPrefs.GetInt("Prisoner3");
            if (prisoner3 == 0) {
                Total.total.prisoner3 = false;
            } else {
                Total.total.prisoner3 = true;
            }
            prisoner4 = PlayerPrefs.GetInt("Prisoner4");
            if (prisoner4 == 0) {
                Total.total.prisoner4 = false;
            } else {
                Total.total.prisoner4 = true;
            }
            prisoner5 = PlayerPrefs.GetInt("Prisoner5");
            if (prisoner5 == 0) {
                Total.total.prisoner5 = false;
            } else {
                Total.total.prisoner5 = true;
            }

            level = PlayerPrefs.GetInt("Level");
            if (level == 0) {
                levelReturn = "Beginning";
            } else if (level == 1) {
                levelReturn = "L1-1";
            } else if (level == 2) {
                levelReturn = "L2-1";
            } else if (level == 3) {
                levelReturn = "L2-2";
            } else if (level == 4) {
                levelReturn = "L3-1";
            } else if (level == 5) {
                levelReturn = "L4-1";
            } else if (level == 6) {
                levelReturn = "L5-1";
            }
            SceneManager.LoadScene(levelReturn);
            break;
        // Load the level select screen       
        case "Puzzles":
            SceneManager.LoadScene("LevelSelect");
            break;
        // For the quit option close the application    
        case "Quit":
            Application.Quit();
            break;
        }
   }
   
   // Update is called once per frame
   void Update () {
      // Get a reference to the currently selected text box   
      Text currentSelection = optionArray[optionIdx];
      
      if(Input.GetKeyDown(KeyCode.DownArrow)) {
         // When user presses down arrow, go to next option
         optionIdx++;
         AudioSource.PlayClipAtPoint(buttonSound, transform.position);
      } else if(Input.GetKeyDown(KeyCode.UpArrow)) {
         // When user presses up arrow, go to previous option
         optionIdx--;
         AudioSource.PlayClipAtPoint(buttonSound, transform.position);
      } else if(Input.GetKeyDown(KeyCode.Return)) {
         // If uses presses Enter or "Jump" key (Space), execute
         // the command corresponding to the current option
         ExecuteCommand(currentSelection.text);
      }
      
      // Make sure that the option index indicator is within the range
      // of the number of options
      if(optionIdx < 0) {
         optionIdx = 0;
      } else if(optionIdx >= optionArray.Length) {
         optionIdx = optionArray.Length-1;
      }
      
      // Set the font colour of the all option text boxes to black   
      for(int i = 0; i < optionArray.Length; i++)
      {
         optionArray[i].color = Color.black;
      }
      // Set the font colour of the currently selected text box to white
      currentSelection.color = Color.white;
   }
}