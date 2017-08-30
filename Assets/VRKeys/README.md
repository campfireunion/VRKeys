# VRKeys - A drum-style keyboard for VR in Unity

[![VRKeys Screenshot](https://github.com/campfireunion/VRKeys/blob/master/Assets/VRKeys/Textures/vrkeys-screenshot.png?raw=true)](https://www.youtube.com/watch?v=Q_kUlVTyRag)
(click image for video)

VRKeys is an open source keyboard interface for single-line text input in VR, made in Unity.

### Features

* Works with both Oculus Touch and HTC Vive
* Drum-style keyboard input
* Shift key for capitalization and special characters
* Special @ and .com keys
* Easy input validation, info, and confirmation messages
* Ability to enable/disable input while validating or submitting
* Alternate keyboard layouts for i18n (Dvorak & Azerty for French included)
* Demo scene shows how to integrate into your own projects.

### Try it out

There are demo builds for both HTC Vive and Oculus Rift available on the [releases page](https://github.com/campfireunion/VRKeys/releases).

### Requirements

* [SteamVR](https://www.assetstore.unity3d.com/en/#!/content/32647) and/or [Oculus Utilities for Unity](https://developer.oculus.com/downloads/package/oculus-utilities-for-unity-5/)
* [TextMesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126)
* [Oculus Audio SDK](https://developer.oculus.com/downloads/package/oculus-audio-sdk-plugins/)
* Demo scene uses [NewtonVR](https://github.com/TomorrowTodayLabs/NewtonVR), but works without or with others like [VRTK](https://github.com/thestonefox/VRTK).

> To replace the Oculus Audio SDK with an alternate spatializer, just remove `ONSP Audio Source` from the Sphere object under `LeftMallet` and `RightMallet`.

### Installation

1. Install the prerequisites above
2. Download [VRKeys.unitypackage](https://github.com/campfireunion/VRKeys/blob/master/VRKeys.unitypackage)
3. Import into your project via Assets > Import Package > Custom Package
4. Open `Assets/VRKeys/Scenes/VRKeys Example.unity` to see a working example scene

Brought to you by [The Campfire Union](https://www.campfireunion.com/).
