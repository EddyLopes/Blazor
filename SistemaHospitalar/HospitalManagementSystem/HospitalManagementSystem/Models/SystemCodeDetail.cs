﻿namespace HospitalManagementSystem.Models;

public class SystemCodeDetail : UserCreateActivity
{
    public int Id { get; set; }
    public int SystemCodeId { get; set; }
    public SystemCode SystemCode { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int OrderNo { get; set; }
}
