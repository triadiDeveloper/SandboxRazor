﻿namespace SandboxRazor.BaseEntity;

public interface IIdentityInt
{
    public int Id { get; set; }
}

public interface IIdentityGuid
{
    public Guid Id { get; set; }
}