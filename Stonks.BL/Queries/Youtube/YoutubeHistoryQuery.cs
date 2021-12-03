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
        public async Task<List<YoutubeVideoDataQuery.ResultModel>> Get(string channelId)
        {
            if (string.IsNullOrEmpty(channelId) || channelId.Contains("https") || channelId.Contains("youtube.com"))
            {
                throw new NotImplementedException(
                    $"The parameter value ({channelId}) you provided  is not supported. " +
                    $"Please pass channelId without any url part");
            }

            var youtube = new YoutubeClient();
            var videos = await youtube.Channels.GetUploadsAsync(channelId);
            var result = new List<YoutubeVideoDataQuery.ResultModel>(videos.Count);

            var videoQuery = new YoutubeVideoDataQuery();
            foreach (var video in videos)
            {
                var metadata = await videoQuery.Get(video);
                result.Add(metadata);
            }

            return result.OrderBy(r => r.UploadDate).ToList();
        }
    }
}