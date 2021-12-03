using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stonks.BL.Queries.Youtube;
using Stonks.Db;
using Stonks.Db.Models;

namespace Stonks.BL.Commands.InitialLoad
{
    public class InvestReviewYoutubeChannelSourceLoaderCommand : BaseCommand<InvestReviewYoutubeChannelSourceLoaderCommand.InModel,
        InvestReviewYoutubeChannelSourceLoaderCommand.OutModel>
    {
        public class InModel
        {
            public string Url { get; set; }
            public string ChannelId { get; set; }
        }

        public class OutModel
        {
            public bool IsSuccess { get; set; } = true;
            public string Message { get; set; }
        }

        public InvestReviewYoutubeChannelSourceLoaderCommand(StocksDbContext stocks, FantasyDbContext fantasy) : base(stocks, fantasy)
        {
        }

        protected override async Task<OutModel> Execute(InModel model, CancellationToken cancellationToken)
        {
            var youtube = new YoutubeHistoryQuery(this.Fantasy);

            var channelHistory = await youtube.Get(model.ChannelId);
            if (!channelHistory.Any())
            {
                return new OutModel() { IsSuccess = false, Message = "Didn't found videos in channel"};
            }

            var author = channelHistory.First().Author;
            var dbChannel = await GetYoutubeChannelDb(cancellationToken, model.Url, author);
            dbChannel = AddNewVideosToDbChannel(channelHistory, dbChannel);

            await Fantasy.SaveChangesAsync(cancellationToken);
            return new OutModel() { IsSuccess = true };
        }

        private async Task<InvestReviewSource> GetYoutubeChannelDb(CancellationToken cancellationToken,
            string youtubeChannelUrl, string youtubeChannelAuthor)
        {
            var dbChannel = await base.Fantasy.InvestReviewSources
                .Include(i => i.Posts)
                .Where(i => i.Url.ToLower() == youtubeChannelUrl.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            if (dbChannel != null)
            {
                return dbChannel;
            }

            var source = new InvestReviewSource()
            {
                Name = youtubeChannelAuthor,
                Url = youtubeChannelUrl,
            };

            Fantasy.InvestReviewSources.Add(source);
            return source;
        }

        private InvestReviewSource AddNewVideosToDbChannel(List<YoutubeVideoDataQuery.ResultModel> channelHistory, InvestReviewSource dbChannel)
        {
            foreach (var youtubeVideo in channelHistory)
            {
                var dbVideo = dbChannel.Posts.FirstOrDefault(p => p.ExternalId == youtubeVideo.Id);
                if (dbVideo != null)
                {
                    continue;
                }

                dbVideo = new InvestReviewPost()
                {
                    Content = youtubeVideo.Description,
                    ExternalId = youtubeVideo.Id,
                    InvestReviewSource = dbChannel,
                    PublicationDate = youtubeVideo.UploadDate.DateTime,
                    Title = youtubeVideo.Title,
                    Tags = youtubeVideo.Keywords.ToArray(),
                };
                dbChannel.Posts.Add(dbVideo);
            }

            return dbChannel;
        }
    }
}