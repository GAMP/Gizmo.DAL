﻿using GizmoDALV2.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GizmoDALV2.Mappings
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");
        }        
    }
}
