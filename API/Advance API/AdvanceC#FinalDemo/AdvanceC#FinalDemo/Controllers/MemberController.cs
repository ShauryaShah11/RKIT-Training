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
using AdvanceC_FinalDemo.Models.POCO;
using System;
using AdvanceC_FinalDemo.Security;

namespace AdvanceC_FinalDemo.Controllers
{
    [RoutePrefix("api/members")] // Base route for the controller
    public class MemberController : ApiController
    {
        private readonly MemberRepository _memberRepository = new MemberRepository();
        private readonly LibraryFileService _fileService = new LibraryFileService();

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
        /// Retrieves a total member count.
        /// </summary>
        /// <returns>The Member Response containing member count</returns>
        [HttpGet]
        [Route("count")]
        public IHttpActionResult GetMemberCount()
        {
            Response res = _memberRepository.GetMemberCount();
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            _memberRepository.type = EnmOperationType.ADD;
            YMM01 poco = _memberRepository.PreSave(member);
            poco.M01F04 = PasswordHasher.HashedPassword(poco.M01F04);
            Response addResponse = _memberRepository.ValidateOnSave(poco);
            if (addResponse.IsError)
            {
                return BadRequest(addResponse.Message); // 400 Bad Request
            }
            Response save = _memberRepository.Save(poco);
            if (save.IsError)
            {
                return BadRequest(save.Message);
            }
            return CreatedAtRoute("GetMemberById", new { id = member.M01F01 }, save); // 201 Created
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            _memberRepository.type = EnmOperationType.UPDATE;
            YMM01 poco = _memberRepository.PreSave(member);
            Response updateResponse = _memberRepository.ValidateOnSave(poco);
            if (updateResponse.IsError)
            {
                return BadRequest(updateResponse.Message); // 400 Bad Request
            }
            Response update = _memberRepository.Save(poco);
            if (update.IsError)
            {
                return BadRequest(update.Message);
            }
            return Ok(update); // 200 OK
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
            _memberRepository.type = EnmOperationType.DELETE;
            YMM01 poco = _memberRepository.PreDelete(member);
            Response deleteResponse = _memberRepository.ValidateOnDelete(poco);
            if (deleteResponse.IsError)
            {
                return BadRequest(deleteResponse.Message); // 400 Bad Request
            }
            Response delete = _memberRepository.Delete(poco);
            if (delete.IsError)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete); // 200 OK
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

            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            string dataFolderPath = Path.Combine(baseDirectory, "data");

            // Use the correct base directory
            string filePath = Path.Combine(dataFolderPath, "members.json");

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
