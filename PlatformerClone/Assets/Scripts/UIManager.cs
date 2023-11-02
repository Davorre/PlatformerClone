using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Marsh, Jeremy
// 10/29/2023
// General UI

public class UIManager : MonoBehaviour
{

    // public variables to modulate the UI
    public PlayerControllerTest playerController;
    public TMP_Text livesText;
    public TMP_Text hpText;
    public TMP_Text wumpaFruitText;


    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + playerController.lives;
        hpText.text = "HP: " + playerController.hp;
        wumpaFruitText.text = "Wumpa Fruit: " + playerController.wumpaFruit;
    }
    /// <summary>
    /// on click quit game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Switches scene to designated scene in Unity
    /// </summary>
    /// <param name="sceneIndex">Scene index specifed by editor</param>
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
