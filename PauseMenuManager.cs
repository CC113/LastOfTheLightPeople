using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    // References to text objects on the panel
    public Text restartText = null;
    public Text quitText = null;

    private Scene currentScene;
    private string sceneName;

    // Array storing text objct with index
    // indicating the current selection
    int optionIdx = 0;
    Text[] optionArray;
   
    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;

        optionArray = new Text[2]; 
        optionArray[0] = restartText;
        optionArray[1] = quitText;   
    }
   
    // Show the pause menu
    public void ShowPause() {
        // Show the panel
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
   
    // Hide the menu panel
    public void Hide() {
        // Deactivate the panel
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
   
   // Execute a command base on command string (that string
   // corresponds to text of the entered option
   void ExecuteCommand(string command) {
      
        switch(command) {
        // For the restart option just reload current scene
        case "Restart":
            // Restart the level
            if(sceneName == "L1-1"){
                Total.total.prisoner1 = false;
            } else if(sceneName == "L2-1"){
                Total.total.prisoner2 = false;
            } else if(sceneName == "L3-1"){
                Total.total.prisoner3 = false;
            } else if(sceneName == "L4-1"){
                Total.total.prisoner4 = false;
            } else if(sceneName == "L5-1"){
                Total.total.prisoner5 = false;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            break;
         
        // For the quit option load the main menu scene         
        case "Quit":
            // Return to the main menu
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
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
      } else if(Input.GetKeyDown(KeyCode.UpArrow)) {
         // When user presses up arrow, go to previous option
         optionIdx--;
      } else if(Input.GetKeyDown(KeyCode.Return) ) {
         // If uses presses Enter, execute the command 
         // corresponding to the current option
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