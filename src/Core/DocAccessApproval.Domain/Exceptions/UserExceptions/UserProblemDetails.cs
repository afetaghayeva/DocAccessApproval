using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocAccessApproval.Domain.Exceptions.UserExceptions;

public class UserProblemDetails : ProblemDetails
{
    public override string ToString() => JsonConvert.SerializeObject(this);
}
