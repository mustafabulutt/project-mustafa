using Business.Abstract;
using Business.Aspects.Secured;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
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
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [SecuredAspect("Delete,Admin")]
        public IResult Delete(OperationClaim operationClaim)
        {
           _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.Delete);
        }
        [SecuredAspect("GetById,Admin")]
        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(t=>t.Id == id));
        }

        [SecuredAspect("GetList,Admin")]
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        [SecuredAspect("Update,Admin")]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameExistUpdate(operationClaim));
            if (result.Success)
            {
                _operationClaimDal.Update(operationClaim);
                return new SuccessResult(Messages.Update);
            }
            return new ErrorResult(result.Message);
           
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        [SecuredAspect("Add,Admin")]

        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameExist(operationClaim.Name));
            if (result.Success)
            {
                _operationClaimDal.Add(operationClaim);
                return new SuccessResult(Messages.Add);
            }
            return new ErrorResult(result.Message);

        }


        private IResult IsNameExist(string name)
        {

            var result = _operationClaimDal.Get(i=>i.Name == name);
            if (result != null)
            {
                return new ErrorResult(Messages.IsNameAviable);
            }
            return new SuccessResult();
        }
        private IResult IsNameExistUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = _operationClaimDal.Get(i=>i.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = _operationClaimDal.Get(i => i.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(Messages.IsUserExistUpdate);
                }
            }
            return new SuccessResult();



        }
    }
}
