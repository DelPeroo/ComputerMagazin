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
    
    public partial class order
    {
        public int id_order { get; set; }
        public Nullable<int> id_pc_features_personal { get; set; }
        public string login { get; set; }
        public int amount { get; set; }
        public Nullable<System.DateTime> data_order { get; set; }
        public Nullable<System.DateTime> data_delivery { get; set; }
        public int total_price { get; set; }
    
        public virtual client client { get; set; }
        public virtual pc_features_personal pc_features_personal { get; set; }
    }
}
