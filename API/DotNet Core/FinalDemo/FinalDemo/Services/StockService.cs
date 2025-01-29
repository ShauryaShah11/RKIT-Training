using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Services
{
    public class StockService : IStockService
    {
        public Response Delete(YMS01 poco)
        {
            throw new NotImplementedException();
        }

        public Response GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public Response GetStockById(int id)
        {
            throw new NotImplementedException();
        }

        public Response GetStockByName(string name)
        {
            throw new NotImplementedException();
        }

        public YMS01 PreDelete(DTOYMS01 dto)
        {
            throw new NotImplementedException();
        }

        public YMS01 PreSave(DTOYMS01 dto)
        {
            throw new NotImplementedException();
        }

        public Response Save(YMS01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnDelete(YMS01 poco)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnSave(YMS01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }
    }
}
