using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    
    // References to text objects on the panel
    public Text oneText = null;
    public Text twoText = null;
    public Text threeText = null;
    public Text fourText = null;
    public Text fiveText = null;
    public Text sixText = null;
    public Text returnText = null;

    // Array storing text objct with index
    // indicating the current selection
    int optionIdx = 0;
    Text[] optionArray;

    // Audio
    private AudioSource levelSelectSound;

    public AudioClip buttonSound;
   
    // Use this for initialization
    void Start () {
        optionArray = new Text[7]; 
        optionArray[0] = oneText;
        optionArray[1] = twoText;
        optionArray[2] = threeText; 
        optionArray[3] = fourText;
        optionArray[4] = fiveText;  
        optionArray[5] = sixText;  
        optionArray[6] = returnText;    
        levelSelectSound = GetComponent<AudioSource>();
        levelSelectSound.Play();    
    }
   
   // Execute a command base on command string (that string
   // corresponds to text of the entered option
   void ExecuteCommand(string command) {
      
        switch(command) {
        // Load level 1       
        case "Puzzle 1":
            SceneManager.LoadScene("L1-1");
            break;
        // Load level 2      
        case "Puzzle 2":
            SceneManager.LoadScene("L2-1");
            break;
        // Load level 3       
        case "Puzzle 3":
            SceneManager.LoadScene("L2-2");
            break;
        // Load level 4       
        case "Puzzle 4":
            SceneManager.LoadScene("L3-1");
            break;
        // Load level 5       
        case "Puzzle 5":
            SceneManager.LoadScene("L4-1");
            break;
        // Load level 6   
        case "Puzzle 6":
            SceneManager.LoadScene("L5-1");
            break;
        // Return to the main menu   
        case "Return":
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