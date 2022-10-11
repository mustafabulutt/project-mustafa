using Business.Abstract;
using Business.Aspects.Secured;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Core.Aspects;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Contans;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal,IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService; 
        }

        
        [RemoveCacheAspect("IUserService.GetList")]
        //[SecuredAspect("Add,Admin")]
        public void Add(RegisterAuthDto user)
        {

            var addUser = CreateUser(user);
           
            _userDal.Add(addUser);
        }



        private User CreateUser(RegisterAuthDto registerDto )
        {
            byte[] PasswordHash, PasswordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out PasswordHash, out PasswordSalt);

            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            return user;

        }

      



        [ValidationAspect(typeof(UserValidator))] //Alanlarımızın is valid olup olmadıgını doğruluyor
        [TransactionAspect] //yapılan işlemi transactiona alıyor
        [RemoveCacheAspect("IUserService.GetList")] //cachede tutlan UserGetlistini siliyor
        [SecuredAspect("Update,Admin")] //yetki ve login kontrolünü yapıyor
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.Update);
        }




        [SecuredAspect("Delete,Admin")]
        [RemoveCacheAspect("IUserService.GetList")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.Delete);
        }



        [CacheAspect(60)]
        [SecuredAspect("GetList,Admin")]
        [PerformanceAspect()]
        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }



        [SecuredAspect("GetById,Admin")]
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(i=>i.Id == id));
        }


        [ValidationAspect(typeof(ChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(Messages.WrongOldPassword);
            }
           
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt=passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(Messages.SuccessPasswordChange);
            



        }


        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
           return _userDal.GetUserOperationClaims(userId);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(p => p.Email == email);
        }
    }
}
