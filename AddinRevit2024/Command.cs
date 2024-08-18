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
        public static List<FamilyAndType> ListFamilyAndType = new List<FamilyAndType>();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            List<Family> listFamily= new FilteredElementCollector(doc).OfClass(typeof(Family))
                .Cast<Family>().Where(x=>x.FamilyCategory.Id.Value == (long)BuiltInCategory.OST_LightingFixtures)
                .ToList();

            List<FamilyModel> listFamilyView = new List<FamilyModel>();
            List<FamilyAndType> listFamilyAndType= new List<FamilyAndType>();
            foreach (Family family in listFamily) 
            {
                FamilyModel faModel = new FamilyModel(family.Id, family.Name);
                listFamilyView.Add(faModel);

                var idOfTypes = family.GetFamilySymbolIds();

                FamilyAndType familyAndType = new FamilyAndType();
                familyAndType.Family = faModel;
                familyAndType.Types = new List<FamilyTypeModel>();
                foreach (ElementId id in idOfTypes) 
                {
                    FamilySymbol symbolType = doc.GetElement(id) as FamilySymbol;
                    FamilyTypeModel typeModel= new FamilyTypeModel(symbolType.Id, symbolType.Name);
                    familyAndType.Types.Add(typeModel);
                }
                listFamilyAndType.Add(familyAndType);
            }
            ListFamilyAndType = listFamilyAndType;

            FamilyViewWpf form = new FamilyViewWpf();
            form.comboboxFamily.ItemsSource = listFamilyView;

            form.ShowDialog();

            return Result.Succeeded;
        }
    }
}