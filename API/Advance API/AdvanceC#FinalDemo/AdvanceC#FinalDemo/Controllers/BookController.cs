using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Repositories;
using System.Web.Http;
using System.Linq.Expressions;
using AdvanceC_FinalDemo.Services;
using System.IO;
using System.Net.Http;
using System.Net;

namespace AdvanceC_FinalDemo.Controllers
{
    /// <summary>
    /// Controller to manage Book-related operations such as retrieving, adding, updating, and deleting books.
    /// </summary>
    [RoutePrefix("api/books")] // Base route for the controller
    public class BookController : ApiController
    {
        private readonly static string _baseDirectory = @"F:\Shaurya Training\RKIT-Training\API\Advance API\AdvanceC#FinalDemo\AdvanceC#FinalDemo\data";
        private readonly BookRepository _bookRepository = new BookRepository();
        private readonly LibraryFileService _fileService = new LibraryFileService(_baseDirectory);

        /// <summary>
        /// Retrieves all books from the repository.
        /// </summary>
        /// <returns>A list of all books or a bad request if an error occurs.</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBooks()
        {
            Response res = _bookRepository.GetAllBooks();
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Retrieves books by a specific category.
        /// </summary>
        /// <param name="category">The category to filter books by.</param>
        /// <returns>A list of books matching the category or a bad request if an error occurs.</returns>
        [HttpGet]
        [Route("category/{category}")]
        public IHttpActionResult GetBooksByCategory(string category)
        {
            Response res = _bookRepository.GetBookByCategory(category);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Retrieves a book by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the book.</param>
        /// <returns>The book details or a 404 Not Found if the book doesn't exist.</returns>
        [HttpGet]
        [Route("{id:int}", Name = "GetBookById")]
        public IHttpActionResult GetBookById(int id)
        {
            Response res = _bookRepository.GetBookById(id);
            if (res.IsError)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        /// <param name="book">The book data to add.</param>
        /// <returns>A 201 Created response with the book details or a bad request if an error occurs.</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBook([FromBody] DTOYMB01 book)
        {
            if (book == null)
            {
                return BadRequest("Invalid book data."); // 400 Bad Request
            }

            Response res = _bookRepository.HandleOperation(book, OperationType.ADD);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return CreatedAtRoute("GetBookById", new { id = book.B01101 }, res); // 201 Created
        }

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        /// <param name="book">The book data to update.</param>
        /// <returns>A success response or a bad request if an error occurs.</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateBook([FromBody] DTOYMB01 book)
        {
            if (book == null)
            {
                return BadRequest("Invalid book data."); // 400 Bad Request
            }

            Response res = _bookRepository.HandleOperation(book, OperationType.UPDATE);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Deletes a book from the repository.
        /// </summary>
        /// <param name="book">The book data to delete.</param>
        /// <returns>A success response with the result message or a bad request if an error occurs.</returns>
        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteBook([FromBody] DTOYMB01 book)
        {
            if (book == null)
            {
                return BadRequest("Invalid book data."); // 400 Bad Request
            }

            Response res = _bookRepository.HandleOperation(book, OperationType.DELETE);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res.Message); // 200 OK
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage ExportBook()
        {
            Response bookResponse = _bookRepository.GetAllBooks();
            if (bookResponse.IsError)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, bookResponse.Message);
            }

            // Serialize data and save to file
            Response serializeResponse = _fileService.SerializeDataTable(bookResponse.Data, "books.json");
            if (serializeResponse.IsError)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, serializeResponse.Message);
            }

            // Construct the correct file path
            string filePath = Path.Combine(_baseDirectory, "books.json");

            if (!File.Exists(filePath))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "File not found.");
            }

            // Read file bytes
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Create response message
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };

            // Set content type and headers
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "books.json"
            };

            return response;
        }

    }
}
