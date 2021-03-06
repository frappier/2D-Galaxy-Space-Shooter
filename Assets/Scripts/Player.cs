using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5.0f;
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserShot;

    private float _offset = 1.05f;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _playerLives = 3;
    private SpawnManager _spawnManager;

    private bool _tripleShot = false;
    private bool _isShieldsActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;

    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
        }

        _shieldVisualizer.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.Log("UIManager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }


    }

    void CalculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
                       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

                
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        
    }

    void FireLaser()
    {
        //cool down system. To restrict fire rate to every 0.5 seconds
        _nextFire = Time.time + _fireRate;

        if (_tripleShot == true)
        {
            Instantiate(_tripleLaserShot, transform.position, Quaternion.identity);
        }
        else
        {
            //Instantiate the laser with a 0.8f offset on the y axis from the player.
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
        }

        
    }

    public void Damage()
    {
        if(_isShieldsActive == true)
        {   
            return;
        }
        
        _playerLives -= 1;

        _uiManager.UpdateLives(_playerLives);

        if(_playerLives == 0)
        {
            Destroy(this.gameObject);

            _spawnManager.OnPlayerDeath();
        }
    }

    public void TripleShot(float _wait)
    {
        _tripleShot = true;
        StartCoroutine(TripleShotPowerDown(_wait));
    }

    IEnumerator TripleShotPowerDown(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        _tripleShot = false;
    }

    public void SpeedBoost()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ShieldsActivation()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldCoolDown());
    }

    IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualizer.SetActive(false);
        _isShieldsActive = false;
    }

    public void AddScore(int value)
    {
        _score += value;
        _uiManager.UpdateScore(_score);
    }

}
