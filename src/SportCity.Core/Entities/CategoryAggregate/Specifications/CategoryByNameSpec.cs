using Ardalis.Specification;
using SportCity.Core.Entities.CityAggregate;

namespace SportCity.Core.Entities.CategoryAggregate.Specifications;

public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name)
    {
        Query
            .Where(c => c.Name.ToLower() == name.ToLower());
    }
}
