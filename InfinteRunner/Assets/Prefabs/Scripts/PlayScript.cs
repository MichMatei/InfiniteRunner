using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour
{
    public AudioSource buttonPress;

    public void playSoundEffect()
    {
        buttonPress.Play();
    }
}
