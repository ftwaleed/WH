using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    private float touchTime;
    private bool enemyTouch;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null)
        {
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        }
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        
    }

    private IEnumerator DamageOverTime()
    {
        while (enemyTouch)
        {
            TakeDamage(maxHealth / 3);
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
{
    if (collision.collider.CompareTag("Enemy"))
    {
        touchTime += Time.deltaTime;
        if (touchTime >= 3f && touchTime < 6f)
        {
            if (!enemyTouch)
            {
                enemyTouch = true;
                StartCoroutine(DamageOverTime());
            }
        }
        else if (touchTime >= 6f && touchTime < 9f)
        {
            // Do nothing, already taking damage
        }
        else if (touchTime >= 9f)
        {
            if (enemyTouch)
            {
                enemyTouch = false;
                StopAllCoroutines();
                TakeDamage(maxHealth / 3);
                canvas.SetActive(true);
                
            }
        }
    }
    else if (collision.collider.CompareTag("EnemyDetection"))
    {
        // Player detected by second collider, set enemyTouch to true
        enemyTouch = true;
        Debug.Log("Game Over22");
    }
}


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            touchTime = 0f;
            enemyTouch = false;
        }
        else if (collision.gameObject.CompareTag("EnemyDetection"))
        {
            // Player no longer detected by second collider, set enemyTouch to false
            enemyTouch = false;
            Debug.Log("Game Over222");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
