using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    GameController gameController;
    float speedMove = 15;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>(gameController);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsGameOver() == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speedMove);
        }
       
    }
}
