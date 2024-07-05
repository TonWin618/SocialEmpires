using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace SocialEmpires.Models
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WithLanguage<T>(this IQueryable<T> source, string language)
        {
            Type type = typeof(T);
            var parameter = Expression.Parameter(type, "e");

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => !Attribute.IsDefined(p,typeof(NotMappedAttribute)));

            var memberBindings = new List<MemberBinding>();
            var multiLangageProperties = properties.Where(p => p.PropertyType == typeof(MultiLanguageString));
            foreach (var property in properties)
            {
                if(property.PropertyType == typeof(MultiLanguageString))
                {
                    var languageProperty = typeof(MultiLanguageString).GetProperty(language);
                    var multiLanguageStringInstance = Expression.MemberInit(
                        Expression.New(typeof(MultiLanguageString)),
                        Expression.Bind(languageProperty, Expression.Property(Expression.Property(parameter,property.Name),language))
                    );
                    memberBindings.Add(Expression.Bind(property, multiLanguageStringInstance));
                }
                else
                {
                    memberBindings.Add(Expression.Bind(property, Expression.Property(parameter, property)));
                }
                
            }

            var body = Expression.MemberInit(Expression.New(type), memberBindings);
            var selector = Expression.Lambda<Func<T, T>>(body, parameter);
            return source.Select(selector);
        }
    }
}
