using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FantasyGridironSite.Models
{
    public class League
    {
        public ObjectId Id { get; set; }

        //Standard League settings

        [BsonElement("Name")]
        public string LeagueName { get; set; }

        [BsonElement("Owner")]
        public string LeagueOwner { get; set; }

        [BsonElement("Photo")]
        public string LeaguePhoto { get; set; }

        [BsonElement("Teams")]
        public string NumOfTeams { get; set; }

        [BsonElement("Divisions")]
        public string NumOfDivisions { get; set; }

        [BsonElement("MaxAdds")]
        public string MaxAdds { get; set; }

        [BsonElement("TradeDeadline")]
        public string TradeDeadline { get; set; }

        [BsonElement("WaiverPeriod")]
        public string WaiverPeriod { get; set; }

        //Player Composition Settings
        [BsonElement("QB")]
        public int QB { get; set; }

        [BsonElement("RB")]
        public int RB { get; set; }

        [BsonElement("WR")]
        public int WR { get; set; }

        [BsonElement("TE")]
        public int TE { get; set; }

        [BsonElement("WR/RB")]
        public int Flex { get; set; }

        [BsonElement("QB/WR/RB/TE")]
        public int SuperFlex { get; set; }

        [BsonElement("Kicker")]
        public int K { get; set; }

        [BsonElement("DST")]
        public int DST { get; set; }

        [BsonElement("DL")]
        public int DL { get; set; }

        [BsonElement("LB")]
        public int LB { get; set; }

        [BsonElement("DB")]
        public int DB { get; set; }

        [BsonElement("IDPFlex")]
        public int IDPFlex { get; set; }

        [BsonElement("Coach")]
        public int Coach { get; set; }

        [BsonElement("Bench")]
        public int Bench { get; set; }

        //Offensive Scoring Settings

        [BsonElement("Completions")]
        public double Completions { get; set; }

        [BsonElement("PassingYards")]
        public double PassingYards { get; set; }

        [BsonElement("PassingTouchdowns")]
        public double PassingTouchdowns { get; set; }

        [BsonElement("InterceptionsThrown")]
        public double InterceptionsThrown { get; set; }

        [BsonElement("40PassingTDBonus")]
        public double FourtyPassingTDBonus { get; set; }

        [BsonElement("50PassingTDBonus")]
        public double FiftyPassingTDBonus { get; set; }

        [BsonElement("RushingAttempts")]
        public double RushingAttempts { get; set; }

        [BsonElement("RushingYards")]
        public double RushingYards { get; set; }

        [BsonElement("RushingTD")]
        public double RushingTD { get; set; }

        [BsonElement("40RushingTDBonus")]
        public double FourtyRushingTDBonus { get; set; }

        [BsonElement("50RushingTDBonus")]
        public double FiftyRushingTDBonus { get; set; }

        [BsonElement("Receptions")]
        public double Receptions { get; set; }

        [BsonElement("ReceivingYards")]
        public double ReceivingYards { get; set; }

        [BsonElement("ReceivingTD")]
        public double ReceivingTD { get; set; }

        [BsonElement("40ReceivingTDBonus")]
        public double FourtyReceivingTDBonus { get; set; }

        [BsonElement("50ReceivingTDBonus")]
        public double FiftyReceivingTDBonus { get; set; }

        [BsonElement("FumbleLost")]
        public double FumbleLost { get; set; }

        [BsonElement("2PointConversion")]
        public double TwoPointConversion { get; set; }

        //Kicking Settings

        [BsonElement("PAT")]
        public double PAT { get; set; }

        [BsonElement("FG40")]
        public double FGFourty { get; set; }

        [BsonElement("FG50")]
        public double FGFIFTY { get; set; }

        [BsonElement("MissedFG")]
        public double MissedFG { get; set; }

        //Defensive Settings

        [BsonElement("Tackle")]
        public double Tackle { get; set; }

        [BsonElement("Assist")]
        public double Assist { get; set; }

        [BsonElement("Sack")]
        public double Sack { get; set; }

        [BsonElement("Interception")]
        public double Interception { get; set; }

        [BsonElement("ForcedFumble")]
        public double ForcedFumble { get; set; }

        [BsonElement("FumbleRecovery")]
        public double FumbleRecovery { get; set; }

        [BsonElement("Touchdown")]
        public double Touchdown { get; set; }

        [BsonElement("Safety")]
        public double Safety { get; set; }

        [BsonElement("TackleBonus")]
        public double TackleBonus { get; set; }

        [BsonElement("SackBonus")]
        public double SackBonus { get; set; }


    }
}
