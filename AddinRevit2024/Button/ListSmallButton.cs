using AddinRevit2024.Properties;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AddinRevit2024.Button
{
    internal class ListSmallButton
    {
        public void SmallButton(UIControlledApplication application)
        {
            try
            {
                application.CreateRibbonTab(Constants.ribbonTabName);
            }
            catch { }
            RibbonPanel ribbonPanel = null;
            foreach (RibbonPanel panel in application.GetRibbonPanels(Constants.ribbonTabName))
            {
                if (panel.Name == Constants.ribbonPanelName)
                {
                    ribbonPanel = panel;
                    break;
                }
            }
            if (ribbonPanel == null)
            {
                ribbonPanel = application.CreateRibbonPanel(Constants.ribbonTabName, Constants.ribbonPanelName);
            }

            ImageSource imageResource = Extension.GetImageSource(Resources.icons8_crop_16__1_);

            PushButtonData pushButtonData = new PushButtonData("CreateColumn122", "Terminal Family",
                 Assembly.GetExecutingAssembly().Location, "AddinRevit2024.TerminalFamily.TerminalBinding");
            pushButtonData.Image = imageResource;
            pushButtonData.LargeImage = imageResource;
            pushButtonData.ToolTip = "Show family and type";
            pushButtonData.LongDescription = "show family";

            ImageSource imageResource2 = Extension.GetImageSource(Resources.icons8_crop_16);
            PushButtonData pushButtonData2 = new PushButtonData("CreateColumn44444", "Create Beam",
                Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData2.Image = imageResource2;
            pushButtonData2.LargeImage = imageResource2;
            pushButtonData2.ToolTip = "Show family and type";
            pushButtonData2.LongDescription = "show family";

            ImageSource imageResource3 = Extension.GetImageSource(Resources.icons8_crop_16__2_);
            PushButtonData pushButtonData3 = new PushButtonData("CreateColumn445633", "Create Floor",
                Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData3.Image = imageResource3;
            pushButtonData3.LargeImage = imageResource3;
            pushButtonData3.ToolTip = "Show family and type";
            pushButtonData3.LongDescription = "show family";

            ribbonPanel.AddStackedItems(pushButtonData,pushButtonData2,pushButtonData3);


        }
    }
}
