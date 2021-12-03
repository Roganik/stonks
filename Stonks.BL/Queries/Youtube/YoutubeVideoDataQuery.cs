using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace Stonks.BL.Queries.Youtube
{
    public class YoutubeVideoDataQuery
    {
        public class ResultModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTimeOffset UploadDate { get; set; }
            public string Description { get; set; }
            public TimeSpan? Duration { get; set; }
            public IReadOnlyList<string> Keywords { get; set; }
        }

        public async Task<ResultModel> Get(PlaylistVideo video)
        {
            var youtube = new YoutubeClient();

            // todo: slow. Need to cache metadata after initial load
            var metadata = await youtube.Videos.GetAsync(video.Id);

            var result = new ResultModel
            {
                Id = video.Id,
                Title = video.Title,
                Author = video.Author.Title,
                UploadDate = metadata.UploadDate,
                Description = metadata.Description,
                Duration = metadata.Duration,
                Keywords = metadata.Keywords,
            };
            return result;
        }
    }
}