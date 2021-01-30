using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingHandler : MonoBehaviour
{
	static PostProcessingHandler instance;

	DepthOfField depthOfField;

	private void Awake()
	{
		instance = this;

		PostProcessVolume volume = GetComponent<PostProcessVolume>();
		depthOfField = volume.profile.GetSetting<DepthOfField>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
	
	public static void SetFocusDistance(float focusDistance, float timeToFocus)
	{
		LeanTween.value(instance.depthOfField.focusDistance, focusDistance, timeToFocus).setOnUpdate((float value) => instance.depthOfField.focusDistance.value = value);
	}


}
