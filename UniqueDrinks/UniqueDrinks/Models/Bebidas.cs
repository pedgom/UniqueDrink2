using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueDrinks.Models
{
    public class Bebidas
    {
        public Bebidas()
        {
            // inicializar a lista de Fotografias de cada uma das Bebidas
            
            // inicializar a lista de Clientes das Reservas de Bebidas
            Reserva = new HashSet<Reservas>();
        }

        /// <summary>
        /// Identificador de cada Bebida
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da bebida
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Descrição breve da bebida
        /// </summary>
        [Required]
        public string Descricao { get; set; }

        /// <summary>
        /// Preço da bebida
        /// </summary>
        [Required]
        public float Preco { get; set; }

        /// <summary>
        /// Imagem referente à bebida
        /// </summary>
        [Required]
        public string Imagem { get; set; }


        /// <summary>
        /// Verificar se a bebida se encontra em stock
        /// </summary>
        [Required]
        public string Stock { get; set; }

        /// <summary>
        /// Indica a categoria da bebida
        /// </summary>
        [Required]
        public string Categoria { get; set; }
        

        /// <summary>
        /// Bebidas que o cliente deseja reservar
        /// </summary>
        public virtual ICollection<Reservas> Reserva { get; set; }

       
        




    }
}
