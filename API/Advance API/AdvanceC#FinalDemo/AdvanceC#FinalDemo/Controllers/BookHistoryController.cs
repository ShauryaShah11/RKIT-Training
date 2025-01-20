using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Repositories;
using System.Web.Http;

namespace AdvanceC_FinalDemo.Controllers
{
    [RoutePrefix("api/history")] // Base route for the controller
    public class BookHistoryController : ApiController
    {
        private readonly BookHistoryRepository _bookHistoryRepository = new BookHistoryRepository();

        /// <summary>
        /// Get all book history
        /// </summary>
        /// <returns>List of all book histories</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllHistory()
        {
            var history = _bookHistoryRepository.GetAllBookHistory();
            return Ok(history); // 200 OK
        }

        /// <summary>
        /// Get book history by bookId and memberId
        /// </summary>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="memberId">The ID of the member</param>
        /// <returns>Book history details</returns>
        [HttpGet]
        [Route("{bookId:int}/{memberId:int}")]
        public IHttpActionResult GetHistoryById(int bookId, int memberId)
        {
            Response res = _bookHistoryRepository.GetBookHistoryById(bookId, memberId);
            if (res.IsError)
            {
                return NotFound(); 
            }
            return Ok(res); // 200 OK
        }

        [HttpGet]
        [Route("not-return")]
        public IHttpActionResult GetUnReturnedBook()
        {
            Response res = _bookHistoryRepository.GetUnreturnedBookHistory();
            if (res.IsError)
            {
                return NotFound();
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Add new book history
        /// </summary>
        /// <param name="dto">DTO containing book history data</param>
        /// <returns>Message confirming the addition of book history</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBookHistory([FromBody] DTOYMH01 dto)
        {
            if(dto == null)
            {
                return BadRequest("Invalid Data.");
            }
            Response res = _bookHistoryRepository.AddBookHistoryRecord(dto);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res.Message); // 200 OK
        }

        /// <summary>
        /// Update book history based on bookId and memberId
        /// </summary>
        /// <param name="dto">DTO containing updated book history data</param>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="memberId">The ID of the member</param>
        /// <returns>Message confirming the update of book history</returns>
        [HttpPut]
        [Route("{bookId:int}/{memberId:int}")]
        public IHttpActionResult UpdateBookHistory([FromBody] DTOYMH01 dto, int bookId, int memberId)
        {
            Response res = _bookHistoryRepository.UpdateBookHistoryRecord(dto, bookId, memberId);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res.Message); // 200 OK
        }

        /// <summary>
        /// Delete book history based on bookId and memberId
        /// </summary>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="memberId">The ID of the member</param>
        /// <returns>Message confirming the deletion of book history</returns>
        [HttpDelete]
        [Route("{bookId:int}/{memberId:int}")]
        public IHttpActionResult DeleteBookHistory(int bookId, int memberId)
        {
            Response res = _bookHistoryRepository.DeleteBookHistoryRecord(bookId, memberId);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res.Message); // 200 OK
        }
    }
}
