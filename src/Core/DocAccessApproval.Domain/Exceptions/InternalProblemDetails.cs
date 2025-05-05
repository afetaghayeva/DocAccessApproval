using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocAccessApproval.Domain.Exceptions;

public class InternalProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
