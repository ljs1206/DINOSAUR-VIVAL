using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarSound : MonoBehaviour
{

    [SerializeField]
    private AudioClip audioS;
    [SerializeField]
    private Transform HandTrm;

    public void PlayRoarSound()
    {
        EazySoundManager.PlaySound(audioS, 100, false, HandTrm);
    }
}
