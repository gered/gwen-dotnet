# gwen-dotnet without OS dependancies

This is a fork of the original gwen-dotnet project which has been slightly
modified to remove OS dependancies (specifically, usage of varios Windows
APIs) to simplify using Gwen with Xamarin.Android and Xamarin.iOS.

Ultimately, this means that things such as the file save/open dialog support
are not implemented and those methods instead simply do nothing. As well,
the cursor support (changing the cursor to, for example, a resize icon when
the mouse is positioned at the edge of a resizable control) has also been
stubbed out and will not work.

As well, the dependancy on System.Drawing has been removed with equivalent
structs to Color, Rectangle, etc being re-implemented with very light-weight
equivalents.