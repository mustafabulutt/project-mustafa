using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class ChangePasswordValidator :AbstractValidator<UserChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Şifre 6 karakterden az Olamaz");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Şifreniz En Az Bir Adet Büyük Harf İçermelidir");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Şifreniz En Az Bir Adet Küçük Harf İçermelidir");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("Şifreniz En Az Bir Adet Sayı İçermelidir");
        }
    }
}
