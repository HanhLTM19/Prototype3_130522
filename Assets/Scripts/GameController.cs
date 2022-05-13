using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool m_isGameOver;
    // Start is called before the first frame update
    public GameObject obstacles;
    Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 1;
    private float repeatRate = 2;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver())
        {
            Debug.Log("Game over");
        }
    }
    public void SpawnObstacle()
    {
        if (IsGameOver() == false)
        {
            Instantiate(obstacles, spawnPos, obstacles.transform.rotation);
        }
        
    }
    public void SetIsGameOver (bool state)
    {
        m_isGameOver = state;
    }
   public bool IsGameOver()
    {
        return m_isGameOver;
    }
}
