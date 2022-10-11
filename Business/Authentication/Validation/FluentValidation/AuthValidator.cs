using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı Adı boş Olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Boş Olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli Bir Mail Adresi Girin");
            RuleFor(p => p.Image.FileName).NotEmpty().WithMessage("Kullanıcı Resmi Boş Olamaz");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre 6 karakterden az Olamaz");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifreniz En Az Bir Adet Büyük Harf İçermelidir");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz En Az Bir Adet Küçük Harf İçermelidir");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifreniz En Az Bir Adet Sayı İçermelidir");

        }




    }
}
