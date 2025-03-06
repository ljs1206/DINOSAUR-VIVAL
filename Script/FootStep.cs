using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private Transform[] footTrms;

    public void PlayFootStep(int foot)
    {
       int id= EazySoundManager.PlaySound(clips[Random.Range(1, clips.Length)], 1, false, footTrms[foot]);
       Audio audio =EazySoundManager.GetAudio(id);
       audio.Min3DDistance = 1f;
       audio.Max3DDistance = 15f;
    }
}
