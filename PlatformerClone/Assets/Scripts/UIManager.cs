using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
