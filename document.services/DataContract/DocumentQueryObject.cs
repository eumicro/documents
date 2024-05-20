using document.model;
using System.Linq.Expressions;

namespace document.services.DataContract
{
    public record DocumentQueryObject(Expression<Func<Document, bool>> Query, BBox BBox);
}
