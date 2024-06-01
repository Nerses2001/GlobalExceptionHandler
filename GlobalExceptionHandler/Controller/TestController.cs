using Microsoft.AspNetCore.Mvc;

namespace Test.Controller;

public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] { "Product1", "Product2" };
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {

        throw new ArgumentException();
        return "Product" + id;
    }

    // POST: api/products
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

}