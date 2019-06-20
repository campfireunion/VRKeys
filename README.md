# VRKeys - A drum-style keyboard for VR in Unity

[![VRKeys Screenshot](https://github.com/campfireunion/VRKeys/blob/master/Assets/VRKeys/Textures/vrkeys-screenshot.png?raw=true)](https://www.youtube.com/watch?v=Q_kUlVTyRag)
(click image for video)

VRKeys is an open source keyboard interface for single-line text input in VR, made in Unity and [available for free in the Unity Asset Store](https://assetstore.unity.com/packages/tools/input-management/vrkeys-99222).

### Features

* Updated for Unity 2019.x
* Works with both Oculus Touch and HTC Vive
* Drum-style keyboard input
* Grab to adjust position and scale
* Shift key for capitalization and special characters
* Special @ and .com keys
* Easy input validation, info, and confirmation messages
* Ability to enable/disable input while validating or submitting
* Alternate keyboard layouts for i18n (Dvorak & Azerty for French included)
* Demo scene shows how to integrate into your own projects.

### Manual installation

In addition to the version on the [Unity Asset Store](https://assetstore.unity.com/packages/tools/input-management/vrkeys-99222),
you can manually install VRKeys into an existing VR project (assumes VR support is enabled in your project) via:

1. Download [VRKeys.unitypackage](https://github.com/campfireunion/VRKeys/blob/master/VRKeys.unitypackage)
2. Import TextMesh Pro via Window > TextMeshPro > Import TMP Essential Resources
3. Import into your project via Assets > Import Package > Custom Package
4. Open `Assets/VRKeys/Scenes/Example Scene.unity` to see a working example scene
5. Example usage can be found in [DemoScene.cs](https://github.com/campfireunion/VRKeys/blob/master/Assets/VRKeys/Scripts/Example/DemoScene.cs)

### Performance

When integrating VRKeys into your own scenes, assign a camera to the Event Camera on the VRKeys > Canvas object.
This prevents Unity from defaulting to calling `Camera.main` multiple times per frame, which in turn calls
`Object.FindObjectWithTag` on each access. For more info, [see point 3 in these Unity UI optimization tips](https://create.unity3d.com/Unity-UI-optimization-tips).

> Brought to you by [Flipside XR](https://www.flipsidexr.com/).
