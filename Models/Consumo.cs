using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consumo_veiculos.Models
{
    public class Consumo
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
        public TipoCombustivel Tipo { get; set; }
        [Display(Name = "Veículo")]
        [Required(ErrorMessage = "Informe o veículo")]
        public int VeiculoId { get; set; }
        [ForeignKey("VeiculoId")]
        public Veiculo Veiculo { get; set; }
    }
    public enum TipoCombustivel
    {
        Gasolina,
        Etanol,
        Eletricidade
    }
}

