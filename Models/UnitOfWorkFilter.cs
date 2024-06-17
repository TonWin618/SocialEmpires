using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialEmpires.Models
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception == null)
            {
                var dbContext = context.HttpContext.RequestServices.GetService<AppDbContext>();
                if (dbContext != null)
                {
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
