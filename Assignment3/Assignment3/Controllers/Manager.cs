using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment3.EntityModels;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                cfg.CreateMap<Employee, EmployeeBaseViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<EmployeeBaseViewModel, EmployeeEditContactFormViewModel>();
               
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees);
        }
        public EmployeeBaseViewModel EmployeeGetById(int id)
        {
            var obj = ds.Employees.Find(id);

            return obj == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(obj);
        }

        public EmployeeBaseViewModel EmployeeEditContactInfo(EmployeeEditContactViewModel employee)
        {
            var obj = ds.Employees.Find(employee.EmployeeId);

            if(obj ==null)
            {
                return null;
            }

            else
            {
                ds.Entry(obj).CurrentValues.SetValues(employee);
                ds.SaveChanges();

                return mapper.Map<Employee, EmployeeBaseViewModel>(obj);
            }
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(p => p.AlbumId).ThenBy(p => p.Name));
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllPop()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.Where(p => p.GenreId == 9).OrderBy(p => p.Name));
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllDeepPurple()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.Where(p => p.Composer.Contains("Jon Lord")).OrderBy(p => p.TrackId));
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop100Longest()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderByDescending(p => p.Milliseconds).Take(100));
        }

        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()
    }
}