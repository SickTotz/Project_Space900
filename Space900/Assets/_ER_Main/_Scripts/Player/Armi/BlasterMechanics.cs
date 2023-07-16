using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMechanics : MonoBehaviour
{
    [SerializeField] ProjectileMechanics _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;

    bool _isFiring = false;
    bool _canFire = true;

    private void Update()
    {
        if (_canFire && !PauseMenu.GameIsPaused && IsTouchInput())
        {
            StartFiring();
        }
        else if (!_canFire && (!IsTouchInput() || PauseMenu.GameIsPaused))
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
        if (!_isFiring)
        {
            _isFiring = true;
            StartCoroutine(FireCoroutine());
        }
    }

    private void StopFiring()
    {
        _isFiring = false;
        _canFire = true;
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
        _canFire = false;
    }
}
