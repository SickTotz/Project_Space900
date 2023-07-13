using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar_Mechanics _healthbar;
    [SerializeField] ER_PlayerController _playerController;

    public void Update()
    {
        /* DEBUG HEALTH BAR AND STAMINA BAR */

        // Health input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerHeal(10);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        GameManager.gameManager._playerHealth.DamageUnit(damage);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Debug per le collisioni con gli asteroidi
            Debug.Log("Collision with asteroid: " + collision.gameObject.name + " - Reference: " + collision.gameObject.GetInstanceID());
            
            // Calcola il danno da applicare alla navicella
            int damage = CalculateDamageFromAsteroid(collision);

            // Applica il danno alla navicella
            PlayerTakeDamage(damage);

            // Effetto esplosione dell'asteroide
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid != null && asteroid._explosionPrefab != null)
            {
                Instantiate(asteroid._explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
    private int CalculateDamageFromAsteroid(Collision collision)
    {
        // Ottieni la velocità dell'asteroide
        float asteroidSpeed = collision.relativeVelocity.magnitude;

        // Ottieni la grandezza dell'asteroide
        float asteroidSize = collision.gameObject.transform.localScale.magnitude;

        // Calcola il danno basato sulla velocità e la grandezza dell'asteroide
        int damage = Mathf.RoundToInt((asteroidSpeed * 5f) + (asteroidSize * 5f));

        return damage;
    }
}
