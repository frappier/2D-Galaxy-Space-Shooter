using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private float _wait = 5.0f;
    [SerializeField] // 0 = Triple Shot, 1 = Speed, 2 = Shields
    private int _powerupID;
        
    
    // Start is called before the first frame update
    void Start()
    {
        float randomStartX = Random.Range(-8, 8);
        transform.position = new Vector3(randomStartX, 9, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        int randomX = Random.Range(-8, 8);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (_powerupID)
                {
                    case 0: // Triple Shot
                        player.TripleShot(_wait);
                        break;
                    case 1: //Speed
                        player.SpeedBoost();
                        break;
                    case 2: //Shield
                        Debug.Log("Shield Activated");
                        break;
                    default:
                        Debug.Log("Invalid powerupID");
                        break;
                }

                
            }
            
            Destroy(this.gameObject);
        }
    }
    
}
