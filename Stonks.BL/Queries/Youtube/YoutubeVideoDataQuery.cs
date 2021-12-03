using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stonks.Db;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace Stonks.BL.Queries.Youtube
{
    public class YoutubeVideoDataQuery
    {
        private readonly FantasyDbContext _fantasy;

        public class ResultModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTimeOffset UploadDate { get; set; }
            public string Description { get; set; }
            public IReadOnlyList<string> Keywords { get; set; }
        }

        public YoutubeVideoDataQuery(FantasyDbContext fantasy)
        {
            _fantasy = fantasy;
        }

        public async Task<ResultModel> Get(PlaylistVideo video)
        {
            var youtube = new YoutubeClient();

            // read from Db if possible
            var dbData = await _fantasy.InvestReviewPosts.Where(i => i.ExternalId == video.Id.Value).SingleOrDefaultAsync();
            if (dbData != null)
            {
                return new ResultModel()
                {
                    Id = video.Id,
                    Title = video.Title,
                    Author = video.Author.Title,
                    UploadDate = dbData.PublicationDate,
                    Description = dbData.Content,
                    Keywords = dbData.Tags,
                };
            }

            // read from youtube if missing in Db
            var metadata = await youtube.Videos.GetAsync(video.Id);
            var result = new ResultModel
            {
                Id = video.Id,
                Title = video.Title,
                Author = video.Author.Title,
                UploadDate = metadata.UploadDate,
                Description = metadata.Description,
                Keywords = metadata.Keywords,
            };
            return result;
        }
    }
}