﻿using UnityEngine;

public class WavesSpawner : MonoBehaviour {

    static GameObject[] waves;
    int waveNumber;
    int WaveNumber
    {
        get
        {
            return waveNumber;
        }
        set
        {
            waveNumber = value;
            GameMaster.instance.UpdateWaveNumberUI(waveNumber); 
        }
    }
    bool waveInProgress = false;
    bool WaveInProgress
    {
        get
        {
            return waveInProgress;
        }
        set
        {
            waveInProgress = value;
            PlayWaveButton.instance.SetButton(waveInProgress); 
        }
    }
    public static WavesSpawner instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else this.enabled = false; 
        waves = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waves[i] = transform.GetChild(i).gameObject;
            waves[i].SetActive(false);
        }

        WaveNumber = 0; 
    }

    public void StartNewWave()
    {
        WaveInProgress = true; 
        waves[waveNumber].SetActive(true); 
    }

    public void StartWave()
    {
        waves[waveNumber].SetActive(true); 
    }

    public void WaveEnded()
    {
        WaveInProgress = false;
        WaveNumber++; 
    }

}