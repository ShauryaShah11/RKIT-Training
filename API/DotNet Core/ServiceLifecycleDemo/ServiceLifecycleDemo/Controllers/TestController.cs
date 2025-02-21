using Microsoft.AspNetCore.Mvc;
using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Controllers
{
    /// <summary>
    /// Controller to test the behavior of service lifetimes (Transient, Scoped, Singleton).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;
        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;

        /// <summary>
        /// Constructor to inject different service lifetimes.
        /// </summary>
        /// <param name="transientService1">First instance of Transient service.</param>
        /// <param name="transientService2">Second instance of Transient service.</param>
        /// <param name="scopedService1">First instance of Scoped service.</param>
        /// <param name="scopedService2">Second instance of Scoped service.</param>
        /// <param name="singletonService1">First instance of Singleton service.</param>
        /// <param name="singletonService2">Second instance of Singleton service.</param>
        public TestController(
            ITransientService transientService1,
            ITransientService transientService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ISingletonService singletonService1,
            ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        /// <summary>
        /// Endpoint to check how different service lifetimes behave.
        /// </summary>
        /// <returns>Returns GUIDs to demonstrate the lifecycle behavior.</returns>
        [HttpGet("test1")]
        public IActionResult Test1()
        {
            return Ok(new
            {
                TransientGuid1 = _transientService1.GetGuid(),
                ScopedGuid1 = _scopedService1.GetGuid(),
                SingletonGuid1 = _singletonService1.GetGuid(),
                TransientGuid2 = _transientService2.GetGuid(),
                ScopedGuid2 = _scopedService2.GetGuid(),
                SingletonGuid2 = _singletonService2.GetGuid()
            });
        }

        /// <summary>
        /// Another endpoint to validate the service lifecycle behavior.
        /// </summary>
        /// <returns>Returns GUIDs for comparison.</returns>
        [HttpGet("test2")]
        public IActionResult Test2()
        {
            return Ok(new
            {
                TransientGuid1 = _transientService1.GetGuid(),
                ScopedGuid1 = _scopedService1.GetGuid(),
                SingletonGuid1 = _singletonService1.GetGuid(),
                TransientGuid2 = _transientService2.GetGuid(),
                ScopedGuid2 = _scopedService2.GetGuid(),
                SingletonGuid2 = _singletonService2.GetGuid()
            });
        }

        /// <summary>
        /// Simulates an exception to trigger the developer exception page.
        /// </summary>
        /// <returns>Throws an exception.</returns>
        [HttpGet("developer-exception")]
        public IActionResult DeveloperException()
        {
            throw new Exception("This is a test exception for the Developer Exception Page.");
        }
    }
}
