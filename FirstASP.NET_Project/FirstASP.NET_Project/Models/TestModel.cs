

using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FirstASP.NET_Project.Models
{
    public class TestModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings= false,ErrorMessage ="Ooops!")]
        public string Product { get; set; }
    }
}
