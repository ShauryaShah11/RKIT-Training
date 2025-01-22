using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Repositories;
using AdvanceC_FinalDemo.Services;
using System.Web.Http;

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

        [HttpPost]
        [Route("export")]
        public IHttpActionResult ExportBook()
        {
            Response bookResponse = _memberRepository.GetAllMember();
            if (bookResponse.IsError)
            {
                return BadRequest(bookResponse.Message);
            }
            Response serializeResponse = _fileService.SerializeDataTable(bookResponse.Data, "members.json");
            if (serializeResponse.IsError)
            {
                return BadRequest(serializeResponse.Message);
            }
            return Ok(serializeResponse.Message);
        }
    }
}
