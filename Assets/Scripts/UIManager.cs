using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverTextWhite;
    [SerializeField]
    private Text _gameOverTextRed;
    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Image _LivesImage;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameOverTextWhite.gameObject.SetActive(false);
        _gameOverTextRed.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverTextWhite.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverTextWhite.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverTextRed.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverTextRed.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }

    }

    

}
