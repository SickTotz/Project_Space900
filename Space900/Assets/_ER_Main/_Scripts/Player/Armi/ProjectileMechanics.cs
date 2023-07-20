using UnityEngine;

public class ProjectileMechanics : MonoBehaviour
{   
    [SerializeField] [Range(5000f, 25000f)]
    private float _launchForce = 10000f;
    [SerializeField] [Range(10f, 1000f)]
    private int _damage = 100;
    [SerializeField] [Range(2f, 10f)]
    private float _range = 2f;
    [SerializeField] [Range(0f, 1f)]
    public float volume = 1f;
    [SerializeField]
    private AudioClip _shootSound;

    private bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    private Rigidbody _rigidbody;
    private float _duration;
    private AudioSource _audioSource;
    private bool _isFiring = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    private void OnEnable()
    {
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;

        if (_shootSound != null && !_isFiring)
        {
            _audioSource.volume = volume;
            _audioSource.PlayOneShot(_shootSound);
            _isFiring = true; // Imposta la variabile _isFiring a true quando il proiettile viene sparato
        }
    }

    private void Update()
    {
        if (OutOfFuel)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damageable.TakeDamage(_damage, hitPosition);
        }

        // Resetta la variabile _isFiring dopo che il proiettile ha colpito qualcosa
        _isFiring = false;
    }
}
