using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.MovieNoteRepository.Validation.FluentValidation
{
    public class MovieNoteValidator :AbstractValidator<MovieNote>
    {

        public MovieNoteValidator()
        {
            RuleFor(p => p.MovieId).NotEmpty().WithMessage("Movie ID null olmamalıdır.");
            RuleFor(p => p.Note).NotEmpty().WithMessage("Not alanı null olamaz.");



        }
    }
}
