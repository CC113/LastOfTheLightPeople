using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;
    public SpriteRenderer sr4;
    public SpriteRenderer sr5;
    public Sprite star;

    void Update () {
        if(Total.total.prisoner1 == true){
			sr1.sprite = star;
		}
        if(Total.total.prisoner2 == true){
			sr2.sprite = star;
		}
        if(Total.total.prisoner3 == true){
			sr3.sprite = star;
		}
        if(Total.total.prisoner4 == true){
			sr4.sprite = star;
		}
        if(Total.total.prisoner5 == true){
			sr5.sprite = star;
		}
        if (Input.GetKeyDown(KeyCode.Return)) {
            Total.total.prisoner1 = false;
            Total.total.prisoner2 = false;
            Total.total.prisoner3 = false;
            Total.total.prisoner4 = false;
            Total.total.prisoner5 = false;
            // Return to the main menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}