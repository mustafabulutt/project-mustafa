using Core.Contans;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.OperationClaimRepository.Validation.FluentValidation
{
    public class MovieScoreValidator : AbstractValidator<MovieScore>
    {

        public MovieScoreValidator()
        {
            RuleFor(p => p.MovieId).NotEmpty().WithMessage("Movie ID NULL olamaz");
            RuleFor(p => p.Score).GreaterThan(0).WithMessage("Score 0'dan büyük olmalıdır")
.LessThan(11).WithMessage("Puan 11 den küçük olmalıdır");



        }
    }
}
