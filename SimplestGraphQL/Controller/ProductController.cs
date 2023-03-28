using GraphQL;
using GraphQL.Transport;
using Microsoft.AspNetCore.Mvc;
using SimplestGraphQL.GraphQL;

namespace SimplestGraphQL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Processes an entire GraphQL request, given an input GraphQL request string. This is intended to
        /// be called by user code to process a query.
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ProductSchema _schema;

        public ProductController(IDocumentExecuter documentExecuter, ProductSchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = request.Query,
                Variables = request.Variables,
                OperationName = request.OperationName,
                RequestServices = HttpContext.RequestServices
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
