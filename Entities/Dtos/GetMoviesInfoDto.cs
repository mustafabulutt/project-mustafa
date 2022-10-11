using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{



        public partial class GetMovieInfoDto
        {
            public bool Adult { get; set; }
            public string? BackdropPath { get; set; }
            public BelongsToCollection BelongsToCollection { get; set; }
            public long? Budget { get; set; }
            public List<Genre> Genres { get; set; }
            public string? Homepage { get; set; }
            public long? Id { get; set; }
            public string? ImdbId { get; set; }
            public string? OriginalLanguage { get; set; }
            public string? OriginalTitle { get; set; }
            public string? Overview { get; set; }
            public double? Popularity { get; set; }
            public string? PosterPath { get; set; }
            public List<ProductionCompany> ProductionCompanies { get; set; }
            public List<ProductionCountry> ProductionCountries { get; set; }
            public DateTimeOffset ReleaseDate { get; set; }
            public long? Revenue { get; set; }
            public long? Runtime { get; set; }
            public List<SpokenLanguage> SpokenLanguages { get; set; }
            public string? Status { get; set; }
            public string? Tagline { get; set; }
            public string? Title { get; set; }
            public bool Video { get; set; }
            public double? VoteAverage { get; set; }
            public long? VoteCount { get; set; }


             public double? MeanScore { get; set; }

             public int? MyScore { get; set; }
            
             public List<MovieNote>? MyNotes { get; set; }
        }

        public partial class BelongsToCollection
        {
            public long? Id { get; set; }
            public string? Name { get; set; }
            public string? PosterPath { get; set; }
            public string? BackdropPath { get; set; }
        }

        public partial class Genre
        {
            public long? Id { get; set; }
            public string? Name { get; set; }
        }

        public partial class ProductionCompany
        {
            public long? Id { get; set; }
            public string? LogoPath { get; set; }
            public string? Name { get; set; }
            public string? OriginCountry { get; set; }
        }

        public partial class ProductionCountry
        {
            public string? Iso3166_1 { get; set; }
            public string? Name { get; set; }
        }

        public partial class SpokenLanguage
        {
            public string? EnglishName { get; set; }
            public string? Iso639_1 { get; set; }
            public string? Name { get; set; }
        }
    }




