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
    
    public partial class GearStat
    {
        public int Id_Gear { get; set; }
        public string Stat_Name { get; set; }
        public int Amount { get; set; }
    
        public virtual Gear Gear { get; set; }
        public virtual Stat Stat { get; set; }
    }
}
