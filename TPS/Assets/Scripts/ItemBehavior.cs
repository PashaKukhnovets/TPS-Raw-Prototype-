using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private GameBehavior gameManager;
    private GameObject _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("game manager");
        gameManager = _gameManager.GetComponent<GameBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Debug.Log("Item collected!");
            gameManager.Items += 1;
        }
    }
}
