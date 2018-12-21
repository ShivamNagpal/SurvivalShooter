using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class EnemySpawnner
{
    public GameObject enemy;
    public Transform[] spawnPoints;
}

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public EnemySpawnner[] enemySpawnners;
    public float spawnStartDelay = 1f;
    public float spawnWait = 3f;
    public PlayerHealth playerHealth;
    public Text restartText;
    public Text gameOverText;

    private int score;
    private bool restart;

    private void Start()
    {
        score = 0;
        UpdateScoreText();

        restartText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        restart = false;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnStartDelay);
        while (true)
        {
            if (playerHealth.currentHealth <= 0)
            {
                restart = true;
                restartText.gameObject.SetActive(true);
                gameOverText.gameObject.SetActive(true);
                break;
            }

            int enemyIndex = Random.Range(0, enemySpawnners.Length);
            EnemySpawnner es = enemySpawnners[enemyIndex];

            int transformIndex = Random.Range(0, es.spawnPoints.Length);
            Instantiate(es.enemy, es.spawnPoints[transformIndex].position, es.spawnPoints[transformIndex].rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }


    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}
