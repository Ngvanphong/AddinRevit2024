using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024.TerminalFamily
{
    public class TypeHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc= uiDoc.Document;
            var form = TerminalBinding.form;
            TerminalViewModel terminalViewModel= form.comboboxTerminalFamily.SelectedItem as TerminalViewModel;
            Family familyTerminal = doc.GetElement(terminalViewModel.Id) as Family;
            var ids = familyTerminal.GetFamilySymbolIds();
            List<TypeViewModel> listTypeViewModel = new List<TypeViewModel>();
            foreach(ElementId id in ids)
            {
                FamilySymbol faSy = doc.GetElement(id) as FamilySymbol;
                TypeViewModel typeViewModel = new TypeViewModel(faSy.Id, faSy.Name);
                listTypeViewModel.Add(typeViewModel);
            }
            form.listViewType.ItemsSource = listTypeViewModel;


        }

        public string GetName()
        {
            return "TypeHandler1";
        }
    }
}
