using Business.Abstract;
using Business.Aspects.Secured;
using Business.Repositories.UserOperationClaimRepository.Validator.FluentValidator;
using Core.Aspects.Validation;
using Core.Contans;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IUserDal _userDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimDal operationClaimDal,IUserDal userDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimDal = operationClaimDal;
            _userDal = userDal;
        }

        [SecuredAspect("Add,Admin")]
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var result = BusinessRules.Run(
                IsClaimUserExist(userOperationClaim.UserId , userOperationClaim.OperationClaimId),
                IsOperationClaimExist(userOperationClaim.OperationClaimId), 
                IsUserExist(userOperationClaim.UserId));
            if (result.Success)
            {
                _userOperationClaimDal.Add(userOperationClaim);
                return new SuccessResult(Messages.Add);

            }
            return new ErrorResult(result.Message);
           
        }

        [SecuredAspect("Delete,Admin")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.Delete);
        }

        [SecuredAspect("GetById,Admin")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(i=>i.Id == id));
        }

        [SecuredAspect("GetList,Admin")]
        public IDataResult<List<UserOperationClaim>> GetList()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        [SecuredAspect("Update,Admin")]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.Update);
        }



        private IResult IsClaimUserExist(int userId,int claimId)
        {

            var result = _userOperationClaimDal.Get(i => i.OperationClaimId == claimId && i.UserId == userId);
            if (result != null)
            {
                return new ErrorResult(Messages.IsClaimUserAvaible);
            }
            return new SuccessResult();
        }

        private IResult IsOperationClaimExist(int claimId)
        {

            var result = _operationClaimDal.Get(i => i.Id == claimId );
            if (result == null)
            {
                return new ErrorResult(Messages.IsOperationClaimExist);
            }
            return new SuccessResult();
        }


        private IResult IsUserExist(int userId)
        {

            var result = _userDal.Get(i => i.Id == userId);
            if (result == null)
            {
                return new ErrorResult(Messages.IsUserExist);
            }
            return new SuccessResult();
        }

    }
}
