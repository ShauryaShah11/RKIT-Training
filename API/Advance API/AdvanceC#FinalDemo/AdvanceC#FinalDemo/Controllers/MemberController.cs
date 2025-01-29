using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Repositories;
using AdvanceC_FinalDemo.Services;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace AdvanceC_FinalDemo.Controllers
{
    [RoutePrefix("api/members")] // Base route for the controller
    public class MemberController : ApiController
    {
        private readonly static string _baseDirectory = @"F:\Shaurya Training\RKIT-Training\API\Advance API\AdvanceC#FinalDemo\AdvanceC#FinalDemo\data";
        private readonly MemberRepository _memberRepository = new MemberRepository();
        private readonly LibraryFileService _fileService = new LibraryFileService(_baseDirectory);

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns>List of all members</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllMember()
        {
            Response res = _memberRepository.GetAllMember();
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Get member by ID
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member details if found</returns>
        [HttpGet]
        [Route("{id:int}", Name = "GetMemberById")]
        public IHttpActionResult GetMemberById(int id)
        {
            Response res = _memberRepository.GetMemberById(id);
            if (res.IsError)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Add a new member
        /// </summary>
        /// <param name="member">Member data</param>
        /// <returns>Created member</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMember([FromBody] DTOYMM01 member)
        {
            if (member == null)
            {
                return BadRequest("Invalid Member Data."); // 400 Bad Request
            }
            Response res = _memberRepository.HandleOperation(member, OperationType.ADD);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return CreatedAtRoute("GetMemberById", new { id = member.M01101 }, res); // 201 Created
        }

        /// <summary>
        /// Update member details
        /// </summary>
        /// <param name="member">Updated member data</param>
        /// <returns>Updated member data</returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMember([FromBody] DTOYMM01 member)
        {
            if (member == null)
            {
                return BadRequest("Invalid Member Data."); // 400 Bad Request
            }
            Response res = _memberRepository.HandleOperation(member, OperationType.UPDATE);
            if (res.IsError)
            {
                return BadRequest(res.Message); // 400 Bad Request
            }
            return Ok(res); // 200 OK
        }

        /// <summary>
        /// Delete a member
        /// </summary>
        /// <param name="member">Member data</param>
        /// <returns>Message confirming deletion</returns>
        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteMember([FromBody] DTOYMM01 member)
        {
            if (member == null)
            {
                return BadRequest("Invalid Member Data."); // 400 Bad Request
            }
            Response res = _memberRepository.HandleOperation(member, OperationType.DELETE);
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
            Response bookResponse = _memberRepository.GetAllMember();
            if (bookResponse.IsError)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, bookResponse.Message);
            }
            Response serializeResponse = _fileService.SerializeDataTable(bookResponse.Data, "members.json");
            if (serializeResponse.IsError)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, serializeResponse.Message);
            }

            // Use the correct base directory
            string filePath = Path.Combine(_baseDirectory, "members.json");

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
                FileName = "members.json"
            };

            return response;
        }

    }
}
