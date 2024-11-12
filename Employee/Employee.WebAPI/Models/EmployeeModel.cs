using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Employee.WebAPI.Models
{
    public class EmployeeModel
    {
        [JsonProperty("employeeId")]
        public Guid EmployeeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("salary")]
        public int Salary { get; set; }
    }
}
