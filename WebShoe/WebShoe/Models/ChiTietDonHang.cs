//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebShoe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonHang
    {
        public int MaCTDH { get; set; }
        public int DonHang_ID { get; set; }
        public int SanPham_ID { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ChietKhau { get; set; }
        public double TongTien { get; set; }
    
        public virtual DonHang DonHang { get; set; }
    }
}
