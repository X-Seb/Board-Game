using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class GGManager : MonoBehaviour
{
    public static GGManager inst;
    [Header("Settings")]
    [SerializeField] private bool m_debugMessages;
    [SerializeField] private GameState m_startingGameState;
    [Header("Game")]
    [SerializeField] private GameState m_currentGameState;
    [Header("Audio")]
    [SerializeField] private AudioSource m_globalAudioSource;
    [SerializeField] private AudioMixer m_mainMixer;
    [SerializeField] private string m_volumeParameterName;

    #region Enums

    public enum GameState
    {
        start,
        playing,
        paused,
        lose,
        win,
        menu
    }

    #endregion

    #region Unity methods

    private void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(gameObject);
            return;
        }
        else { inst = this; }
    }

    private void Start()
    {
        m_currentGameState = m_startingGameState;
        if (PlayerPrefs.HasKey(m_volumeParameterName))
        {
            SetVolume(PlayerPrefs.GetFloat(m_volumeParameterName));
        }
        else { SetVolume(0.0f); }
    }

    #endregion

    #region Getter and Setter methods

    public void SetState(GameState newState)
    {
        m_currentGameState = newState;
        Debug.Log("The current game state is: " + newState);
    }

    public GameState GetState() { return m_currentGameState; }

    public bool IsPlaying() { return m_currentGameState == GameState.playing; }

    public bool IsPaused() { return m_currentGameState == GameState.paused; }

    #endregion

    #region Other public methods

    public void OpenURL(string url) { Application.OpenURL(url); }

    public void QuitApp() { Application.Quit(); }

    public void SetVolume(float volume)
    {
        m_mainMixer.SetFloat(m_volumeParameterName, volume);
        PlayerPrefs.SetFloat(m_volumeParameterName, volume);
    }

    public void PlaySFX(AudioClip clip, AudioSource source, float volume = 1.0f)
    {
        source.PlayOneShot(clip, volume);
    }

    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void SaveAll()
    {
        PlayerPrefs.Save();
    }

    public void DebugLog(string message)
    {
        Debug.Log(message);
    }

    #endregion

    #region Private methods



    #endregion
}