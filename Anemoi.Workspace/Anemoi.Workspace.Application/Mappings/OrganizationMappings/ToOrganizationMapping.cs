using Anemoi.BuildingBlock.Application.Abstractions;
using Anemoi.BuildingBlock.Application.Helpers;
using Anemoi.Contract.Workspace.Commands.OrganizationCommands.CreateOrganization;
using Anemoi.Contract.Workspace.Commands.OrganizationCommands.UpdateOrganization;
using Anemoi.Contract.Workspace.ModelIds;
using AutoMapper;
using Anemoi.Workspace.Domain.Models;

namespace Anemoi.Workspace.Application.Mappings.OrganizationMappings;

public sealed class ToOrganizationMapping : Profile
{
    public ToOrganizationMapping()
    {
        CreateMap<CreateOrganizationCommand, Organization>()
            .ForMember(x => x.Id, opt => opt.MapFrom(_ => new OrganizationId(IdGenerator.NextGuid())))
            .ForMember(x => x.WorkspaceId, opt => opt.MapFrom<WorkspaceIdValueResolver>())
            .ForMember(x => x.CreatedTime, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember is { }));
        CreateMap<UpdateOrganizationCommand, Organization>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember is { }));
    }

    private class WorkspaceIdValueResolver(IWorkspaceIdGetter workspaceIdGetter)
        : IValueResolver<CreateOrganizationCommand, Organization, WorkspaceId>
    {
        public WorkspaceId Resolve(CreateOrganizationCommand source, Organization destination, WorkspaceId destMember,
            ResolutionContext context) => new(Guid.Parse(workspaceIdGetter.WorkspaceId));
    }
}