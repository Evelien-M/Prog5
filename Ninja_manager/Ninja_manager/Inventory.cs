//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ninja_manager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public int Id_Ninja { get; set; }
        public string Category { get; set; }
        public Nullable<int> Id_Gear { get; set; }
    
        public virtual Category Category1 { get; set; }
        public virtual Gear Gear { get; set; }
        public virtual Ninja Ninja { get; set; }
    }
}
