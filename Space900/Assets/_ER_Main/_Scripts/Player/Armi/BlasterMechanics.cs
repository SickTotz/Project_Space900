using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMechanics : MonoBehaviour
{
    [SerializeField] ProjectileMechanics _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;

    bool _isFiring = false;

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

    private void Update()
    {
        if (IsTouchInput() && !PauseMenu.GameIsPaused && !gameOverManager.isGameOver)
        {
            if (!_isFiring)
            {
                StartFiring();
            }
        }
        else
        {
            StopFiring();
        }
    }

    private bool IsTouchInput()
    {
        // Controlla se è stato effettuato un tocco su schermo
        return Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended;
    }

    private void StartFiring()
    {
        _isFiring = true;
        StartCoroutine(FireCoroutine());
    }

    private void StopFiring()
    {
        _isFiring = false;
    }

    private IEnumerator FireCoroutine()
    {
        while (_isFiring)
        {
            FireProjectile();
            yield return new WaitForSeconds(_coolDownTime);
        }
    }

    private void FireProjectile()
    {
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
