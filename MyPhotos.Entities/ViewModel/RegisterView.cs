using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPhotos.Entities
{
    public class RegisterView
    {
        [DisplayName("Ad"), MinLength(4, ErrorMessage = "{0} min. {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı boş geçilemez."),
               StringLength(30, ErrorMessage = "Kullanıcı Adı max. 30 karakter olmalı.")]
        public string Ad { get; set; }

        [DisplayName("Soyad"),  Required(ErrorMessage = "{0} alanı boş geçilemez."), 
                     StringLength(30, ErrorMessage = "Şifre max. 20 karakter olmalı.")]
        public string SoyAd { get; set; }
        [DisplayName("Kullanıcı Adı"), MinLength(4,ErrorMessage ="{0} min. {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı boş geçilemez."),
               StringLength(30, ErrorMessage = "Kullanıcı Adı max. 30 karakter olmalı.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Email"),DataType(DataType.EmailAddress) Required(ErrorMessage = "{0} alanı boş geçilemez."), MinLength(6, ErrorMessage = "{0} min. {1} karakter olmalıdır."),
                     StringLength(30, ErrorMessage = "Şifre max. 20 karakter olmalı.")]
       
        public string Email { get; set; }
        [DisplayName("Şifre"), MinLength(6, ErrorMessage = "{0} min. {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(30, ErrorMessage = "Şifre max. 20 karakter olmalı.")]
        public string Sifre { get; set; }
        [DisplayName("Şifre Tekrarı"), MinLength(6, ErrorMessage = "{0} min. {1} karakter olmalıdır."),  Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(30, ErrorMessage = "Şifre max. 20 karakter olmalı."), Compare("Sifre", ErrorMessage = "Girmiş olduğunuz şifreler eşleşmiyor.")]
        public string TekrarSifre { get; set; }
    
        public DateTime DogumTarihi { get; set; }

    }
}