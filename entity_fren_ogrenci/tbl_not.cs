//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace entity_fren_ogrenci
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_not
    {
        public int not_id { get; set; }
        public Nullable<int> not_ogrenci { get; set; }
        public Nullable<int> not_ders { get; set; }
        public Nullable<int> not_sınav1 { get; set; }
        public Nullable<int> not_sınav2 { get; set; }
        public Nullable<int> not_sınav3 { get; set; }
        public Nullable<decimal> not_ortalama { get; set; }
        public Nullable<bool> durum { get; set; }
    
        public virtual tbl_dersler tbl_dersler { get; set; }
        public virtual tbl_ogrenciler tbl_ogrenciler { get; set; }
    }
}
