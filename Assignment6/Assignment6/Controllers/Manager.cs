using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment6.EntityModels;
using Assignment6.Models;

namespace Assignment6.Controllers
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
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<PlaylistBaseViewModel, Playlist>();
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
                cfg.CreateMap<PlaylistWithTracksViewModel, PlaylistBaseViewModel>();



            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            return mapper.Map<IEnumerable<PlaylistBaseViewModel>>(ds.Playlists.Include("Tracks").OrderBy(s => s.Name));
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(s => s.Name));
        }

      //  public IEnumerable<PlaylistWithTracksViewModel> PlaylistGetAllWithTracks()
       // {
       //     return mapper.Map<IEnumerable<PlaylistWithTracksViewModel>>(ds.Playlists.Include("Tracks").OrderBy(e => e.Name));
     //   }
        public PlaylistBaseViewModel PlaylistGetByIdWithDetail(int id)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(e => e.PlaylistId == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Playlist, PlaylistBaseViewModel>(o);

        }

        public PlaylistBaseViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection

            var o = ds.Playlists.Include("Tracks").SingleOrDefault(e => e.PlaylistId == newItem.Id);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.Tracks.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.TracksIds)
                {
                    var a = ds.Tracks.Find(item);
                    o.Tracks.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<Playlist,PlaylistBaseViewModel>(o);
            }
        }


                // Add methods below
                // Controllers will call these methods
                // Ensure that the methods accept and deliver ONLY view model objects and collections
                // The collection return type is almost always IEnumerable<T>

                // Suggested naming convention: Entity + task/action
                // For example:
                // ProductGetAll()
                // ProductGetById()
                // ProductAdd()
                // ProductEdit()
                // ProductDelete()
            }
}