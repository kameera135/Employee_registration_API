namespace EmployeeRegistration.DTO
{
    public class EmployeeDTO
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? epf { get; set; }
        public int? mobile { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
    }

    public class PostEmployeeDTO
    {
        public string? name { get; set; }
        public string? epf { get; set; }
        public int? mobile { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
    }
}
