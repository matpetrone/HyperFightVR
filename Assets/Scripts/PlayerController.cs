using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar_Player healthbar;
    public int damage = 5;
    public GameObject mainCamera;
    public GameObject weapon;
    public AudioSource hitAudio;

    private string enemyWeapon = "EnemyKatana";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == enemyWeapon)
        {
            TakeDamage();
            hitAudio.Play();
        }
    }

    void TakeDamage()
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

}
