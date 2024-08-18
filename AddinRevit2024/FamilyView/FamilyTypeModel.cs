using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024.FamilyView
{
    public class FamilyTypeModel
    {
        public FamilyTypeModel(ElementId id , string name)
        {
            Id= id; Name= name; 
        }
        public ElementId Id { set; get; }

        public string Name { set; get; }    
    }
}
