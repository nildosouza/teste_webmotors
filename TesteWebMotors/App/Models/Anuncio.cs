using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("tb_AnuncioWebmotors")]
    public class Anuncio
    {
        [Key, Required]        
        public int ID { get; set; }
                
        [Required, MaxLength(45, ErrorMessage = "Tamanho Máximo 45 dígitos!")]
        [DisplayName("Marca")]
        public string Marca { get; set; }

        [Required, MaxLength(45, ErrorMessage = "Tamanho máximo 45 dígitos!")]
        [DisplayName("Modelo")]
        public string Modelo { get; set; }
       
        [Required, MaxLength(45, ErrorMessage = "Tamanho máximo 45 dígitos!")]
        [DisplayName("Versão")]
        public string Versao { get; set; }

        [Required, MinLength(4, ErrorMessage = "Tamanho mínimo 4 dígitos!")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Valor Inválido!")]
        public int Ano { get; set; }
        
        [Required, DisplayName("Quilometragem")]
        [RegularExpression("[0-9]{1,7}", ErrorMessage = "Valor Inválido!")]
        public int Quilometragem { get; set; }
        
        [Required, DisplayName("Observação")]
        public string Observacao { get; set; }
    }
}