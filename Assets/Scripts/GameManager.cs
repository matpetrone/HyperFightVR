using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Gradient winGradient;
    public Gradient loseGradient;
    public ParticleSystem particleRing;
    public GameObject gameOverText;
    public GameObject wonText;
    public AudioSource winAudio;
    public AudioSource gameoverAudio;

    private PlayerController playerController;
    private EnemyController enemyController;
    private int playerHealth;
    private int enemyHealth;
    private bool gameAlive;
    private int loadMenuSec = 20;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
        gameAlive = true;
        wonText.SetActive(false);
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = playerController.currentHealth;
        enemyHealth = enemyController.currentHealth;

        if (gameAlive)
        {
            
            if (enemyHealth <= 0)
            {
                GameWon();
                gameAlive = false;
            }
                

            if (playerHealth <= 0)
            {
                GameOver();
                gameAlive = false;
            }
                
        }
        
    }

    void GameOver()
    {
        var col = particleRing.colorOverLifetime;
        col.color = loseGradient;
        enemyController.Won();
        gameOverText.SetActive(true);
        gameoverAudio.Play();
        StartCoroutine("ResetScene");
    }

    void GameWon()
    {
        var col = particleRing.colorOverLifetime;
        col.color = winGradient;
        enemyController.Dead();
        wonText.SetActive(true);
        winAudio.Play();
        StartCoroutine("ResetScene");
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(loadMenuSec);
        SceneManager.LoadScene("MainMenu");
    }

}
