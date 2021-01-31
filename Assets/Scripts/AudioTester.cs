using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    static AudioTester instance;
	private void Awake()
	{
        instance = this;
	}

	[SerializeField] AudioClip waitingRoom, booth;

    [SerializeField] float fadeTime = 1, boothVolume = 0.1f, waitingRoomAudio = 0.5f;

    private CrossFadeAudio audioFade;

    // Start is called before the first frame update
    void Start()
    {
        audioFade = GetComponent<CrossFadeAudio>();
    }

    [ContextMenu("Fade Into Booth")]
    public static void FadeIntoBooth()
    {
        instance.audioFade.CrossFade(instance.booth, instance.boothVolume, instance.fadeTime);
    }

    [ContextMenu("Fade Into WaitingRoom")]
    public static void FadeIntoWaitingRoom()
    {
        instance.audioFade.CrossFade(instance.waitingRoom, instance.waitingRoomAudio, instance.fadeTime);
    }
}
