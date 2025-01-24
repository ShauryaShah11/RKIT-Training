using Microsoft.AspNetCore.Mvc;
using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        public readonly ITransientService _transientService1;
        public readonly ITransientService _transientService2;
        public readonly IScopedService _scopedService1;
        public readonly IScopedService _scopedService2;
        public readonly ISingletonService _singletonService1;
        public readonly ISingletonService _singletonService2;

        public TestController(ITransientService transientService1, ITransientService transientService2, IScopedService scopedService1, IScopedService scopedService2, ISingletonService singletonService1, ISingletonService singletonService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        [HttpGet("/test1")]
        public IActionResult Test1()
        {
            var transientGuid1 = _transientService1.GetGuid();
            var scopedGuid1 = _scopedService1.GetGuid();
            var singletonGuid1 = _singletonService1.GetGuid();

            var transientGuid2 = _transientService2.GetGuid();
            var scopedGuid2 = _scopedService2.GetGuid();
            var singletonGuid2 = _singletonService2.GetGuid();

            return Ok(new
            {
                TransientGuid1 = transientGuid1,
                ScopedGuid1 = scopedGuid1,
                SingletonGuid1 = singletonGuid1,
                TransientGuid2 = transientGuid2,
                ScopedGuid2 = scopedGuid2,
                SingletonGuid2 = singletonGuid2
            });
        }

        [HttpGet("/test2")]
        public IActionResult Test2()
        {
            var transientGuid1 = _transientService1.GetGuid();
            var scopedGuid1 = _scopedService1.GetGuid();
            var singletonGuid1 = _singletonService1.GetGuid();

            var transientGuid2 = _transientService2.GetGuid();
            var scopedGuid2 = _scopedService2.GetGuid();
            var singletonGuid2 = _singletonService2.GetGuid();

            return Ok(new
            {
                TransientGuid1 = transientGuid1,
                ScopedGuid1 = scopedGuid1,
                SingletonGuid1 = singletonGuid1,
                TransientGuid2 = transientGuid2,
                ScopedGuid2 = scopedGuid2,
                SingletonGuid2 = singletonGuid2
            });
        }
    }
}
