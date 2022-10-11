using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contans
{
    public class Messages
    {
        public static string NullTokenError = "Lütfen giriş yapın.";
        public static string AuthorizationError = "İşlem için yetkiniz bulumuyor.";


        #region CrudOperationMessage
        public static string Update = "Kayıt başarıyla güncellendi";
        public static string Add ="Kayıt işlemi başarıyla gerçekleşti";
        public static string Delete ="Silme işlemi başarıyla gerçekleşti";
        public static string IsActiveFalse ="Kayıt pasif duruma getirildi";
        public static string isActiveTrue = "Kayıt aktif duruma getirildi";

        #endregion
        #region User
        public static string LoginSuccess = "Kullanıcı girişi başarılı";
        public static string LoginFailed = "Kullanıcı bilgileri hatalı";
        public static string EmailExists = "Bu mail adresi daha önce kullanılmış";
        public static string ImageSizeIsLessThanOneMb = "Yüklediğiniz resim 1 MB Dan düşük olmalıdır";
        public static string ImageExtensionsAllow = "Lütfen geçerli bir dosya türü seçin";
        public static string WrongOldPassword = "Geçersiz şifre.";
        public static string SuccessPasswordChange = "Şifre değiştirme işlemi başarılı.";
        #endregion

        #region OperationClaim
        public static string NameIsNullMessage = "Yetki adı boş olamaz";
        public static string IsNameAviable = "Bu yetki zaten mevcut"; 
        #endregion

        #region UserOperationClaim
        public static string UserIdIsNullMessage = "Yetki eklenecek kullanıcı bulnamadı";
        public static string ClaimIdIsNullMessage = "Eklenecek yetki bulanamadı";
        public static string IsClaimUserAvaible = "Kullanıcıda yetki mevcut";
        public static string IsOperationClaimExist = "Verilmek istenilen yetki bulunamadı";
        public static string IsUserExist = "Yetki verilmek istenilen kullanıcı bulunamadı";
        public static string IsUserExistUpdate = "Kayıt güncelleme işlemi başarısız";

        #endregion





    }
}
