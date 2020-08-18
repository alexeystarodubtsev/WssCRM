using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WssCRM.Models;
using System.IO;


namespace WssCRM.Processing
{
    public class Statistics
    {
        DBModels.ApplicationContext db;
        public Statistics(DBModels.ApplicationContext context)
        {
            db = context;
        }
        public List<StatisticStage> getStatistics(ChooseFilter f1)
        {
            List<string> nameStatistics = new List<string>();
            nameStatistics.Add("Средний процент");
            nameStatistics.Add("Количество");
            nameStatistics.Add("Общая продолжительность");
            //nameStatistics.Add("Средняя продолжительность");

            DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;

            List<StatisticStage> statisticStages = new List<StatisticStage>();
            var dbStages = db.Stages.Where(c => c.CompanyID == f1.Company.id && (f1.stage.id == -40 || f1.stage.id == c.Id));
            var dbManagers = db.Managers.Where(m => m.CompanyID == f1.Company.id && (f1.manager.id == -40 || f1.manager.id == m.Id));
            var dbCalls = db.Calls.Where(c => dbManagers.Any(m => m.Id == c.ManagerID)
                        && dbStages.Any(s => s.Id == c.StageID)
                        && c.Date >= f1.StartDate && c.Date <= f1.EndDate && !c.ParentCallID.HasValue);
            var queryPerMonthPerStage = from c in dbCalls
                        from point in db.Points.Where(p => p.CallID == c.Id || db.Calls.Any(child => child.ParentCallID == c.Id && p.CallID == child.Id))
                        from AbstractPoint in db.AbstractPoints.Where(p => p.Id == point.AbstractPointID)
                        group new {point.Value, AbstractPoint.maxMark} by new {c.Id, AbstractPoint.StageID, c.ManagerID, c.duration, c.Date} into pointsPerStagePerCall
                        group new {AVGPersStagePerCall = (double)pointsPerStagePerCall.Sum(p => p.Value) / pointsPerStagePerCall.Sum(p => p.maxMark),
                                    CallDuration = pointsPerStagePerCall.Key.duration
                        } by new { pointsPerStagePerCall.Key.StageID, pointsPerStagePerCall.Key.ManagerID, pointsPerStagePerCall.Key.Date.Month, pointsPerStagePerCall.Key.Date.Year} into byStage
                        from s in dbStages
                        where s.Id == byStage.Key.StageID
                        from m in dbManagers
                        where m.Id == byStage.Key.ManagerID
                        orderby s.Id, byStage.Key.Year * 100 + byStage.Key.Month, m.Id
                        
                          select new { Manager = m.name
                                    , qty = byStage.Count()
                                    , AvgPers = byStage.Sum(stage => stage.AVGPersStagePerCall) / byStage.Count()
                                    , StageName = s.Name
                                    , Period = info.MonthNames[byStage.Key.Month - 1]
                                    , numMonth = byStage.Key.Year * 100 + byStage.Key.Month
                            //        , Duration = byStage.Aggregate(new TimeSpan(), (sum, nextcall) => sum.Add(nextcall.CallDuration))
                            //, TotalDuration = g.Aggregate(new TimeSpan(), (sum, nextcall) => sum.Add(nextcall.duration))

                        };


            var queryPerMonthTotal = from c in dbCalls
                                     from point in db.Points.Where(p => p.CallID == c.Id || db.Calls.Any(child => child.ParentCallID == c.Id && p.CallID == child.Id))
                                     from AbstractPoint in db.AbstractPoints.Where(p => p.Id == point.AbstractPointID)
                                     group new { point.Value, AbstractPoint.maxMark } by new { c.Id, c.ManagerID, c.duration, c.Date } into pointsPerCall
                                     group new
                                     {
                                         AVGPerCall = (double)pointsPerCall.Sum(p => p.Value) / pointsPerCall.Sum(p => p.maxMark),
                                         CallDuration = pointsPerCall.Key.duration
                                     } by new { pointsPerCall.Key.ManagerID, pointsPerCall.Key.Date.Month, pointsPerCall.Key.Date.Year } into byManager
                                     from m in dbManagers
                                     where m.Id == byManager.Key.ManagerID

                                     select new
                                     {
                                         Manager = m.name
                                                 ,
                                         qty = byManager.Count()
                                                 ,
                                         AvgPers = byManager.Sum(call => call.AVGPerCall) / byManager.Count()
                                         ,

                                         StageName = "Все этапы"
                                         ,
                                         Period = info.MonthNames[byManager.Key.Month - 1]
                                         ,

                                         numMonth = byManager.Key.Year * 100 + byManager.Key.Month
                                                 
                                                 
                                            //  , Duration = byManager.Aggregate(new TimeSpan(), (sum, nextcall) => sum.Add(nextcall.CallDuration))
                                            //, TotalDuration = g.Aggregate(new TimeSpan(), (sum, nextcall) => sum.Add(nextcall.duration))

                                        };
            var totalQuery = queryPerMonthPerStage;
            if (f1.stage.id == -40)
            {
                totalQuery = queryPerMonthPerStage.Union(queryPerMonthTotal);
            }
            foreach (var manStage in totalQuery)
            {
                StatisticStage curStage;
                
                try
                {
                    curStage = statisticStages.Where(stage => stage.StageName == manStage.StageName).First();
                }
                catch (InvalidOperationException)
                {
                    statisticStages.Add(new StatisticStage(manStage.StageName));
                    curStage = statisticStages.Where(stage => stage.StageName == manStage.StageName).First();
                    curStage.Tables = new List<TableWithStatistic>();
                }
                

                foreach (var nameStat in nameStatistics)
                {

                    TableWithStatistic table;
                    try
                    {
                        table = curStage.Tables.Where(t => t.TableName == nameStat).First();
                    }
                    catch(InvalidOperationException)
                    {
                        curStage.Tables.Add(new TableWithStatistic(nameStat));
                        table = curStage.Tables.Where(t => t.TableName == nameStat).First();
                        table.Data = new List<RowInTableStatistic>();
                    }
                    table.Mode = "Month";
                    if (!table.Managers.Contains(manStage.Manager))
                    {
                        table.Managers.Add(manStage.Manager);
                    }
                    if (!table.Period.Contains(manStage.Period))
                    {
                        table.Period.Add(manStage.Period);
                        
                    }
                    RowInTableStatistic curRow;
                    CellInStatistic curCell;
                    if (!table.Data.Any(row => row.rowname == manStage.Manager))
                    {
                        curRow = new RowInTableStatistic(manStage.Manager);
                        table.Data.Add(curRow);
                        
                    }
                    curRow = table.Data.Where(row => row.rowname == manStage.Manager).First();
                    curCell = new CellInStatistic();
                    curCell.period = manStage.Period;
                    if (nameStat == nameStatistics[0])
                    {
                        curCell.value = manStage.AvgPers.ToString("P", CultureInfo.InvariantCulture);
                    }
                    if (nameStat == nameStatistics[1])
                    {
                        curCell.value = manStage.qty.ToString();
                    }
                    if (nameStat == nameStatistics[2])
                    {
                        curCell.value = "0:01:01";
                    }
                    curRow.Cells.Add(curCell);

                }
                   
            }
            foreach (var stage in statisticStages)
            {
                foreach (var table in stage.Tables)
                {
                    foreach (var row in table.Data)
                    {
                        foreach (var nameCol in table.Period)
                        {
                            if (!row.Cells.Any(c => c.period == nameCol))
                            {
                                CellInStatistic curCell = new CellInStatistic();
                                curCell.period = nameCol;

                                if (table.TableName == nameStatistics[0])
                                {
                                    curCell.value = "";
                                }
                                if (table.TableName == nameStatistics[1])
                                {
                                    curCell.value = "0";
                                }
                                if (table.TableName == nameStatistics[2])
                                {
                                    curCell.value = "0:00:00";
                                }
                                row.Cells.Add(curCell);
                            };
                        }
                        RowInTableStatistic newRow = new RowInTableStatistic(row.rowname);
                        for (int i = 0; i < table.Period.Count; i++)
                        {
                            
                            CellInStatistic newCell = new CellInStatistic();
                            newCell.period = table.Period[i];
                            newCell.value = row.Cells.Where(c => c.period == table.Period[i]).First().value;
                            newRow.Cells.Add(newCell);
                            
                        }
                        row.Cells = newRow.Cells;
                    }
                }
            }

                return statisticStages;
        }
    }
}
