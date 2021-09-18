using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueDrinks.Models
{
    public class Reservas
    {
        [Key]
        public int ReservaID { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [ForeignKey(nameof(ListaReserva))]
        public int LRIdFK { get; set; }

        public virtual ListaReservas ListaReserva { get; set; }

        [ForeignKey(nameof(Bebida))]
        public int BebidaFK { get; set; }

        public virtual Bebidas Bebida { get; set; }

        
    }
}
