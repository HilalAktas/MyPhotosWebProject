using MyPhotos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhotos.Language;

namespace MyPhotos.BusinessLayer
{
  public  class ExcepClass

    {
        public static ExceptionCode code;

        public void LoginExcep(LoginViewModel login)
            
        {
            code = ExceptionCode.Login;

            if (login.KullaniciAdi==null||login.KullaniciAdi.Length<5 || login.KullaniciAdi.Length > 15)
            {
              throw new Exception(Resource.AdSınırlama);
  
            }
           
            if(login.Sifre==null||login.Sifre.Length<5 || login.Sifre.Length > 20) {

                throw new Exception(Resource.ŞifreSınırlama);
            }

      

        }


        public void RegisterExcep(RegisterView register)
        {
            code = ExceptionCode.Register;

            if (register.Ad==null||register.Ad.Length < 5 || register.Ad.Length > 15)
            {
                throw new Exception(Resource.AdSınırlama);
            }

            if(register.SoyAd==null|| register.SoyAd.Length<5 || register.SoyAd.Length > 15)
            {
                throw new Exception(Resource.SoyadıSınırlama);
            }

            if (register.Sifre==null && register.Sifre.Length<5 || register.Sifre.Length>15)
            {
                throw new Exception(Resource.ŞifreSınırlama);
            }

            if (register.TekrarSifre == null||register.Sifre!=register.TekrarSifre)
            {
                throw new Exception(Resource.ŞifreKontrol);
            }

            if (register.Email == null|| register.Email.Length < 5 || register.Email.Length > 35) 
            {
                throw new Exception(Resource.EmailSınırlama);
 
            }
            if(!register.Email.Contains("@"))
            {
                throw new Exception(Resource.EmailFormat);
            }

            //string data = register.DogumTarihi.GetType().ToString();

            //if (!(register.DogumTarihi.GetType().ToString()==TypeCode.DateTime.ToString()))
            //{
            //    throw new Exception("Doğum tarihi alanı boş geçilemez.");
            //}

            if (register.DogumTarihi == null)
            {
                {
                    throw new Exception(Resource.DoğumTarihiKontrol);
                }
            }

        }

        public void EditProfileExcep(MyPhotosUser editprofile)
        {
            code = ExceptionCode.EditProfile;

            if(editprofile.Name==null|| editprofile.Name.Length<5 || editprofile.Name.Length > 25)
            {
                throw new Exception(Resource.AdSınırlama);
            }
            if (editprofile.Surname == null || editprofile.Surname.Length < 5 || editprofile.Surname.Length > 15)
            {
                throw new Exception(Resource.SoyadıSınırlama);
            }

            if (editprofile.Password == null && editprofile.Password.Length < 5 || editprofile.Password.Length > 25)
            {
                throw new Exception(Resource.ŞifreSınırlama);
            }
            if (editprofile.Email == null || editprofile.Email.Length < 5 || editprofile.Email.Length > 35)
            {
                throw new Exception(Resource.EmailSınırlama);

            }
            if (editprofile.UserName == null || editprofile.UserName.Length < 5 || editprofile.UserName.Length > 15)
            {
                throw new Exception(Resource.KullanıcıAdıSınırlama);
            }

            if (!editprofile.Email.Contains("@"))
            {
                throw new Exception(Resource.EmailFormat);
            }
        }
    }
}
