namespace Prototipo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Personas
    {
        [StringLength(21)]
        public string Nombre { get; set; }

        [StringLength(41)]
        public string Apellido { get; set; }

        [Key]
        [StringLength(10)]
        public string Rut { get; set; }
    }
}
