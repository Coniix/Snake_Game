using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class OptionsScreen : MonoBehaviour
{
    public TMP_Text ResolutionLabel;
    public Toggle fullScreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;

    public AudioMixer theMixer;
    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;


    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0) {
            vsyncTog.isOn = false;
        } else {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++) {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical) {
                foundRes = true;
                selectedRes = i;
                updateResLabel();
            }
        }
        if (!foundRes) {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;
            updateResLabel();
        }

        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0) {
            selectedRes = 0;
        }
        updateResLabel();
    }

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1) {
            selectedRes = resolutions.Count - 1;
        }
        updateResLabel();
    }

    public void updateResLabel()
    {
        ResolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    public void applyGraphics()
    {
        //Screen.fullScreen = fullScreenTog.isOn;
        if (vsyncTog.isOn) {
            QualitySettings.vSyncCount = 1;
        } else {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullScreenTog.isOn);
    }

    public void setMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void setMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void setSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
