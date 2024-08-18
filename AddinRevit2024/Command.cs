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

namespace AddinRevit2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            List<Line> listLine= new List<Line>();
            while (true) 
            {
                try
                {
                    Reference pickObject = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element
                        , "Pick line");
                    DetailLine detailLine = doc.GetElement(pickObject) as DetailLine;
                    if (detailLine != null) 
                    {
                        Curve curve = detailLine.GeometryCurve;
                        listLine.Add(curve as Line);
                    }
                }
                catch 
                { 
                    break; 
                }
            }

            var pipeSystem = new FilteredElementCollector(doc).OfClass(typeof(PipingSystemType)).First();
            var pipeType = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_PipeCurves)
                .WhereElementIsElementType().FirstOrDefault(x=>x.Name=="Default");

            //Pipe previousPipe = null;
            List<Pipe> listNewPipe = new List<Pipe>();
            for (int i = 0; i < listLine.Count; i++)
            { 
                using(Transaction t= new Transaction(doc, "CreatePipe"))
                {
                    t.Start();
                    Line line = listLine[i];
                    XYZ start = line.GetEndPoint(0);
                    XYZ end = line.GetEndPoint(1);
                    var newPipe = Autodesk.Revit.DB.Plumbing.Pipe.Create(doc, pipeSystem.Id, pipeType.Id,
                        doc.ActiveView.GenLevel.Id, start, end);
                    listNewPipe.Add(newPipe);
                    //else
                    //{
                    //    Connector endPreviousConnect = previousPipe.ConnectorManager.Lookup(1);
                    //    XYZ end = line.GetEndPoint(1);
                    //    previousPipe = Autodesk.Revit.DB.Plumbing.Pipe.Create(doc, pipeType.Id,
                    //        doc.ActiveView.GenLevel.Id, endPreviousConnect, end);

                    //}
                    t.Commit();
                }
                
            }
            using(Transaction t= new Transaction(doc, "Connect"))
            {
                t.Start();
                for(int i = 0; i < listNewPipe.Count-1; i++)
                {
                    Pipe pipePre = listNewPipe[i];
                    Pipe pipeNex = listNewPipe[i + 1];
                    Connector startConnect = pipePre.ConnectorManager.Lookup(1);
                    Connector endConnect = pipeNex.ConnectorManager.Lookup(0);
                    var eblow = doc.Create.NewElbowFitting(startConnect, endConnect);
                }
                t.Commit();
            }

            


           
            Dimension dim = null;
            XYZ pointClick = uiDoc.Selection.PickPoint("Pick a point");
            XYZ previousVector = null;
            int indexBreak = 0;
            for (int i = 0; i < dim.Segments.Size; i++)
            {
                indexBreak = i;
                DimensionSegment segment = dim.Segments.get_Item(i);
                XYZ origin = segment.Origin;
                XYZ currentVector = pointClick.Subtract(new XYZ(origin.X, origin.Y, pointClick.Z)).Normalize();
                if (previousVector != null)
                {
                    if (!currentVector.IsAlmostEqualTo(previousVector))
                    {
                        break;
                    }
                }
                previousVector = currentVector;
            }

            ReferenceArray listRef1 = new ReferenceArray();
            ReferenceArray listRef2 = new ReferenceArray;
            for (int i = 0; i < dim.References.Size; i++)
            {
                if (i < indexBreak)
                {
                    listRef1.Append(dim.References.get_Item(i));
                }
                else
                {
                    listRef2.Append(dim.References.get_Item(i));
                }
            }

            using(Transaction t= new Transaction(doc, "CreateDim"))
            {
                t.Start();
                doc.Create.NewDimension(doc.ActiveView, dim.Curve as Line, listRef1);
                doc.Create.NewDimension(doc.ActiveView, dim.Curve as Line, listRef2);
                t.Commit();
            }
            
            
            return Result.Succeeded;
        }
    }
}