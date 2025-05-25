using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public Camera fpsCam;
    public Transform firePoint;
    public Animator animator;
    public LayerMask hitMask;
    public AudioSource fireAudioSource;
    public AudioSource reloadAudioSource;
    public float knockbackForce = 10f;
    public float damage = 25f;
    public float range = 100f;
    public int maxClipSize = 10;
    public int maxReserveAmmo = 20;

    private int _currentAmmo;
    private int _reserveAmmo;
    private GameController _gameController;

    private static readonly int ShootTrigger = Animator.StringToHash("Shoot");
    private bool _canShoot = true;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _currentAmmo = maxClipSize;
        _reserveAmmo = maxReserveAmmo;
        _gameController.UpdateAmmo(_currentAmmo, _reserveAmmo);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (_currentAmmo <= 0) return;

        _canShoot = false;
        _currentAmmo--;
        _gameController.UpdateAmmo(_currentAmmo, _reserveAmmo);
        animator.SetTrigger(ShootTrigger);
        fireAudioSource.Play();
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, range, hitMask)) return;

        var zombie = hit.collider.GetComponentInParent<ZombieHealth>();
        if (!zombie) return;

        bool isHeadshot = hit.collider.CompareTag("ZombieHead");
        zombie.TakeDamage(damage, isHeadshot);
        zombie.ApplyKnockback((hit.point - firePoint.position).normalized, knockbackForce);
    }

    private void Reload()
    {
        int needed = maxClipSize - _currentAmmo;
        if (needed <= 0 || _reserveAmmo <= 0) return;

        reloadAudioSource.Play();
        if (_reserveAmmo >= needed)
        {
            _currentAmmo += needed;
            _reserveAmmo -= needed;
        }
        else
        {
            _currentAmmo += _reserveAmmo;
            _reserveAmmo = 0;
        }
        _gameController.UpdateAmmo(_currentAmmo, _reserveAmmo);
    }
    
    public void RefillAmmo()
    {
        reloadAudioSource.Play();
        _currentAmmo = maxClipSize;
        _reserveAmmo = maxReserveAmmo;
        _gameController.UpdateAmmo(_currentAmmo, _reserveAmmo);
    }

    public void OnShootAnimationEnd()
    {
        _canShoot = true;
    }
}
