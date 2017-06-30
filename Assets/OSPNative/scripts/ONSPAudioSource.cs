/************************************************************************************
Filename    :   ONSPAudioSource.cs
Content     :   Interface into the Oculus Native Spatializer Plugin
Created     :   September 14, 2015
Authors     :   Peter Giokaris
Copyright   :   Copyright 2015 Oculus VR, Inc. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.1 (the "License"); 
you may not use the Oculus VR Rift SDK except in compliance with the License, 
which is provided at the time of installation or download, or which 
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.1 

Unless required by applicable law or agreed to in writing, the Oculus VR SDK 
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
************************************************************************************/

// Uncomment below to test access of read-only spatializer parameters
//#define TEST_READONLY_PARAMETERS

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class ONSPAudioSource : MonoBehaviour
{
    // Spatializer read-only system parameters (global)
    static int readOnly_GlobalRelectionOn = 8;
    static int readOnly_NumberOfUsedSpatializedVoices = 9;

    // Import functions
    public const string strONSPS = "AudioPluginOculusSpatializer";

    [DllImport(strONSPS)]
    private static extern void ONSP_GetGlobalRoomReflectionValues(ref bool reflOn, ref bool reverbOn, 
                                                                  ref float width, ref float height, ref float length);

	// Public

	[SerializeField]
	private bool enableSpatialization = true;
	public  bool EnableSpatialization
	{
		get
		{
			return enableSpatialization;
		}
		set
		{
			enableSpatialization = value;
		}
	}

	[SerializeField]
	private float gain = 0.0f;
	public  float Gain
	{
		get
		{
			return gain;
		}
		set
		{
			gain = Mathf.Clamp(value, 0.0f, 24.0f);
		}
	}
	
	[SerializeField]
	private bool useInvSqr = false;
	public  bool UseInvSqr
	{
		get
		{
			return useInvSqr;
		}
		set
		{
			useInvSqr = value;		
		}
	}

	[SerializeField]
	private float near = 1.0f;
	public float Near
	{
		get
		{
			return near;
		}
		set
		{
			near = Mathf.Clamp(value, 0.0f, 1000000.0f);
		}
	}

	[SerializeField]
	private float far = 10.0f;
	public float Far
	{
		get
		{
			return far;
		}
		set
		{
			far = Mathf.Clamp(value, 0.0f, 1000000.0f);
		}
	}

    [SerializeField]
    private float volumetricRadius = 0.0f;
    public float VolumetricRadius
    {
        get
        {
            return volumetricRadius;
        }
        set
        {
            volumetricRadius = Mathf.Clamp(value, 0.0f, 1000.0f);
        }
    }

	[SerializeField]
	private bool enableRfl = false;
	public  bool EnableRfl
	{
		get
		{
			return enableRfl;
		}
		set
		{
			enableRfl = value;
		}
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// We might iterate through multiple sources / game object
		var source = GetComponent<AudioSource>();
		SetParameters(ref source);
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
		// We might iterate through multiple sources / game object
		var source = GetComponent<AudioSource>();

        // READ-ONLY PARAMETER TEST      
#if TEST_READONLY_PARAMETERS
        float rfl_enabled = 0.0f;
        source.GetSpatializerFloat(readOnly_GlobalRelectionOn, out rfl_enabled);
        float num_voices = 0.0f;
        source.GetSpatializerFloat(readOnly_NumberOfUsedSpatializedVoices, out num_voices);

        String readOnly = System.String.Format
        ("Read only values: refl enabled: {0:F0} num voices: {1:F0}", rfl_enabled, num_voices);
        Debug.Log(readOnly);
#endif

        // Check to see if we should disable spatializion
        if ((Application.isPlaying == false) || 
            (AudioListener.pause == true) || 
            (source.isPlaying == false) ||
            (source.isActiveAndEnabled == false)
           )
        {
            source.spatialize = false;
            return;
        }
        else
        {
            SetParameters(ref source);	
        }
    }

	/// <summary>
	/// Sets the parameters.
	/// </summary>
	/// <param name="source">Source.</param>
	public void SetParameters(ref AudioSource source)
	{
        // See if we should enable spatialization
        source.spatialize = enableSpatialization;
		
        source.SetSpatializerFloat(0, gain);
		// All inputs are floats; convert bool to 0.0 and 1.0
		if(useInvSqr == true)
			source.SetSpatializerFloat(1, 1.0f);
		else
			source.SetSpatializerFloat(1, 0.0f);

		source.SetSpatializerFloat(2, near);
		source.SetSpatializerFloat(3, far);

		if(enableRfl == true)
			source.SetSpatializerFloat(4, 0.0f);
		else
			source.SetSpatializerFloat(4, 1.0f);

        source.SetSpatializerFloat(5, volumetricRadius);
	}

    private static ONSPAudioSource RoomReflectionGizmoAS = null; 

    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmos()
    {
        // Are we the first one created? make sure to set our static ONSPAudioSource
        // for drawing out room parameters once
        if(RoomReflectionGizmoAS == null)
        {
            RoomReflectionGizmoAS = this;
        }

        Color c;
        const float colorSolidAlpha = 0.1f;

        // Draw the near/far spheres

        // Near (orange)
        c.r = 1.0f;
        c.g = 0.5f;
        c.b = 0.0f;
        c.a = 1.0f;
        Gizmos.color = c;
        Gizmos.DrawWireSphere(transform.position, Near);
        c.a = colorSolidAlpha;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, Near);

        // Far (red)
        c.r = 1.0f;
        c.g = 0.0f;
        c.b = 0.0f;
        c.a = 1.0f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Far);
        c.a = colorSolidAlpha;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, Far);
        
        // VolumetricRadius (purple)
        c.r = 1.0f;
        c.g = 0.0f;
        c.b = 1.0f;
        c.a = 1.0f;
        Gizmos.color = c;
        Gizmos.DrawWireSphere(transform.position, VolumetricRadius);
        c.a = colorSolidAlpha;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, VolumetricRadius);

        // Draw room parameters ONCE only, provided reflection engine is on
        if (RoomReflectionGizmoAS == this)
        {
            // Get global room parameters (write new C api to get reflection values)
            bool reflOn    = false;
            bool reverbOn  = false;
            float width    = 1.0f;
            float height   = 1.0f;
            float length   = 1.0f;

            ONSP_GetGlobalRoomReflectionValues(ref reflOn, ref reverbOn, ref width, ref height, ref length);

            // TO DO: Get the room reflection values and render those out as well (like we do in the VST)

            if((Camera.main != null) && (reflOn == true))
            {
                // Set color of cube (cyan is early reflections only, white is with reverb on)
                if(reverbOn == true)
                    c = Color.white;
                else
                    c = Color.cyan;

                Gizmos.color = c;
                Gizmos.DrawWireCube(Camera.main.transform.position, new Vector3(width, height, length));
                c.a = colorSolidAlpha;
                Gizmos.color = c;
                Gizmos.DrawCube(Camera.main.transform.position, new Vector3(width, height, length));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDestroy() 
    {
        // We will null out single pointer instance
        // of the room reflection gizmo since we are being destroyed.
        // Any ONSPAS that is alive or born will re-set this pointer
        // so that we only draw it once
        if(RoomReflectionGizmoAS == this)
        {
            RoomReflectionGizmoAS = null;
        }
    }
}
