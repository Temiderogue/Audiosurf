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
    [SerializeField] private Text _highScoreText;

    private int _score;

    private void Start()
    {
        if (!_isLoopStarted) 
        { 
            _scoreText.enabled = false;
        }
        else 
        {
            _scoreText.enabled = true;
            StartCoroutine(ScoreCount());
        }

        _blackScreen.enabled = false;
        _restartButton.enabled = false;
        _highScoreText.enabled = false;
    }

    public void StartGame()
    {
        StartCoroutine(GameStart());
        StartCoroutine(ScoreCount());
    }

    public IEnumerator GameStart()
    {
        _player.Speed = 5f;
        _scoreText.enabled = true;
        _playButton.enabled = false;
        _spawner.CreateCones();
        _spawner._isConeSpawned = true;
        _animator.SetBool("StartGame", true);
        yield return new WaitForSeconds(2f);
        _animator.SetBool("StartGame", false);
        _animator.enabled = false;
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }
    public void ReloadLevel()
    {
        _player.Speed = 5f;
        StartCoroutine(ScoreCount());
        Time.timeScale = 1;
        _scoreText.enabled = true;
        _isPlayerDead = false;
        SceneManager.LoadScene(1);
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene(0);
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
    }
}
