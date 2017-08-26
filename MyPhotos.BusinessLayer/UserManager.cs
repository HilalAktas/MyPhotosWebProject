using MyEvernote.Common.Helpers;

using MyPhotos.Common.Helper;
using MyPhotos.DataAccessLayer.EntityFramework;
using MyPhotos.Entities;
using MyPhotos.Language;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace MyPhotos.BusinessLayer
{
    public class UserManager:ManagerBase<MyPhotosUser> 

    {
        
        BusinessLayerResult<MyPhotosUser> check = new BusinessLayerResult<MyPhotosUser>();
        public BusinessLayerResult<MyPhotosUser> RegisterUser(RegisterView data)
        {
            MyPhotosUser u =Find(x => x.UserName == data.KullaniciAdi || x.Email == data.Email);
            if (u != null)
            {
              

                if (u.UserName == data.KullaniciAdi)
                {
                   
                    check.AddError(Resource.KullanıcıAdıKullanılıyor);
                }

                if (u.Email == data.Email)
                {
                    check.AddError(Resource.EpostaKullanılıyor);
                }
            }

            else
            {
                int result = Insert(new MyPhotosUser()
                {
                    Name = data.Ad,
                    Surname = data.SoyAd,
                    UserName = data.KullaniciAdi,
                    Password = data.Sifre,
                    Email = data.Email,
                    IsActive = false,
                    ActivateGuid = Guid.NewGuid(),
                    IsAdmin = false,
                    DateofBirth = data.DogumTarihi,

                    ModifiedOn = DateTime.Now,
                    CreatedOn = DateTime.Now


                });

                if (result > 0)
                {
                    check.Result = Find(x => x.Email == data.Email);
                    string siteurl = ConfigHelper.Get<string>("SiteRootUrl");
                    string ActivateUri = $"{siteurl}/Home/Activate/{  check.Result.ActivateGuid}";
                    string body = ($"Lütfen bu linkten hesabınızı aktif ediniz.<a href='{ActivateUri}' target='_blank'>Bu Linke Tıklayınız</a>");

                    MailHelper.SendMail(body, check.Result.Email, "MyPhotos Hesap Aktifleştirme Maili", true);

                }
            }

            return check;

        }

        public BusinessLayerResult<MyPhotosUser> LoginUser(LoginViewModel data)
        {
            check.Result =Find(x => x.UserName == data.KullaniciAdi && x.Password == data.Sifre);
            if (check.Result != null)
            {
                if (!check.Result.IsActive)
                {
                    check.AddError(Resource.KullanıcıAktifDeğil);
                }

            }

            else
            {
                check.AddError(Resource.KullanıcıadıŞifreUyumsuz);

            }

            return check;

        }

        public BusinessLayerResult<MyPhotosUser> ActivateUser(Guid Activate)
        {
            check.Result = Find(x => x.ActivateGuid == Activate);

            if (check.Result != null)
            {
                if (check.Result.IsActive)
                {
                    check.AddError(Resource.KullanıcıZatenAktifleştirilmiş_);

                    return check;
                }

                check.Result.IsActive = true;

                Update(check.Result);
            }

            else
            {
                check.AddError(Resource.Böyle_bir_aktifleştirme_kodu_mevcut_değil_);

            }

            return check;
        }

        public BusinessLayerResult<MyPhotosUser> GetById(int ıd)
        {
            BusinessLayerResult<MyPhotosUser> profileuser = new BusinessLayerResult<MyPhotosUser>();

            profileuser.Result = Find(x => x.Id == ıd);

            if (profileuser.Result == null)
            {
                profileuser.AddError(Resource.KullanıcıBulunamıyor);

            }

            return profileuser;

        }

        public BusinessLayerResult<MyPhotosUser> UpdateProfile(MyPhotosUser data)
        {
            MyPhotosUser db_user = Find(x => x.Id==data.Id);

            if (db_user.IsAdmin) data.IsAdmin = true;
            BusinessLayerResult<MyPhotosUser> res = new BusinessLayerResult<MyPhotosUser>();
        

            if (db_user.UserName==data.UserName)
            {
                if (db_user.Email != data.Email)
                {
                    MyPhotosUser user = Find(x => x.Email == data.Email);

                    if (user!=null)
                    {
                        res.AddError(Resource.EpostaKullanılıyor);

                        return res;
                    }
                }
            }

            else
            {

                MyPhotosUser user = Find(x => x.UserName == data.UserName);

                if (user!=null)
                {
                    res.AddError(Resource.KullanıcıAdıKullanılıyor);

                    return res;
                }


            }

            res.Result = Find( x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsActive = true;
            res.Result.IsAdmin = data.IsAdmin;

         int deger=Update(res.Result);

            if (deger==0)
            {
                res.AddError(Resource.KullanıcıGüncellenemedi_);
            }

            return res;
        }

        public BusinessLayerResult<MyPhotosUser>  RemoveUser(int id)
        {
            BusinessLayerResult<MyPhotosUser> res = new BusinessLayerResult<MyPhotosUser>();
            MyPhotosUser user =Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(Resource.KullanıcıSilinemedi);
                    return res;
                }
            }
            else
            {
                res.AddError(Resource.KullanıcıBulunamıyor);
            }

            return res;
        }

    }
}
