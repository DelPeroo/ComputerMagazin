//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComputerMagazin
{
    using System;
    using System.Collections.Generic;
    
    public partial class pc_features_default
    {
        public int id_pc_features_default { get; set; }
        public int id_Body { get; set; }
        public Nullable<int> id_CPU { get; set; }
        public Nullable<int> id_RAM { get; set; }
        public Nullable<int> id_GPU { get; set; }
        public Nullable<int> id_SSD { get; set; }
        public Nullable<int> id_HDD { get; set; }
        public Nullable<int> id_type_OC { get; set; }
        public int id_category { get; set; }
        public int price { get; set; }
    
        public virtual pc_Body pc_Body { get; set; }
        public virtual pc_category pc_category { get; set; }
        public virtual pc_CPU pc_CPU { get; set; }
        public virtual pc_GPU pc_GPU { get; set; }
        public virtual pc_HDD pc_HDD { get; set; }
        public virtual pc_RAM pc_RAM { get; set; }
        public virtual pc_SSD pc_SSD { get; set; }
        public virtual type_OC type_OC { get; set; }
    }
}
