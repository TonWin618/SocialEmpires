using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace SocialEmpires.Models
{
    public static class IQueryableExtensions
    {
        private static ConcurrentDictionary<string, object> _cachedExpressions = new();

        public static IQueryable<T> WithLanguage<T>(this IQueryable<T> query, string language) where T:class
        {
            Expression<Func<T, T>> selector;
            if (_cachedExpressions.TryGetValue(GenerateKey<T>(language), out var func))
            {
                selector = func as Expression<Func<T, T>>;
            }
            else
            {
                selector = BuildWithLanguageExpression<T>(language);
                _cachedExpressions.TryAdd(GenerateKey<T>(language), selector);
            }
            return query.Select(selector);
        }

        private static string GenerateKey<T>(string language)
        {
            return nameof(T) + '_' + language;
        }

        private static Expression<Func<T, T>> BuildWithLanguageExpression<T>(string language) where T:class
        {
            Type type = typeof(T);
            var parameter = Expression.Parameter(type, "e");

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)));

            var memberBindings = new List<MemberBinding>();
            var multiLangageProperties = properties.Where(p => p.PropertyType == typeof(MultiLanguageString));
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(MultiLanguageString))
                {
                    var languageProperty = typeof(MultiLanguageString).GetProperty(language);
                    var multiLanguageStringInstance = Expression.MemberInit(
                        Expression.New(typeof(MultiLanguageString)),
                        Expression.Bind(languageProperty, Expression.Property(Expression.Property(parameter, property.Name), language))
                    );
                    memberBindings.Add(Expression.Bind(property, multiLanguageStringInstance));
                }
                else
                {
                    memberBindings.Add(Expression.Bind(property, Expression.Property(parameter, property)));
                }

            }

            var body = Expression.MemberInit(Expression.New(type), memberBindings);
            return Expression.Lambda<Func<T, T>>(body, parameter);
        }
    }
}
