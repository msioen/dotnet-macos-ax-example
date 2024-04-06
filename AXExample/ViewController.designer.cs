// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AXExample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton BtnExample { get; set; }

		[Outlet]
		AppKit.NSTextField LblOutput { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BtnExample != null) {
				BtnExample.Dispose ();
				BtnExample = null;
			}

			if (LblOutput != null) {
				LblOutput.Dispose ();
				LblOutput = null;
			}

		}
	}
}
