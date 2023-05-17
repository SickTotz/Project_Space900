using UnityEngine;

public class ProjectileMechanics : MonoBehaviour
{   
    [SerializeField] [Range(5000f, 25000f)]
    float _launchForce = 10000f;
    [SerializeField] [Range(10f, 1000f)] int _damage = 100;
    [SerializeField] [Range(2f, 10f)] float _range = 5f;

    bool OutOfFuel{
        get{
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    Rigidbody _rigidbody;
    float _duration;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;
    }

    void Update() {
        if(OutOfFuel){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(message: $"projectile collided with {collision.collider.name}");
    }
}
