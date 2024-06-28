// using Data.Entities;
// using HotChocolate.Authorization;
// using Server.Services;
// using Shared.Helpers;
// using Shared.InputModels;
//
// namespace Server.GraphQL.Mutations;
//
// public partial class Mutation
// {
//     [GraphQLDescription("Only admins can create a new Organisation.")]
//     [Authorize(Roles = [Constants.Roles.Admin])]
//     public async Task<Organisation> CreateOrganisation(
//         [Service] IOrganisationService organisationService,
//         OrganisationInputModel organisationInputModel,
//         CancellationToken cancellationToken
//     )
//     {
//         Organisation result = await municipalityService.CreateMunicipality(organisationInputModel, cancellationToken);
//         return result;
//     }
// }

namespace Server.GraphQL.Mutations;
