using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyPhotos.Entities
{
    public enum ErrorMessageCode
    {
        KullanıcıAdıKullanılıyor=100,
        EmailAdresiKullamılıyor=101,
        KullanıcıAktifDegil=201,
        KullanıcıAdıveSifreUyuşmuyor=301,
        AktiflestirilmisKullanici=303,
        AktiflestirmeIdBulunamadı=222,
        KullanıcıBulunamadı=223,
        KullanıcıGuncellenemedi=220,
        KullanıcıSilinemedi=224,
        KullanıcıAdminDegil=440,
        KullanıcıEklenemedi=900


        
    }
}
