using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPhotos.Entities
{
    public class LoginViewModel
    {    [DisplayName("Kullanıcı Adı"), MinLength(4, ErrorMessage = "{0} min. {1} karakter olmalıdır."), MaxLength(20), Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre"), MinLength(6, ErrorMessage = "{0} min. {1} karakter olmalıdır."), MaxLength(20), Required(ErrorMessage = "{0} alanı boş geçilemez."),DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}