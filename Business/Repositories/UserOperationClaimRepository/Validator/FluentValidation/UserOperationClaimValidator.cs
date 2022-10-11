using Core.Contans;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Validator.FluentValidator
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(i => i.UserId).Must(IsIdValid).WithMessage(Messages.UserIdIsNullMessage);
            RuleFor(i => i.OperationClaimId).Must(IsIdValid).WithMessage(Messages.ClaimIdIsNullMessage);
        }



        private bool IsIdValid(int id)
        {
            if (id > 0 && id != null)
            {
                return true;
            }
            return false;

        }
    }



}
