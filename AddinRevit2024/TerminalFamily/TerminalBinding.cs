using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024.TerminalFamily
{
    [Transaction(TransactionMode.Manual)]
    public class TerminalBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            List<Family> listFamily= new FilteredElementCollector(doc).OfClass(typeof(Family)).
                Cast<Family>().Where(x=>x.FamilyCategory.Id.Value == (long)BuiltInCategory.OST_DuctTerminal).ToList();
            List<TerminalViewModel> listTerminal = new List<TerminalViewModel>();
            foreach(Family family in listFamily)
            {
                TerminalViewModel terminal = new TerminalViewModel(family.Id, family.Name);
                listTerminal.Add(terminal);
            }

            TerminalWpf form= new TerminalWpf();
            form.comboboxTerminalFamily.ItemsSource= listTerminal;
            form.ShowDialog();


            return Result.Succeeded;
        }
    }
}
