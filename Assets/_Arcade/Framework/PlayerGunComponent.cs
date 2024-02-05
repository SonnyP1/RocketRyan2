using UnityEngine;

public class PlayerGunComponent : MonoBehaviour
{
    
    [SerializeField] Transform BombSpawnPoint;
    [SerializeField] AudioSource ShootingSound;

    private Transform _projectileSpawnPoint;

    private GameObject _projectile;
    private GameObject _bomb;

    private int _bomberAmmo = 3;
    private int _bomberMaxAmmo = 3;

    //Getters & Setters
    public GameObject ProjectilePrefab
    {
        set { _projectile = value; }
        get { return _projectile; }
    }

    public GameObject BombPrefab
    {
        set { _bomb = value; }
        get { return _bomb; }
    }

    public int BombMaxAmmo
    {
        set { _bomberMaxAmmo = value; }
        get { return _bomberMaxAmmo; }
    }

    public Transform ProjectileSpawnPoint
    {
        set { _projectileSpawnPoint = value; }
        get { return _projectileSpawnPoint; }
    }

    private void Start()
    {
        _bomberAmmo = ScoreKeeper.m_scoreKeeper.GetCurrentBombAmmo();
        UpdateBombUI();
    }
    internal void Fire()
    {
        GameObject newObject = Instantiate(_projectile,ProjectileSpawnPoint);
        newObject.transform.parent = null;
        if(ShootingSound.isPlaying)
        {
            ShootingSound.Stop();
        }
        ShootingSound.Play();
    }

    internal void DropBomb()
    {
        if(_bomberAmmo != 0)
        {
            _bomberAmmo = Mathf.Clamp(_bomberAmmo - 1, 0, _bomberMaxAmmo);
            GameObject newObject = Instantiate(_bomb, BombSpawnPoint);
            newObject.transform.parent = null;
            UpdateBombUI();
        }
    }

    public void AddBomb()
    {
        _bomberAmmo = Mathf.Clamp(_bomberAmmo + 1, 0, _bomberMaxAmmo);
        UpdateBombUI();
    }


    private void UpdateBombUI()
    {
        ScoreKeeper.m_scoreKeeper.UpdateBombAmmo(_bomberAmmo);
        ScoreKeeper.m_scoreKeeper.GetGameplayUIManager().UpdateBombUI(_bomberAmmo);
    }
}
