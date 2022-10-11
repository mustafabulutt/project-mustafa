using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Contans;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(LoginAuthDto loginAuthDto)
        {

            var user = _userService.GetByEmail(loginAuthDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<Token>("Kullanıcı girişi başarısız");

            }
            var result = HashingHelper.VerifyPasswordHash(loginAuthDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id);
            if (result)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token,Messages.LoginSuccess);
            }
            return new ErrorDataResult<Token>("Kullanıcı girişi başarısız");

        }

        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto authDto)
        {
                      

                IResult result = BusinessRules.Run(
                CheckIFEmailExists(authDto.Email)
               
                );

            if (!result.Success)
            {
                return result;
            }
       


            _userService.Add(authDto);
            return new SuccessResult(Messages.Add);

        }


        private IResult CheckIFEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult(Messages.EmailExists);
            }
            return new SuccessResult();

        }

        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);

            if (imgMbSize > 1)
            {
                return new ErrorResult(Messages.ImageSizeIsLessThanOneMb);
            }

            return new SuccessResult();


        }

        private IResult CheckIfImageExtensionsAllow(string fileName) {


            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg ", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult(Messages.ImageExtensionsAllow);
            }
            return new SuccessResult();

        }

    }
}
