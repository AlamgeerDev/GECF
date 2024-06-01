using System;
using Acr.UserDialogs;
using Android.App;
using GECF.Interfaces;
using GECF.Platforms.Android.DependencyServices;
using Plugin.CurrentActivity;
namespace GECF.Platforms.Android.DependencyServices
{
	public class DialogServices: IDialogService
    {
        protected global::Android.App.ProgressDialog progressDialog;

        protected global::Android.App.Activity CurrentActivity =>
        CrossCurrentActivity.Current.Activity;

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
        public Task ShowAlertAsync(string message,
            string title, string buttonText)
        {
            return Task.Run(() =>
            {
                UserDialogs.Instance.Alert(message, title, buttonText);
            });
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
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(CurrentActivity)
                       .SetMessage(message)
                       .SetTitle(title)
                       .SetCancelable(false)
                       .SetPositiveButton(okButtonText, delegate
                       {
                           if (okClicked != null)
                           {
                               okClicked();
                           }
                       })
                       .SetNegativeButton(cancelButtonText, delegate
                       {
                           if (cancelClikced != null)
                           {
                               cancelClikced();
                           }
                       });
            try
            {
                global::Android.App.Application.SynchronizationContext.Post(ignored =>
                {
                    if (CurrentActivity == null || CurrentActivity.IsFinishing)
                    {
                        return;
                    }
                    CurrentActivity.RunOnUiThread(() =>
                    {
                        alertDialogBuilder.Show();
                    });

                }, null);
            }

            catch (Exception ex)
            {
                Console.WriteLine(" Alert exception while showing" + ex.Message);
            }

        }

        /// <summary>
        /// Shows the loading.
        /// </summary>
        /// <param name="loadingText">Loading text.</param>
        public void ShowLoading(string loadingText = null)
        {
            UserDialogs.Instance.ShowLoading();

        }

        /// <summary>
        /// Alert the specified message, title and okButton.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="title">Title.</param>
        /// <param name="okButton">Ok button.</param>
        private void Alert(string message, string title, string okButton)
        {
            global::Android.App.Application.SynchronizationContext.Post(ignored =>
            {
                var builder = new AlertDialog.Builder(CurrentActivity);
                builder.SetIconAttribute
                    (global::Android.Resource.Attribute.AlertDialogIcon);
                builder.SetTitle(title);
                builder.SetMessage(message);
                builder.SetPositiveButton(okButton, delegate { });
                builder.Create().Show();
            }, null);
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
                title = "Error";
            if (string.IsNullOrEmpty(buttonText))
                buttonText = "Ok";

            UserDialogs.Instance.Alert(message, title, buttonText);
        }
    }
}

