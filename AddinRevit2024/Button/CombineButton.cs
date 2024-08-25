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
    internal class CombineButton
    {
        public void Combine(UIControlledApplication application)
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

            PushButtonData pushButtonData = new PushButtonData("CreateColumn1224444", "Create Column",
                 Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData.Image = imageResource;
            pushButtonData.LargeImage = imageResource;
            pushButtonData.ToolTip = "Show family and type";
            pushButtonData.LongDescription = "show family";

            ImageSource imageResource2 = Extension.GetImageSource(Resources.icons8_crop_16);
            PushButtonData pushButtonData2 = new PushButtonData("CreateColumn444444444", "Create Beam",
                Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData2.Image = imageResource2;
            pushButtonData2.LargeImage = imageResource2;
            pushButtonData2.ToolTip = "Show family and type";
            pushButtonData2.LongDescription = "show family";

            SplitButtonData split1 = new SplitButtonData("Split1Floor", "Split1");

            ImageSource imageResource3 = Extension.GetImageSource(Resources.icons8_crop_16__2_);
            PushButtonData pushButtonData3 = new PushButtonData("CreateColumn44563344444", "Create Floor",
                Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData3.Image = imageResource3;
            pushButtonData3.LargeImage = imageResource3;
            pushButtonData3.ToolTip = "Show family and type";
            pushButtonData3.LongDescription = "show family";

            ImageSource imageResource4 = Extension.GetImageSource(Resources.icons8_crop_16__2_);
            PushButtonData pushButtonData4 = new PushButtonData("CreateColumn44563344444555", "Create Floor",
                Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData4.Image = imageResource4;
            pushButtonData4.LargeImage = imageResource4;
            pushButtonData4.ToolTip = "Show family and type";
            pushButtonData4.LongDescription = "show family";

            IList<RibbonItem> listRibbonItems= ribbonPanel.AddStackedItems(split1, pushButtonData3,pushButtonData4);
            foreach(RibbonItem item in listRibbonItems)
            {
                if(item.Name == "Split1Floor")
                {
                    SplitButton splitButtonItem= item as SplitButton;
                    splitButtonItem.AddPushButton(pushButtonData);
                    splitButtonItem.AddPushButton(pushButtonData2);
                    break;
                }
            }



        }
    }
}
