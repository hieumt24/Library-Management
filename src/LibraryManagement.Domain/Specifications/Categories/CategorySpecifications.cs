using LibraryManagement.Domain.Common.Specifications;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Specifications.Categories
{
    public class CategorySpecifications
    {
        public static BaseSpecification<Category> GetAllCategoriesSpec()
        {
            var spec = new BaseSpecification<Category>(x => x.IsDeleted == false);
            return spec;
        }
    }
}