using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ninja_manager.Helper
{
    static class WindowExtension
    {
        //
        // Summary:
        //     Returns true if user confirmed to close window.
        public static bool ClosePrompt(this Window window)
        {
            if (window.IsLoaded)
            {
                string sMessageBoxText = "Warning: All changes will be lost, are you sure you want to continue?";
                string sCaption = "Closing prompt";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                if (rsltMessageBox.Equals(MessageBoxResult.Yes))
                {
                    window.Close();
                    return true;
                }

                return false;
            }
            return true;
        }
    }
}
