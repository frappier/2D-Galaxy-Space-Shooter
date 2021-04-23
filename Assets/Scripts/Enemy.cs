using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 5.0f;

    private Player _player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        int randomStartX = Random.Range(-8, 9);
        int randomStartY = Random.Range(9, 12);
        transform.position = new Vector3(randomStartX, randomStartY, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        int randomX = Random.Range(-8, 9);
        int randomY = Random.Range(9, 12);

        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
                
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(randomX, randomY, 0);
        }

    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                int value = Random.Range(5, 13);
                _player.AddScore(value);
            }

            Destroy(this.gameObject);
        }
        
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
           
            Destroy(this.gameObject);
            
        }
    }

    
}
