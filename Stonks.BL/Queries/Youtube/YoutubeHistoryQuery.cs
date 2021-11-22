using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;

namespace Stonks.BL.Queries.Youtube
{
    public class YoutubeHistoryQuery
    {
        public class ResultModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTimeOffset UploadDate { get; set; }
            public string Description { get; set; }
            public TimeSpan? Duration { get; set; }
            public IReadOnlyList<Thumbnail> Thumbnails { get; set; }
            public IReadOnlyList<string> Keywords { get; set; }
        }

        public async Task<List<ResultModel>> Get(string channelId)
        {
            var youtube = new YoutubeClient();
            var videos = await youtube.Channels.GetUploadsAsync(channelId);
            var result = new List<ResultModel>(videos.Count);


            foreach (var video in videos)
            {
                // todo: slow. Need to cache metadata after initial load
                var metadata = await youtube.Videos.GetAsync(video.Id);

                result.Add(new ResultModel
                {
                    Id = video.Id,
                    Title = video.Title,
                    Author = video.Author.Title,
                    UploadDate = metadata.UploadDate,
                    Description = metadata.Description,
                    Duration = metadata.Duration,
                    Thumbnails = metadata.Thumbnails,
                    Keywords = metadata.Keywords,
                });
            }

            return result.OrderBy(r => r.UploadDate).ToList();
        }
    }
}