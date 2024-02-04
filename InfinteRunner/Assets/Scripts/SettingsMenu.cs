using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    //using the unity engines audio mixer i have set the audio to go between 0 & -80
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    //On start of game records resolutions available.
    void Start()
    {
        resolutions = Screen.resolutions;
        //Clears resolution options
        resolutionDropdown.ClearOptions();
        //Lists all available options for resolutions

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        //Modular box that displays the resolutions inside.
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //Setting the prefered resolution.
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //Adds resolutions to the dropdown box.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //Allows mixer to alter volume in game.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    //Allows Player to change quality of the game by selecting which option they would like in game.
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Allows the player to set to full screen.
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
