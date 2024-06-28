using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationRole : IdentityRole { }

//https://stackoverflow.com/questions/24364152/not-set-discriminator-column-when-save-entity-in-entity-framework
//inherit twice from the same Class to get rid of this discriminator error.
//code below does not to work in builder, therefore i do it in this way.
//modelBuilder.Entity<ApplicationUser>().Map(p => p.Requires("Discriminator").HasValue("ApplicationUser"));
public class ApplicationRoleIAmNotForUsage : IdentityRole { }
