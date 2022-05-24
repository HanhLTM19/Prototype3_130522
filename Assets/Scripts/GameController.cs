using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool m_isGameOver;
    // Start is called before the first frame update
    public GameObject[] obstacles;
    Vector3 spawnPos;
    private float startDelay = 1;
    private float repeatRate = 2;

    public Transform startingPonit;
    public float lerpSpeed;
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver())
        {
            Debug.Log("Game over");
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startingPonit.position;
        float m_journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float m_distance = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = m_distance / m_journeyLength;

        player.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            m_distance = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = m_distance / m_journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        player.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);

    }
    public void SpawnObstacle()
    {
        if (IsGameOver() == false)
        {
            int index;
            index = Random.Range(0, obstacles.Length);
            spawnPos = new Vector3(25,obstacles[index].transform.position.y, 0);
            Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
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
