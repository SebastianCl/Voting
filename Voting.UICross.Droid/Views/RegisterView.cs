﻿namespace Voting.UICross.Droid.Views
{
    using Common.ViewModels;
    using global::Android.App;
    using global::Android.OS;
    using MvvmCross.Platforms.Android.Views;

    [Activity(Label = "@string/app_name")]
    public class RegisterView : MvxActivity<RegisterViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.RegisterPage);
        }
    }

}