using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI killedEnemiesCountText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerName;
    public GameObject settingsForm;
    
    private static int _killedEnemies = 0;
    private static int _characterHealth = 0;
    private bool _isPaused = false;
    
    private MouseLookX _lookX;
    private MouseLookY _lookY;

    private void Start()
    {
        settingsForm.SetActive(false);
        
        _lookX = FindObjectOfType<MouseLookX>();
        _lookY = FindObjectOfType<MouseLookY>();

        playerName.text = "Player";
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

    public void ShowSettings()
    {
        settingsForm.SetActive(true);
    }
    
    public void HideSettings()
    {
        settingsForm.SetActive(false);
    }

    private void EnablePause()
    {
        Time.timeScale = 0;
        
        _lookX.enabled = false;
        _lookY.enabled = false;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
    private void DisablePause()
    {
        HideSettings();
        Time.timeScale = 1;
        
        _lookX.enabled = true;
        _lookY.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
