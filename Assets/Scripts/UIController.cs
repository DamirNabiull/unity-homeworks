using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class UIController : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip clickSound;
    public TextMeshProUGUI killedEnemiesCountText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerName;
    public Slider speedSlider;
    public GameObject settingsForm;
    
    private static int _killedEnemies = 0;
    private static int _characterHealth = 0;
    private bool _isPaused = false;
    
    private MouseLookX _lookX;
    private MouseLookY _lookY;

    private int _currentMusicId = 0;
    private const int MusicCount = 2;
    private string[] _musicList = new[]
    {
        "Music/06 - Quad machine",
        "Music/07 - Big Gun"
    };


    private void Start()
    {
        settingsForm.SetActive(false);
        
        _lookX = FindObjectOfType<MouseLookX>();
        _lookY = FindObjectOfType<MouseLookY>();

        playerName.text = "Player";
        
        PlayMusic(false);
        
        SetSpeedForAll(speedSlider.value);
    }

    private void Update()
    {
        killedEnemiesCountText.text = _killedEnemies.ToString();
        healthText.text = _characterHealth.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            _isPaused = true;
            EnablePause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            _isPaused = false;
            DisablePause();
        }
    }
    
    public void OnMusicValue(float volume)
    {
        musicSource.volume = volume;
    }
    
    public void OnSoundValue(float volume)
    {
        AudioListener.volume = volume;
    }

    public void PlayClickSound()
    {
        soundSource.PlayOneShot(clickSound);
    }

    public static void EnemyKilled()
    {
        _killedEnemies++;
    }

    public static void SetHealth(int health)
    {
        _characterHealth = health;
    }

    public void SetPlayerName(string playerNameString)
    {
        playerName.text = playerNameString;
    }

    public void SetSpeedForAll(float speed)
    {
        SceneController.SetSpeedForAll(speed);
    }

    public void ShowSettings()
    {
        PlayClickSound();
        settingsForm.SetActive(true);
    }
    
    public void HideSettings()
    {
        PlayClickSound();
        settingsForm.SetActive(false);
    }

    private void EnablePause()
    {
        PlayClickSound();
        
        Time.timeScale = 0;
        
        RayShooter.SetInMenu(true);
        RigidbodyShooter.SetInMenu(true);
        WanderingAI.SetInMenu(true);
        
        _lookX.enabled = false;
        _lookY.enabled = false;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
    private void DisablePause()
    {
        PlayClickSound();
        
        HideSettings();
        Time.timeScale = 1;
        
        RayShooter.SetInMenu(false);
        RigidbodyShooter.SetInMenu(false);
        WanderingAI.SetInMenu(false);
        
        _lookX.enabled = true;
        _lookY.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayMusic(bool isButton)
    {
        if (isButton)
        {
            PlayClickSound();
        }
        musicSource.clip = Resources.Load(_musicList[_currentMusicId]) as AudioClip;
        musicSource.Play();
    }
    
    public void PlayNextMusic(bool isButton)
    {
        if (isButton)
        {
            PlayClickSound();
        }
        
        _currentMusicId = (_currentMusicId + 1) % MusicCount;
        musicSource.clip = Resources.Load(_musicList[_currentMusicId]) as AudioClip;
        musicSource.Play();
    }
    
    public void StopMusic(bool isButton)
    {
        if (isButton)
        {
            PlayClickSound();
        }
        
        musicSource.Stop();
    }
}
