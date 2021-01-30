using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    [SerializeField] AudioClip waitingRoom, booth;

    [SerializeField] float fadeTime = 1, boothVolume = 0.1f, waitingRoomAudio = 0.5f;

    private CrossFadeAudio audioFade;

    // Start is called before the first frame update
    void Start()
    {
        audioFade = GetComponent<CrossFadeAudio>();
    }

    [ContextMenu("Fade Into Booth")]
    private void FadeIntoBooth()
    {
        audioFade.CrossFade(booth, boothVolume, fadeTime);
    }

    [ContextMenu("Fade Into WaitingRoom")]
    private void FadeIntoWaitingRoom()
    {
        audioFade.CrossFade(waitingRoom, waitingRoomAudio, fadeTime);
    }
}
