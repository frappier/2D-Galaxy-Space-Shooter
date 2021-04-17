using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;

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

    [SerializeField]
    private bool _tripleShot = false;

    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
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

        /*
        // check if player position on the Y is > 0, then y position = 0
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }


        if (transform.position.y <= -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }
        */
        // The movement restriction of the Y axis can also be done using the Mathf.Clamp function
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        /*  This section will se left and right boundaries for the player
         
        if(transform.position.x >= 9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }

        if(transform.position.x < -9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        */
        // This can also be acheved usning the Mathf.Clamp method
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.3f, 9.3f), transform.position.y, 0);

        
        // This code will make the player appear on the opposite side as if wrapping if they
        //go off screen either left or right.
        
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
        _playerLives -= 1;

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

}
