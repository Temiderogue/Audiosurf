using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private bool _isLoopStarted;
    [SerializeField] private bool _isPlayerDead;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Image _blackScreen;
    [SerializeField] private Text _playButton;
    [SerializeField] private Text _restartButton;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _backToMenu;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _quit;

    private int _score;
    public bool isGameStarted = false;

    private void Start()
    {
        if (!_isLoopStarted) 
        { 
            _scoreText.enabled = false;
            _quit.enabled = true;
        }
        else 
        {
            _quit.enabled = false;
            _scoreText.enabled = true;
            StartCoroutine(ScoreCount());
        }

        _blackScreen.enabled = false;
        _restartButton.enabled = false;
        _highScoreText.enabled = false;
        _backToMenu.enabled = false;
        
    }

    public void StartGame()
    {
        StartCoroutine(GameStart());
        StartCoroutine(ScoreCount());
    }

    public IEnumerator GameStart()
    {
        Time.timeScale = 1f;
        _quit.enabled = false;
        isGameStarted = true;
        _player.Speed = 0.1f;
        _scoreText.enabled = true;
        _playButton.enabled = false;
        _spawner.CreateCones();
        _spawner._isConeSpawned = true;
        _quit.enabled = false;
        _animator.SetBool("StartGame", true);
        yield return new WaitForSeconds(2f);
        _animator.SetBool("StartGame", false);
        _animator.enabled = false;
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadLevel()
    {
        _player.Speed = 0.1f;
        StartCoroutine(ScoreCount());
        Time.timeScale = 1;
        _scoreText.enabled = true;
        _isPlayerDead = false;
        SceneManager.LoadScene(1);
    }

    public IEnumerator ScoreCount()
    {
        yield return new WaitForSeconds(1f);
        _score++;
        _scoreText.text = _score.ToString();
        StartCoroutine(ScoreCount());
    }

    public void Death()
    {

        _highScoreText.enabled = true;
        if (_score > Data.HighScore)
        {
            Data.HighScore = _score;
        }
        StopCoroutine(ScoreCount());
        _isPlayerDead = true;
        _audioSource.Stop();
        Time.timeScale = 0;
        _highScoreText.text = "HighScore: " + Data.HighScore.ToString();
        _blackScreen.enabled = true;
        _restartButton.enabled = true;
        _backToMenu.enabled = true;
        _quit.enabled = true;
    }
}
