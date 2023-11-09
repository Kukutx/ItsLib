namespace ItsLib.WebApi.Models
{
    public class StatisticsModel
    {
        public float Average { get; set; }


        public static int Days(DateTime? date)
        {
            DateTime? now = DateTime.Now;
            TimeSpan differenza = now.Value - date.Value;
            int differenzaInGiorni = differenza.Days + 1;
            return differenzaInGiorni;
        }
    }
}
