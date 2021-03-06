﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

namespace StrangersToFriends.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
			global::Xamarin.FormsMaps.Init();
			KeyboardOverlapRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
