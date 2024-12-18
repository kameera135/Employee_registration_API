using System;
using System.Collections.Generic;

namespace EmployeeRegistration.Models;

public partial class Employee
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Epf { get; set; }

    public int? Mobile { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? DeletedBy { get; set; }

    public long? DeletedAt { get; set; }
}
