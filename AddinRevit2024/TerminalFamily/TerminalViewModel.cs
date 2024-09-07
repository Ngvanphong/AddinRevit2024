using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024.TerminalFamily
{
    public class TerminalViewModel
    {
        public TerminalViewModel(ElementId id, string name)
        {
            (Id,Name)=(id,name);
        }
        public ElementId Id { get; set; }
        public string Name { get; set; }    
    }
}
