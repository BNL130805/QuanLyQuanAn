//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyQuanAn.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillInf
    {
        public int idBillInf { get; set; }
        public int idFood { get; set; }
        public int count { get; set; }
        public int idBill { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual food food { get; set; }
    }
}
