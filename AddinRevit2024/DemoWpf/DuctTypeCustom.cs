using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinRevit2024.DemoWpf
{
    public class DuctTypeCustom
    {
        public DuctTypeCustom(ElementId id , string name)
        {
            Id= id; Name = name;
        }
        public ElementId Id { get; set; }

        public string Name { set; get; }
    }
}
