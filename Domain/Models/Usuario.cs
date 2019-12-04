using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("Usuarios")]
    public class Usuario
    {

        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "E-mail:")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [Display(Name = "Confirmação da senha:")]
        [NotMapped]
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        public string ConfirmacaoSenha { get; set; }
        public Endereco Endereco { get; set; }

        public bool Admin { get; set; }

        public Usuario()
        {
            Endereco endereco = new Endereco();
        }
    }
}
