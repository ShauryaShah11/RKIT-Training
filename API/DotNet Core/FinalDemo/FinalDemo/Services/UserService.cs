using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Services
{
    public class UserService : IUserService
    {
        public Response Delete(YMU01 poco)
        {
            throw new NotImplementedException();
        }

        public Response GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Response GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public YMU01 PreDelete(DTOYMU01 dto)
        {
            throw new NotImplementedException();
        }

        public YMU01 PreSave(DTOYMU01 dto)
        {
            throw new NotImplementedException();
        }

        public Response Save(YMU01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnDelete(YMU01 poco)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnSave(YMU01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }
    }
}
