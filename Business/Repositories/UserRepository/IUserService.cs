using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto user);

        IResult Update(User user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
        IResult Delete(User user);

        IDataResult<List<User>> GetList();

        IDataResult<User> GetById(int id);


        public User GetByEmail(string email);
        List<OperationClaim> GetUserOperationClaims(int userId);




    }
}
