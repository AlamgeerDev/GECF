using System;
using Acr.UserDialogs;
using GECF.Interfaces;
using GECF.Platforms.iOS.DependencyServices;
using UIKit;

//[assembly: Dependency(typeof(DialogServices))]
namespace GECF.Platforms.iOS.DependencyServices
{
    public class DialogServices : IDialogService
    {
        //UIActivityIndicatorView activityIndicatorView;

        /// <summary>
        /// Hides the loading.
        /// </summary>
        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        /// <summary>
        /// Shows the action sheet alert.
        /// </summary>
        public void ShowActionSheetAlert()
        {

        }

        /// <summary>
        /// Shows the action sheet alert.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="cancelstr">Cancelstr.</param>
        /// <param name="item1">Item1.</param>
        /// <param name="item2">Item2.</param>
        /// <param name="item1Clicked">Item1 clicked.</param>
        /// <param name="item2Clikced">Item2 clikced.</param>
        public void ShowActionSheetAlert(string title, string cancelstr, string item1, string item2, Action item1Clicked = null, Action item2Clikced = null)
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                                             .SetTitle(title)
                                             .SetCancel(cancelstr, () => { })
                                             .Add(item1, () =>
                                             {
                                                 item1Clicked();
                                             }).Add(item2, () =>
                                             {
                                                 item2Clikced();
                                             })
                                             );
        }

        /// <summary>
        /// Shows the alert async.
        /// </summary>
        /// <returns>The alert async.</returns>
        /// <param name="message">Message.</param>
        /// <param name="title">Title.</param>
        /// <param name="buttonText">Button text.</param>
        public Task ShowAlertAsync(string message, string title, string buttonText)
        {
            return Task.Run(() =>
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    new UIAlertView(title, message, null, buttonText).Show();
                }));
        }

        /// <summary>
        /// Shows the alert with two buttons.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="title">Title.</param>
        /// <param name="okButtonText">Ok button text.</param>
        /// <param name="cancelButtonText">Cancel button text.</param>
        /// <param name="okClicked">Ok clicked.</param>
        /// <param name="cancelClikced">Cancel clikced.</param>
        public void ShowAlertWithTwoButtons(string message, string title = null, string okButtonText = "OK", string cancelButtonText = "Cancel", Action okClicked = null, Action cancelClikced = null)
        {
            UIAlertView alert = new UIAlertView(title, message, null, cancelButtonText, null);
            if (okClicked != null)
            {
                alert.AddButton(okButtonText);
            }

            alert.Clicked += (object sender, UIButtonEventArgs e) =>
            {
                switch (e.ButtonIndex)
                {
                    case 0:
                        if (cancelClikced != null)
                        {
                            cancelClikced();
                        }
                        break;
                    case 1:
                        if (okClicked != null)
                        {
                            okClicked();
                        }
                        break;
                    default:
                        break;
                }
            };
            alert.Show();

        }

        /// <summary>
        /// Shows the error alert.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="title">Title.</param>
        /// <param name="buttonText">Button text.</param>
        public void ShowErrorAlert(string message, string title = "", string buttonText = "")
        {
            if (string.IsNullOrEmpty(title))
                title = "Error";// App.Translation.@default.errorTitle;
            if (string.IsNullOrEmpty(buttonText))
                buttonText = "Ok";

            UserDialogs.Instance.Alert(message, title, buttonText);
        }

        /// <summary>
        /// Shows the loading.
        /// </summary>
        /// <param name="loadingText">Loading text.</param>
        public void ShowLoading(string loadingText = null)
        {
            UserDialogs.Instance.ShowLoading();
        }
    }
}

