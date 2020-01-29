using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment4.EntityModels;
using Assignment4.Models;

namespace Assignment4.Controllers
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
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();

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
        public IEnumerable<InvoiceBaseViewModel> InvoiceGetall()
        {
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(ds.Invoices.OrderBy(p => p.CustomerId).ThenBy(p => p.InvoiceDate));
        }

      /*  public InvoiceBaseViewModel InvoiceGetById(int id)
        {
            var obj = ds.Invoices.Find(id);
            return (obj == null) ? null : mapper.Map<Invoice, InvoiceBaseViewModel>(obj);
        }*/

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id)
        {
            var obj = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(c => c.InvoiceId == id);

            return (obj == null) ? null : mapper.Map<Invoice, InvoiceWithDetailViewModel>(obj);
        }
       

        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()
    }
}