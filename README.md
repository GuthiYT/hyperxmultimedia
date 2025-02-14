## Table of contents
* [General info](#general-info)
* [Warning](#warning)
* [Supported devices](#supported-devices)
* [How to get your device to be supported](#how-to-get-your-device-to-be-supported)
* [Setup](#setup)
* [Technologies](#technologies)
## General info
Remaps any button on (theoretically) any wireless headset ([Supported devices](#supported-devices)) to do 3 different actions that you can programm yourself, with the default being pause, skip, raplay

## Warning
The original functionality is handled locally on the headset itself so it still gets triggered.
Because of that I would advise you to only use this tool if you can live without the origianal functionality. For example, I don't use my microphone on my headset, so I can tolerate not having the original mute function.

## Supported devices
[How to get your device to be supported](#how-to-get-your-device-to-be-supported) (Windows only)
* HyperX Cloud II Wireless (DTS Version)
* HyperX Cloud III Wireless
* Corsair Virtuoso XT

## How to get your device to be supported
* Execute Debug.exe ([Releases](https://github.com/TizianGuth/Wireless-Button-Reprogrammer/releases/tag/Debug)) and follow the instruction.
* Open an issue here on Github and send me the readings

## Setup
### Download
You can donload the .exe here on github under "Releases"
## How to find you Vendor and Product ID:
### The newest and easiest way now is to download [`debug.zip`](https://github.com/Tiziam/Wireless-Button-Reprogrammer/releases/tag/Debug) under releases and follow the instructions until *step 3.*
\
Alternatively (old way):

To find your 2 IDs you basically have 2 ways of doing so: 1. Trial and error using your device manager (not advised) or 2. using a third party tool like [Busdog](https://github.com/djpnewton/busdog). Here you will only find documentation how to find your IDs via busdog. 

1. Install and open [Busdog](https://github.com/djpnewton/busdog). Here you will have to check "Automaticly trace new Devices" on the bottom left hand corner. \
![BusdogTraceNew](https://github.com/GuthiYT/hyperxrebutton/blob/main/doc/img/busdog_trace_new.png)

2. Make sure every box is unchecked then un- and replug your headset's USB dongle. Now one Device should be checked with its multiple "sub-devices". Now hover over one of them, look for the 4 letterrs after "VID_" and "PID_" and write them down somewhere. \
![BusdogDevice](https://github.com/GuthiYT/hyperxrebutton/blob/main/doc/img/busdog_device.png)

4. Now just input your VID and PID in the program, click "Apply" and "Start".

### How to change keycodes
To change what happens after each click, refer to this [Site](https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes) to get your desired
keycodes, then enter them in the program, click "Apply" and "Start".


## Technologies
* Visual Studio's WPF
* .NET 8.0
* [AudioManager](https://gist.github.com/sverrirs/d099b34b7f72bb4fb386)
* [HIDLibrary](https://github.com/mikeobrien/HidLibrary)
	
