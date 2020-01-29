using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here
           /* LoadData();
            LoadDataGenre();
            LoadDataArtist();
            LoadDataTrack();
            LoadDataAlbum();*/
            
            
           
            
            
            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithAlbumViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithArtistAndTrackViewModel>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackWithDetailsViewModel>();
               



            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
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
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithAlbumViewModel ArtistGetByIdWithAlbum(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(e => e.Id == id);

            return (o == null) ? null : mapper.Map<Artist, ArtistWithAlbumViewModel>(o);

        }

        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            var o = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));
            o.Executive = User.Name;
            ds.SaveChanges();
            return mapper.Map<Artist, ArtistBaseViewModel>(o);
        }

        //ALBUM ENTITY
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumWithArtistAndTrackViewModel AlbumGetByIdWithDetails(int id)
        {
            var o = ds.Albums.Include("Tracks")
                .Include("Artists")
                .SingleOrDefault(e => e.Id == id);

            return (o == null) ? null : mapper.Map<Album, AlbumWithArtistAndTrackViewModel>(o);

        }

        public AlbumWithArtistAndTrackViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            //check if artist/s exist/s before adding new album
            var a = new List<Artist>();
            foreach (var i in newItem.ArtistIds)
            {
                var artist = ds.Artists.Find(i);
                if (artist == null)
                {
                    return null;
                }
                else
                {
                    a.Add(artist);
                }
            }

            var o = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
            o.Coordinator = User.Name;
            o.Artists = a;
            ds.SaveChanges();
            return mapper.Map<Album, AlbumWithArtistAndTrackViewModel>(o);

        }

        //TRACK ENTITY
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(a => a.Name));
        }

        public TrackWithDetailsViewModel TrackGetById(int id)
        {

            var o = ds.Tracks.Include("Albums.Artists")
                .SingleOrDefault(t => t.Id == id);

            if (o == null)
            {
                return null;
            }
            else
            {
                // Create the result collection
                var result = mapper.Map<Track, TrackWithDetailsViewModel>(o);
                // Fill in the album names
                result.AlbumNames = o.Albums.Select(a => a.Name);  
                return result;
            }
        }

        public TrackWithDetailsViewModel TrackAdd(TrackAddViewModel newItem)
        {
            //check if album exists before adding new track
            var a = ds.Albums.Find(newItem.AlbumId);

            if (a == null)
            {
                return null;
            }
            else
            {
                var o = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));
                o.Clerk = User.Name;
                o.Albums.Add(a);
                ds.SaveChanges();
                return mapper.Map<Track, TrackWithDetailsViewModel>(o);
            }
        }

      


        public bool TrackDelete(int id)
        {
            var t = ds.Tracks.Find(id);
            if (t == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Tracks.Remove(t);
                ds.SaveChanges();
                return true;
            }
        }


        //GENRE ENTITY
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(a => a.Name));
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            if (ds.RoleClaims.Count() == 0)
            {
                // Create and add objects
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                // Save changes
                ds.SaveChanges();

                return true;
            }
            return done;
        }


        // Load Initial Data / Genre
        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() == 0)
            {
                // Create and add objects
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Dance" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Folk" });
                ds.Genres.Add(new Genre { Name = "Hip hop" });
                ds.Genres.Add(new Genre { Name = "Blues" });
                ds.Genres.Add(new Genre { Name = "Classic" });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }

        // Load Initial Data / Artist
        public bool LoadDataArtist()
        {
            if (ds.Artists.Count() == 0)
            {
                // Create and add objects
                ds.Artists.Add(new Artist
                {
                    Name = "Neeti",
                    BirthName = "Neeti Mohan",
                    BirthOrStartDate = new DateTime(1979, 11, 18),
                    Executive = "exec@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/1/13/Neeti_Mohan_attends_Shakti_Mohan%E2%80%99s_Nritya_Shakti_celebrations_for_World_Dance_Day_%2804%29_%28cropped%29.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Palak",
                    BirthName = "Palak Muchhal",
                    BirthOrStartDate = new DateTime(1992, 03, 30),
                    Executive = "exec@example.com",
                    Genre = "Folk",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/b/b2/Palak_Muchhal_filmfare.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Arijit",
                    BirthName = "Arijit Singh",
                    BirthOrStartDate = new DateTime(1987, 04, 25),
                    Executive = "exec@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://www.theindianmoviechannel.com/assets/uploads/15405390846235.jpg"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }

        // Load Initial Data / Album
        public bool LoadDataAlbum()
        {
            if (ds.Albums.Count() == 0)
            {
                //arijit
                var arijit = ds.Artists.SingleOrDefault(a => a.Name == "Arijit");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { arijit },
                    Name = "The Epic Collection",
                    ReleaseDate = new DateTime(2011, 01, 24),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://s.mxmcdn.net/images-storage/albums/1/8/0/3/5/0/32053081_350_350.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { arijit },
                    Name = "Aashiqui 2",
                    ReleaseDate = new DateTime(2010, 11, 29),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://images-na.ssl-images-amazon.com/images/I/81ACYtftx8L._RI_SX300_.jpg"
                });

                //neeti
                var neeti = ds.Artists.SingleOrDefault(a => a.Name == "Neeti");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { neeti },
                    Name = "Jab Tak Hai Jaan",
                    ReleaseDate = new DateTime(2012, 04, 17),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://media-images.mio.to/by_artist/H/Harshdeep%20Kaur%2C%20Javed%20Ali%2C%20Mohit%20Chauhan%2C%20Neeti%20Mohan%2C%20Rabbi%2C%20Raghav%20Mathur%2C%20Safia%20Ashraf%2C%20Shakthishree%20Gopalan%2C%20Shilpa%20Rao%2C%20Shreya%20Ghoshal/Back2Back%20-%20Jab%20Tak%20Hai%20Jaan%20%282015%29/Art-350.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { neeti },
                    Name = "Bajirao Mastani",
                    ReleaseDate = new DateTime(2015, 04, 17),
                    Coordinator = "coord@example.com",
                    Genre = "Folk",
                    UrlAlbum = "https://i.ytimg.com/vi/odrml3vYQjE/maxresdefault.jpg"
                });

                //palak
                var palak = ds.Artists.SingleOrDefault(a => a.Name == "Palak");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { palak },
                    Name = "From Sydney with Love",
                    ReleaseDate = new DateTime(2012, 08, 31),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/4/45/Poster_of_hindi_film_from_sydney_with_love.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { palak },
                    Name = "Zanjeer",
                    ReleaseDate = new DateTime(2013, 09, 06),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/a/ad/Zanjeer_poster.jpg"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }


        // Load Initial Data / Track
        public bool LoadDataTrack()
        {
            if (ds.Tracks.Count() == 0)
            {
                //arijit
                var arijit = ds.Albums.SingleOrDefault(a => a.Name == "The Epic Collection");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { arijit },
                    Name = "Tum hi ho",
                    Composer = "Arijit Singh",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { arijit },
                    Name = "Sanam Re",
                    Composer = "Arijit Singh",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { arijit },
                    Name = "Tera Fitoor",
                    Composer = "Arijit Singh",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { arijit },
                    Name = "Phir bhi Tumko Chahunga",
                    Composer = "Arijit Singh",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { arijit },
                    Name = "Hamari Adhuri Kahani",
                    Composer = "Arijit Singh",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                //neeti
                var neeti = ds.Albums.SingleOrDefault(a => a.Name == "Jab Tak Hai Jaan");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { neeti },
                    Name = "Challa",
                    Composer = "Rabbi Shergill",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { neeti },
                    Name = "Saans",
                    Composer = "Shreya Ghoshal, Mohit Chauhan",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { neeti },
                    Name = "Ishq Shava",
                    Composer = "Shilpa Rao",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { neeti },
                    Name = "Heer",
                    Composer = "Harshdeep Kaur",
                    Genre = "Pop",
                    Clerk = "clerkd@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { neeti },
                    Name = "Jiya re",
                    Composer = "Neeti Mohan",
                    Genre = "Pop",
                    Clerk = "clerkd@example.com"
                });


                //palak
                var palak = ds.Albums.SingleOrDefault(a => a.Name == "Zanjeer");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { palak },
                    Name = "Mumbai ke hero",
                    Composer = "Mika Singh,Talia Bentson",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { palak },
                    Name = "Pinky",
                    Composer = "Meet Bros",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { palak },
                    Name = "Lamha Tera Mera",
                    Composer = "Palak Muchhal",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { palak },
                    Name = "Kaatilana",
                    Composer = "Palak Muchhal",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { palak },
                    Name = "Shakila Banoo",
                    Composer = "Shreya Ghoshal",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }
            return false;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}