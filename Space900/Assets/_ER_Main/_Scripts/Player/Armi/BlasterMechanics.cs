using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMechanics : MonoBehaviour
{
    [SerializeField] ProjectileMechanics _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;

    bool _isFiring = false;
    private float _nextFireTime = 0f;

    private PlayerBehaviour playerBehaviour;
    private GameOverManager gameOverManager;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        if (playerBehaviour == null)
        {
            Debug.LogError("PlayerBehaviour not found in the scene!");
        }

        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager not found in the scene!");
        }
    }

    private bool IsTouchInput()
    {
        // Controlla se il tasto/tocco fuoco è stato premuto
        return Input.GetButton("Fire1");
    }

    private void Update()
    {
        if (IsTouchInput() && !PauseMenu.GameIsPaused && !gameOverManager.isGameOver)
        {
            if (!_isFiring && Time.time >= _nextFireTime)
            {
                FireProjectile();
                _nextFireTime = Time.time + _coolDownTime;
            }
        }
        else
        {
            StopFiring();
        }
    }

    private void StartFiring()
    {
        _isFiring = true;
    }

    private void StopFiring()
    {
        _isFiring = false;
    }

    private void FireProjectile()
    {
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
