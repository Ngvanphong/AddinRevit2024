using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Windows.Media;
using AddinRevit2024.Properties;
using System.Reflection;

namespace AddinRevit2024.Button
{
    internal class FamilyTypeButton
    {
        public void FamilyType(UIControlledApplication application)
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
            if(ribbonPanel == null)
            {
                ribbonPanel = application.CreateRibbonPanel(Constants.ribbonTabName, Constants.ribbonPanelName);
            }

            ImageSource imageResource = Extension.GetImageSource(Resources.icons8_crop_24__1_);

            PushButtonData pushButtonData = new PushButtonData("FamilyTypeShow", "Family\nType",
                 Assembly.GetExecutingAssembly().Location, "AddinRevit2024.FamilyView.FamilyTypeBinding");
            pushButtonData.Image = imageResource;
            pushButtonData.LargeImage = imageResource;
            pushButtonData.ToolTip = "Show family and type";
            pushButtonData.LongDescription = "show family";

            PushButton pushButton= ribbonPanel.AddItem(pushButtonData) as PushButton;
            pushButton.Enabled = true;

            Autodesk.Windows.RibbonItem buttonWindow = GetRibbonItem(pushButton);
            Autodesk.Windows.RibbonToolTip ribbonButtonTooltip = new Autodesk.Windows.RibbonToolTip();
            ribbonButtonTooltip.Title = "Family";
            ribbonButtonTooltip.Shortcut = "T";
            ribbonButtonTooltip.ExpandedVideo = new Uri(@"C:/Users/ngvan/Downloads/sample_640x360.swf");

            buttonWindow.ToolTip = ribbonButtonTooltip;


        }

        private Autodesk.Windows.RibbonItem GetRibbonItem( RibbonItem item)
        {
            Autodesk.Windows.RibbonControl ribbonControl= UIFramework.RevitRibbonControl.RibbonControl;

            foreach (var tab in ribbonControl.Tabs)
            {
                if (tab.Name != Constants.ribbonTabName) continue;
                foreach (var panel in tab.Panels)
                {
                    foreach (var ribbonItem in panel.Source.Items)
                    {
                        if (ribbonItem.AutomationName == item.ItemText)
                        {
                            return ribbonItem as Autodesk.Windows.RibbonItem;
                        }
                    }
                }
            }
            return null;
        }
    }
}
