using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Models.DTO
{
    public class Status
    {
       public int StatusCode { get; set; }
       public string Message { get; set; }
    }
}
