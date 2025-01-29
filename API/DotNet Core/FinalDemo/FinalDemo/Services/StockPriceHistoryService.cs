using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Services
{
    public class StockPriceHistoryService : IStockPriceHistoryService
    {
        public Response Delete(YMH01 poco)
        {
            throw new NotImplementedException();
        }

        public YMH01 GetStockPriceHistoryById(int id)
        {
            throw new NotImplementedException();
        }

        public YMH01 PreDelete(DTOYMH01 dto)
        {
            throw new NotImplementedException();
        }

        public YMH01 PreSave(DTOYMH01 dto)
        {
            throw new NotImplementedException();
        }

        public Response Save(YMH01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnDelete(YMH01 poco)
        {
            throw new NotImplementedException();
        }

        public Response ValidateOnSave(YMH01 poco, OperationType type)
        {
            throw new NotImplementedException();
        }
    }
}
