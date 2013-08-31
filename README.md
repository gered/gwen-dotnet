# gwen-dotnet without OS dependancies

This is a fork of the original [gwen-dotnet](https://code.google.com/p/gwen-dotnet/) project which has been slightly
modified to remove OS dependancies (specifically, usage of various Windows
APIs) to simplify using Gwen with Xamarin.Android and Xamarin.iOS. The core
library of GwenCS has been changed to build as a Portable Class Library.

The most significant change in this fork is the removal of all the specific
renderer implementations. This fork is *primarily* developed for my own use with 
[my own code](https://github.com/gered/Blarg.GameFramework) who's renderer probably wouldn't benefit anyone else. However,
it probably wouldn't be too much work to convert Gwen.Renderer.OpenTK to work
with this fork and Android/iOS.

Other significant changes include the removal of any platform-specific code
that was present in GwenCS. This includes things such as the file save/open
dialog support, cursor changing, and the System.Drawing dependancy. Very
light-weight equivalents to Color, Rectangle, etc. have been added to 
fill in the gaps.

## TODO

* Update to latest gwen-dotnet sources. This fork was originally started by me
almost a year ago and I never bothered to update the original sources ...
