using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMechanics : MonoBehaviour
{
    [SerializeField] ProjectileMechanics _projectilePrefab; 
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;

    float _coolDown;

    bool canFire{
        get{
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    void Update()
    {
        if(canFire && IsFireButtonPressed()){
            FireProjectile();
        }
    }

    bool IsFireButtonPressed()
    {
        // Controlla se il bottone personalizzato "FireButton" è premuto
        return Input.GetButton("FireButton");
    }

    public void FireProjectile(){
        _coolDown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
