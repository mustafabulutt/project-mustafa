using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator :AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı Adı boş Olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Boş Olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli Bir Mail Adresi Girin");
           
        }
    }
}
