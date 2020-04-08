using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class DateTimeExtension
    {

        public static DateTime FirstMonthDay(this DateTime dateTime)
            => dateTime.Date.AddDays(-dateTime.Date.Day + 1).AbsoluteStart();

        public static DateTime LastMonthDay(this DateTime dateTime)
        {
            int totalDias = dateTime.TotalMonthDays();
            return dateTime.Date.FirstMonthDay().AddDays(totalDias - 1).AbsoluteStart();
        }

        public static int TotalMonthDays(this DateTime dateTime)
            => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        public static IEnumerable<DateTime> DateRange(this DateTime fromDate, DateTime toDate)
            => Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1).Select(d => fromDate.AddDays(d));

        public static DateTime AbsoluteStart(this DateTime dateTime)
            => dateTime.Date;

        public static int DaysInMonth(this DateTime dateTime)
            => dateTime.Date.FirstMonthDay().AddMonths(1).AddDays(-1).Day;

        public static DateTime AbsoluteEnd(this DateTime dateTime)
            => AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);

        public static DateTime[] ListarDiasDoMes(this DateTime data)
            => Enumerable.Range(0, data.TotalMonthDays())
                .Select(x => data.FirstMonthDay()
                .AddDays(x)).ToArray();

        public static int BussinessDaysUntil(this DateTime date, DateTime until)
            => date.DateRange(until)
                .Where(x => x.DayOfWeek != DayOfWeek.Sunday && x.DayOfWeek != DayOfWeek.Saturday)
                .Count();

        public static string DescricaoMes(this DateTime data)
        {
            switch (data.Month)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Março";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
                default: return "Inválido";
            }
        }
        public static string DescricaoMesAbreviado(this DateTime data)
        {
            switch (data.Month)
            {
                case 1: return "Jan";
                case 2: return "Fev";
                case 3: return "Mar";
                case 4: return "Abr";
                case 5: return "Mai";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Ago";
                case 9: return "Set";
                case 10: return "Out";
                case 11: return "Nov";
                case 12: return "Dez";
                default: return "Inv.";
            }
        }

        public static string DescricaoDia(this DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Sunday: return "Domingo";
                case DayOfWeek.Monday: return "Segunda";
                case DayOfWeek.Tuesday: return "Terça";
                case DayOfWeek.Wednesday: return "Quarta";
                case DayOfWeek.Thursday: return "Quinta";
                case DayOfWeek.Friday: return "Sexta";
                case DayOfWeek.Saturday: return "Sábado";
                default: return "Inválido";
            }
        }

        public static string DescricaoDiaAbreviado(this DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Sunday: return "Dom";
                case DayOfWeek.Monday: return "Seg";
                case DayOfWeek.Tuesday: return "Ter";
                case DayOfWeek.Wednesday: return "Qua";
                case DayOfWeek.Thursday: return "Qui";
                case DayOfWeek.Friday: return "Sex";
                case DayOfWeek.Saturday: return "Sáb";
                default: return "Inv.";
            }
        }

        public static string ToStringNumbers(this DateTime data)
        {

            return data.ToString("ddMMyyyyHHmm");
        }

    }
}