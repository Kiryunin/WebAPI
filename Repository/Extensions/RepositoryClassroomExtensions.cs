using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryClassroomExtensions
    {
        public static IQueryable<Classroom> FilterClassrooms(this IQueryable<Classroom>
            classrooms, uint minNumberOfSeats, uint maxNumberOfSeats) => classrooms.Where(e => (e.NumberOfSeats >= minNumberOfSeats && e.NumberOfSeats <= maxNumberOfSeats));
        public static IQueryable<Classroom> SearchClassrooms(this IQueryable<Classroom> classrooms, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return classrooms;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return classrooms.Where(e => e.Type.ToLower().Contains(lowerCaseTerm));           
        }
        public static IQueryable<Classroom> SortClassrooms(this IQueryable<Classroom> classrooms, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return classrooms.OrderBy(e => e.Type);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Classroom>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return classrooms.OrderBy(e => e.Type);
            return classrooms.OrderBy(orderQuery);
        }
    }
}
