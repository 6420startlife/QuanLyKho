﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace QuanLyKho.Models
{
    public partial class Kho
    {
        public Kho()
        {
            list_phieuNhap = new HashSet<PhieuNhap>();
        }

        public string maKho { get; set; }
        public string tenKho { get; set; }

        public virtual ICollection<PhieuNhap> list_phieuNhap { get; set; }
    }
}