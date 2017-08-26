using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyPhotos.Entities
{
  public class MyPhotosUser:EntityBase
    {   [MaxLength(50) ,Required(ErrorMessage = "İsim Alanı boş geçilemez.")]
        public string Name { get; set; }
        [MaxLength(50), Required(ErrorMessage = "Soyisim Alanı boş geçilemez.")]
        public string Surname { get; set; }
        [MaxLength(50), Required(ErrorMessage = "İsim Alanı boş geçilemez.")]
        public string UserName { get; set; }
        [DataType(DataType.DateTime),Required]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Email Alanı boş geçilemez.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsActive  { get; set; }
        [ScaffoldColumn(true)]
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }

        public virtual List<Photo> Photos { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }
}
