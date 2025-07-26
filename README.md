# GMTK 2025

## Software Installation

### C#
We are using .NET SDK version: `9.0.303`. You can visit https://dotnet.microsoft.com/en-us/download/dotnet/9.0 to install it.

Once installed, in the terminal use this command to verify that the correct SDK was installed:
```
dotnet --list-sdks
```
### VS Code
Installation Link: https://code.visualstudio.com/download

Extensions To Install: 
- https://marketplace.visualstudio.com/items?itemName=VisualStudioToolsForUnity.vstuc
- https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp
- https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit

### Unity
Install the latest version of Unity Hub: https://unity.com/download

In Unity Hub, click Installs > Install Editor, then install Unity Version 6.1 (`6000.1.10f1`). 

## Clone the Repo
```
git clone https://github.com/faizahsayyid/gmtk-2025.git
```
## Open Project in Unity
In Unity Hub go to Projects > Add > Add project from disk then select this repository. From there you should be able to open the project in the Unity Editor. 

If all goes well, you should be able to hit play, go to the console, and see a log for "Hello World!". 

## Workflow Note

For whatever reason I've found that the Unity intellisense/autocomplete only works if you open the C# file from the Unity project :')

