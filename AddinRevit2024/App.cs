using AddinRevit2024.Button;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            FamilyTypeButton buttonFamily = new FamilyTypeButton();
            buttonFamily.FamilyType(a);

            ListSmallButton smallButton= new ListSmallButton();
            smallButton.SmallButton(a);

            SplitButtonCustom splitButtonCustom = new SplitButtonCustom();
            splitButtonCustom.Split(a);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
