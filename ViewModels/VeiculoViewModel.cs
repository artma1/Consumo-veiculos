﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consumo_veiculos.ViewModels
{
    [Table("Veiculos")]
    public class VeiculoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o nome!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a placa!")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o ano de fabricação!")]
        [Display(Name = "Ano de Fabricação")]
        public int AnoFabricacao { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o ano do modelo!")]
        [Display(Name = "Ano do Modelo")]
        public int AnoModelo { get; set; }
       
        public ICollection<ConsumoViewModel> Consumos { get; set; }

    
    }
}
