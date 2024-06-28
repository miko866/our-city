using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data;

public static class ModelBuilderSnakeCaseExtension
{
    /// <summary>
    /// ConvertToSnakeCase
    /// Convert Entity Framework Core syntax into PostgreSQL snake case
    /// </summary>
    /// <param name="builder"></param>
    public static void ConvertToSnakeCase(this ModelBuilder builder)
    {
        foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
        {
            // Replace table names
            entity.SetTableName(entity.GetTableName()!.ToSnakeCase());

            // Replace column names
            foreach (IMutableProperty property in entity!.GetProperties())
            {
                string? columnName = property.GetColumnName(
                    StoreObjectIdentifier.Table(property.DeclaringEntityType.GetTableName()!, null)
                );
                property.SetColumnName(columnName!.ToSnakeCase());
            }

            foreach (IMutableKey key in entity.GetKeys())
            {
                key.SetName(key.GetName()!.ToSnakeCase());
            }

            foreach (IMutableForeignKey key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName()!.ToSnakeCase());
            }

            foreach (IMutableIndex index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.Name!.ToSnakeCase());
            }
        }
    }

    /// <summary>
    /// ToSnakeCase
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        Match startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}
