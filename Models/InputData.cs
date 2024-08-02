using System.ComponentModel.DataAnnotations;

namespace WebVideoTransport.Models
{
    public class InputData
    {
        [Url, Required]
        public string Url { get; set; }

    }
}
