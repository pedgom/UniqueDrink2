using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueDrinks.Models
{
    public class ListaReservas
    {
        public ListaReservas()
        {
            Reserva = new HashSet<Reservas>();
        }

        [Key]
        public int LRId { get; set; }

        [Required]
        public DateTime Reservado { get; set; }

        [Required]
        public bool CheckOut { get; set; }


        [ForeignKey(nameof(Cliente))]
        public int ClienteFK { get; set; }
        public virtual Clientes Cliente { get; set; }

        public virtual ICollection<Reservas> Reserva { get; set; }

        
    }
}
