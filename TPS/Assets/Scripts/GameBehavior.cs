using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;

    private bool showWinScreen = false;
    private bool showLossScreen = false;

    private string _state;
    public string State
    {
        get {
            return _state;
        }
        set {
            _state = value;
        }
    }

    private int _itemsCollected = 0;
    public int Items
    {
        get {
            return _itemsCollected;
        }

        set {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                EndGame("You've found all the items!", 0);
            }
            else {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 3;

    public int HP
    {
        get {
            return _playerHP;
        }

        set {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                EndGame("You want another life with that?", 1);
            }
            else {
                labelText = "Ouch... that's got hurt.";
            }
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        GUI.Label(new Rect(Screen.width / 2 - 4, Screen.height / 2 - 10, 24, 24), "+");

        if (showWinScreen) {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!")) {
                Utilities.RestartLevel();
            }
        }
        if (showLossScreen) {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose...")) {
                Utilities.RestartLevel();
            }
        }
    }

    void EndGame(string message, int numberOfScreen) {
        labelText = message;
        if (numberOfScreen == 0)
        {
            showWinScreen = true;
        }
        else if (numberOfScreen == 1) {
            showLossScreen = true;
        }
        Time.timeScale = 0.0f;
    }

    public void Initialize() {
        _state = "Manager initialized...";
        _state.FancyDebug();
        Debug.Log(_state);
    }
}