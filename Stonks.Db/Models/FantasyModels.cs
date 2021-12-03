using System;
using System.Collections.Generic;

namespace Stonks.Db.Models
{
    public class FantasyTeam
    {
        public int FantasyTeamId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public List<FantasyTeamMember> TeamMembers { get; set; } = new List<FantasyTeamMember>();
    }

    public class FantasyTeamMember
    {
        public int FantasyTeamMemberId { get; set; }

        public string StockSymbol { get; set; }
        public string Exchange { get; set; }

        public DateTime PublicationDate { get; set; }
        public decimal PriceOnPublicationDate { get; set; }

        public DateTime PriceDate { get; set; }
        public decimal Price { get; set; }

        public int InvestReviewPostId { get; set; }
        public InvestReviewPost InvestReviewPost { get; set; }
    }

}