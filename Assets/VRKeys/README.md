# VRKeys - A VR keyboard for use with NewtonVR

![VRKeys Screenshot](https://github.com/campfireunion/VRKeys/blob/master/Assets/VRKeys/Textures/vrkeys-screenshot.png?raw=true)

VRKeys is a simple keyboard interface for single-line text input that integrates with [NewtonVR](https://github.com/TomorrowTodayLabs/NewtonVR).

### Features

* Works with both Oculus Touch and HTC Vive
* Drum-style keyboard input
* Shift key for capitalization and special characters
* Special @ and .com keys
* Easy input validation, info, and confirmation messages
* Ability to enable/disable input while validating or submitting
* Alternate keyboard layouts for i18n (Dvorak & Azerty for French included)
* Demo scene shows how to integrate into your own projects

### Requirements

* [SteamVR](https://www.assetstore.unity3d.com/en/#!/content/32647) and/or [Oculus Utilities for Unity](https://developer.oculus.com/downloads/package/oculus-utilities-for-unity-5/)
* [NewtonVR](https://github.com/TomorrowTodayLabs/NewtonVR)
* [TextMesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126)
* [Oculus Audio SDK](https://developer.oculus.com/downloads/package/oculus-audio-sdk-plugins/)

> To replace the Oculus Audio SDK with an alternate spatializer, just remove `ONSP Audio Source` from the Sphere object under `LeftMallet` and `RightMallet`.

### Installation

1. Install the prerequisites above
2. Download [VRKeys.unitypackage](https://github.com/campfireunion/VRKeys/blob/master/VRKeys.unitypackage)
3. Import into your project via Assets > Import Package > Custom Package
4. Open `Assets/VRKeys/Scenes/VRKeys Example.unity` to see a working example scene

Brought to you by [The Campfire Union](https://www.campfireunion.com/).
