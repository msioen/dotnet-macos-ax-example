using System.Runtime.InteropServices;
using CoreFoundation;
using ObjCRuntime;

namespace AXExample;

public partial class ViewController : NSViewController
{    
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    static extern IntPtr AXUIElementCreateApplication(int pid);
        
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    unsafe static extern int AXUIElementCopyAttributeValue(IntPtr element, IntPtr attribute, IntPtr* value);
    
    protected ViewController(NativeHandle handle) : base(handle)
    {
        // This constructor is required if the view controller is loaded from a xib or a storyboard.
        // Do not put any initialization here, use ViewDidLoad instead.
    }
    
    public override void ViewDidAppear()
    {
        base.ViewDidAppear();
        
        BtnExample.Activated += OnButtonActivated;
    }
    
    public override void ViewDidDisappear()
    {
        base.ViewDidDisappear();
        
        BtnExample.Activated -= OnButtonActivated;
    }

    private void OnButtonActivated(object? sender, EventArgs e)
    {
        try
        {
            string bundleIdentifier = "com.apple.Safari";
            NSRunningApplication app = NSRunningApplication.GetRunningApplications(bundleIdentifier)?.First();

            var application = AXUIElementCreateApplication(app.ProcessIdentifier);
        
            var ptrApplicationTitle = GetAXAttributeValue(application, "AXTitle");
            var applicationTitle = CFString.FromHandle(ptrApplicationTitle);
        
            var ptrFocusedWindow = GetAXAttributeValue(application,"AXFocusedWindow");
            var ptrFocusedWindowTitle = GetAXAttributeValue(ptrFocusedWindow, "AXTitle");
            var focusedWindowTitle = CFString.FromHandle(ptrFocusedWindowTitle);
        
            LblOutput.StringValue = $"Application: {applicationTitle}\nFocused Window: {focusedWindowTitle}";
        }
        catch (Exception exception)
        {
            LblOutput.StringValue = exception.Message;
        }
    }
    
    private unsafe IntPtr GetAXAttributeValue(IntPtr axUiElement, string attributeName)
    {
        IntPtr ptrAttributeValue;
        var attributeValueError = AXUIElementCopyAttributeValue(axUiElement, CFString.CreateNative(attributeName), &ptrAttributeValue);
        return ptrAttributeValue;
    }
}