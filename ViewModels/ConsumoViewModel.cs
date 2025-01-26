using Consumo_veiculos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consumo_veiculos.ViewModels
{
    public class ConsumoViewModel
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Informe a descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe a data")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Informe o valor")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Informe a quilometragem")]
        public int Km { get; set; }
        [Display(Name = "Tipo de Combustível")]
        public TipoCombustivelViewData Tipo { get; set; }
        [Display(Name = "Veículo")]
        [Required(ErrorMessage = "Informe o veículo")]
        [ForeignKey("VeiculoId")]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        public ConsumoViewModel() { }
    }

    public enum TipoCombustivelViewData
    {
        Gasolina,
        Etanol,
        Eletricidade
    }
}

