using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    GameController gameController;
    Player player;
    public float speedMove = 15;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>(gameController);
        player = FindObjectOfType<Player>(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsGameOver() == false)
        {
            if (player.isDoubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speedMove * 2));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speedMove);
            }
            
        }
       
    }
}
