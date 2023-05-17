using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMechanics : MonoBehaviour
{
    [SerializeField] ProjectileMechanics _projectilePrefab; 
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;

    [Header("Button Settings")]
    public FixedJoystick Shoot_Button;

    bool canFire{
        get{
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    float _coolDown;
     
    void Update()
    {
        if(canFire && (Input.GetMouseButton(0))){
            FireProjectile();
        }
    }

    void FireProjectile(){
        _coolDown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
