using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Recorder.OutputPath;

public class EatSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioS;
    [SerializeField]
    private Transform HandTrm;

    public void PlayEatSound()
    {
        EazySoundManager.PlaySound(audioS, 2, false, HandTrm);
    }
}
