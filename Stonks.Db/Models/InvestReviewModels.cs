using System;
using System.Collections.Generic;

namespace Stonks.Db.Models
{
    public class InvestReviewSource
    {
        public int InvestReviewSourceId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<InvestReviewPost> Posts { get; set; } = new List<InvestReviewPost>();
    }

    public class InvestReviewPost
    {
        public int InvestReviewPostId { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }

        public int InvestReviewSourceId { get; set; }
        public InvestReviewSource InvestReviewSource { get; set; }
    }
}