namespace OneBeyondApi.Helper
{
    public static class FineCalculator
    {
        private const decimal FinePerDay = 2; // $2 per day

        public static decimal CalculateFine(DateTime loanEndDate)
        {
            int daysOverdue = (DateTime.UtcNow - loanEndDate).Days;
            return daysOverdue > 0 ? daysOverdue * FinePerDay : 0;
        }
    }
}
