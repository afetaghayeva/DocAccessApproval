using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocAccessApproval.Domain.Exceptions.DocumentExceptions;

public class DocumentProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
