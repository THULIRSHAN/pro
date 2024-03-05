using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using pro.Models.Version;
using System;

[Route("api/version")]
[ApiController]
public class VersionController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiVersionInfo> GetVersion()
    {
        var versionInfo = new ApiVersionInfo
        {
            Framework = "7.0",
           
    };

        return Ok(versionInfo);
    }

    private string GetFrameworkVersion()
    {
        return Environment.Version.ToString();
    }

    private string GetRuntimeVersion()
    {
        return System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
    }
}
