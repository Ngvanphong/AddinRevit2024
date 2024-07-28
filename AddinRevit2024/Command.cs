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


namespace AddinRevit2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();
            List<Line> lines = new List<Line>();    
            foreach(ElementId id in selectedIds)
            {
                DetailCurve detailCurve = doc.GetElement(id) as DetailCurve;
                if (detailCurve != null)
                {
                    Curve curve = detailCurve.GeometryCurve as Curve;
                    Line lineNew = curve as Line;
                    if (lineNew != null)
                    {
                        lines.Add(lineNew);
                    }
                }
            }

           
            ComboboxWpf comboboxWpf= new ComboboxWpf();

            var allSystem = new FilteredElementCollector(doc).OfClass(typeof(MechanicalSystemType))
               .Cast<MechanicalSystemType>();
            comboboxWpf.comboboxSystemType.ItemsSource = allSystem;

            List<DuctType> ductTypes= new FilteredElementCollector(doc).OfClass(typeof(DuctType))
                .Cast<DuctType>().ToList();

            List<DuctTypeCustom> listDuctTypeCus = new List<DuctTypeCustom>();
            foreach (DuctType ductType in ductTypes) 
            {
                ElementId id = ductType.Id;
                string name= $"{ductType.FamilyName}: {ductType.Name}";
                DuctTypeCustom ductTypeCus = new DuctTypeCustom(id, name);
                listDuctTypeCus.Add(ductTypeCus);
            }

            listDuctTypeCus= listDuctTypeCus.OrderBy(x=>x.Name).ToList();

            comboboxWpf.comboboxDuctType.ItemsSource = listDuctTypeCus;

            comboboxWpf.listVidewDuctType.ItemsSource = listDuctTypeCus;


            

            comboboxWpf.ShowDialog();

            MechanicalSystemType systemTypeChoose= comboboxWpf.comboboxSystemType.SelectedItem as MechanicalSystemType;
            DuctTypeCustom ductCustomTypeChose= comboboxWpf.comboboxDuctType.SelectedItem as DuctTypeCustom;

            List<DuctTypeCustom> listItemSeleted = comboboxWpf.listVidewDuctType.SelectedItems.Cast<DuctTypeCustom>().ToList();

            Level level = doc.ActiveView.GenLevel;

            foreach (Line line in lines) 
            {
                using(Transaction t= new Transaction(doc, "CreateDuct"))
                {
                    t.Start();
                    XYZ start = line.GetEndPoint(0);
                    XYZ end= line.GetEndPoint(1);
                    Autodesk.Revit.DB.Mechanical.Duct.Create(doc, systemTypeChoose.Id, ductCustomTypeChose.Id, level.Id, start, end);
                    t.Commit();
                }
            }





          

             return Result.Succeeded;
        }
    }
}
