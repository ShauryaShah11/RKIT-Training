using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using AdvanceC_FinalDemo.Repositories;
using AdvanceC_FinalDemo.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdvanceC_FinalDemo.Controllers
{
    /// <summary>
    /// Controller to manage Book-related operations such as retrieving, adding, updating, and deleting books.
    /// </summary>
    [RoutePrefix("api/books")] // Base route for the controller
    public class BookController : ApiController
    {
        private readonly BookRepository _bookRepository = new BookRepository();
        private readonly LibraryFileService _fileService = new LibraryFileService();

        /// <summary>
        /// Retrieves all books from the repository.
        /// </summary>
        /// <returns>A list of all books or a bad request if an error occurs.</returns>
        [HttpGet]
        [Route("all")]
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
        /// Retrieves a total book count.
        /// </summary>
        /// <returns>The Book Response containing book count</returns>
        [HttpGet]
        [Route("count")]
        public IHttpActionResult GetBookCount()
        {
            Response res = _bookRepository.GetBookCount();
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            _bookRepository.type = EnmOperationType.ADD;
            YMB01 poco = _bookRepository.PreSave(book);
            Response addResponse = _bookRepository.ValidateOnSave(poco);
            if (addResponse.IsError)
            {
                return BadRequest(addResponse.Message); // 400 Bad Request
            }
            Response save = _bookRepository.Save(poco);
            if (save.IsError)
            {
                return BadRequest(save.Message);
            }
            return CreatedAtRoute("GetBookById", new { id = book.B01F01 }, save); // 201 Created
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            _bookRepository.type = EnmOperationType.UPDATE;
            YMB01 poco = _bookRepository.PreSave(book);
            Response updateResponse = _bookRepository.ValidateOnSave(poco);
            if (updateResponse.IsError)
            {
                return BadRequest(updateResponse.Message); // 400 Bad Request
            }
            Response update = _bookRepository.Save(poco);
            if (update.IsError)
            {
                return BadRequest(update.Message);
            }
            return Ok(update); // 200 OK
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            _bookRepository.type = EnmOperationType.DELETE;
            YMB01 poco = _bookRepository.PreDelete(book);
            Response deleteResponse = _bookRepository.ValidateOnDelete(poco);
            if (deleteResponse.IsError)
            {
                return BadRequest(deleteResponse.Message); // 400 Bad Request
            }
            Response delete = _bookRepository.Delete(poco);
            if (delete.IsError)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message); // 200 OK
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

            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            string dataFolderPath = Path.Combine(baseDirectory, "data");

            // Use the correct base directory
            string filePath = Path.Combine(dataFolderPath, "books.json");

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
