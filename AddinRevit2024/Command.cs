using AddinRevit2024.DemoWpf;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Electrical;
using System.Windows.Forms;
using Autodesk.Revit.DB.Architecture;
using System.Runtime.Remoting.Contexts;
using AddinRevit2024.FamilyView;

namespace AddinRevit2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
           

            return Result.Succeeded;
        }
    }
}