using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment5.EntityModels;
using Assignment5.Models;

namespace Assignment5.Controllers
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
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();

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

        /** Album Methods **/

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(p => p.AlbumId));
        }
        public AlbumBaseViewModel AlbumGetById(int? id)
        {
            return mapper.Map<Album, AlbumBaseViewModel>(ds.Albums.Find(id.GetValueOrDefault()));
        }

        /** MediaType Methods **/
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(ds.MediaTypes.OrderBy(item => item.MediaTypeId));
        }

        public MediaTypeBaseViewModel MediaTypeGetById(int? id)
        {
            return mapper.Map<MediaType, MediaTypeBaseViewModel>(ds.MediaTypes.Find(id.GetValueOrDefault()));
        }

        /** Artist Methods **/
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(item => item.ArtistId));
        }
        public ArtistBaseViewModel ArtistGetById(int? id)
        {
            return mapper.Map<Artist, ArtistBaseViewModel>(ds.Artists.Find(id.GetValueOrDefault()));
        }

        /** Track Methods **/
        public IEnumerable<TrackWithDetailViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(ds.Tracks.Include("Album.Artist").Include("Mediatype").OrderBy(p => p.AlbumId).ThenBy(p => p.Name));
        }

        public TrackWithDetailViewModel TrackGetById(int? id)
        {
            return mapper.Map<Track, TrackWithDetailViewModel>(ds.Tracks.Include("Album.Artist").Include("Mediatype").SingleOrDefault(p => p.TrackId == id));
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel obj)
        {
            var media = ds.MediaTypes.SingleOrDefault(p => p.MediaTypeId == obj.MediaTypeId);
            var album = ds.Albums.SingleOrDefault(p => p.AlbumId == obj.AlbumId);

            if (media == null || album == null) { return null; }

            var newTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(obj));
            newTrack.MediaType = media;
            newTrack.Album = album;

            ds.SaveChanges();

            return (newTrack == null) ? null : mapper.Map<Track, TrackBaseViewModel>(newTrack);
        }


        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()
    }
}