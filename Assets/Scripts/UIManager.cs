using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    _scoreText          = null;
    [SerializeField] private TextMeshProUGUI    _gameOverText       = null;
    [SerializeField] private TextMeshProUGUI    _restartText        = null;
    [SerializeField] private TextMeshProUGUI    _sceneStartText     = null;
    [SerializeField] private TextMeshProUGUI    _ammoText           = null;
    [SerializeField] private TextMeshProUGUI    _missleText         = null;
    [SerializeField] private Image              _livesImg           = null;
    [SerializeField] private Image              _thrusterChargeImg  = null;
    [SerializeField] private Sprite[]           _livesSprites       = null;
    [SerializeField] private GameObject         _pausePanel         = null;

    private float       _thrusterFill   = 0f;
    private GameManager _gameManager    = null;
    private Player      _player         = null;

    private void Awake()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null) Debug.Log("GameManage script is null");

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.Log("Player script is null");
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
        _sceneStartText.gameObject.SetActive(true);

        if(_gameManager.IsNewScene)
            StartCoroutine(StartSceneTimerRoutine());

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    private void Update()
    {
        _scoreText.text = "Score: " + _player.GetScore().ToString();
        _livesImg.sprite = _livesSprites[_player.GetLives()];
        _ammoText.text = _player.GetAmmo().ToString() + " / " + _player.GetMaxAmmo().ToString();
        _missleText.text = _player.GetMissleCount().ToString() + " / " + _player.GetMaxMissile().ToString();

        _thrusterFill = _player.ThrusterCharge / _player.ThrusterMaxCharge;
        if(_thrusterChargeImg != null)
            _thrusterChargeImg.fillAmount = _thrusterFill;

        if(_player.GetLives() == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
    }

    private IEnumerator StartSceneTimerRoutine()
    {
        _sceneStartText.text = "";
        yield return new WaitForSeconds(1f);
        _sceneStartText.text = "3";
        yield return new WaitForSeconds(1f);
        _sceneStartText.text = "2";
        yield return new WaitForSeconds(1f);
        _sceneStartText.text = "1";
        yield return new WaitForSeconds(1f);
        _sceneStartText.fontSize = 120;
        _sceneStartText.text = "GO!";
        yield return new WaitForSeconds(1f);
        _sceneStartText.gameObject.SetActive(false);

        _gameManager.IsNewScene = false;
    }

    public void PausePanel(bool activate)
    {
        _pausePanel.SetActive(activate);
    }
}
