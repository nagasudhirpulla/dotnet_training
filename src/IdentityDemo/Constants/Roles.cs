using System.Reflection;

namespace IdentityDemo.Constants;

public abstract class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string User = nameof(User);

    public static IEnumerable<string> GetRoles()
    {
        // Use reflection to get all constant string values defined in this class
        FieldInfo[] fields = typeof(Roles).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy);
        foreach (FieldInfo field in fields)
        {
            if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
            {
                yield return (string)(field.GetValue(null) ?? throw new Exception($"Failed to get value for role {field.Name}"));
            }
        }
    }
}
