﻿using Anemoi.BuildingBlock.Domain;

namespace Anemoi.Contract.Identity.ModelIds;

public sealed record UserMapRoleGroupId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public override string ToString() => base.ToString();
}