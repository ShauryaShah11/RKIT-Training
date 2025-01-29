using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Services
{
    public class OrderService : IOrderService
    {
        public Response Delete(YMO01 poco)
        {
            throw new NotImplementedException();
        }

        public Response GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public Response GetOrderbyId(int id)
        {
            throw new NotImplementedException();
        }

        public YMO01 PreDelete(DTOYMO01 dto)
        {
            return new YMO01
            {
                O01F01 = dto.O01101
            };
        }

        public YMO01 PreSave(DTOYMO01 dto)
        {
            return new YMO01
            {
                O01F01 = dto.O01101,
                O01F02 = dto.O01102,
                O01F03 = dto.O01103,
                O01F04 = dto.O01104
            };
        }

        public Response Save(YMO01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnDelete(YMO01 poco)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnSave(YMO01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }
    }
}
