namespace Prototipo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Documento")]
    public partial class Documento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Documento { get; set; }

        [StringLength(60)]
        public string Archivo { get; set; }

        public long? Tamaño { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }
    }
}
