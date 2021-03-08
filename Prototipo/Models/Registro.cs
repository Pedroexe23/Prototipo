namespace Prototipo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Registro")]
    public partial class Registro
    {
        [Key]
        public int id_registro { get; set; }

        [StringLength(10)]
        public string Fk_RUT { get; set; }

        public int? Fk_Id_Documento { get; set; }
    }
}
