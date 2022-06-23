using MyBlazor.Shared.DataClass;
public class StateContainer
{
    //GA UI
    private int currentRound = 0;
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void SetCurrentRound(int i)
    {
        currentRound = i;
        CurrentRoundChange(i);
    }
    public event Action<int>? OnCurrentRoundChange;
    public void CurrentRoundChange(int currentRoung) => OnCurrentRoundChange?.Invoke(currentRoung);

    private string gaBtnCss = "is-primary";
    public string GetGABtnCSS()
    {
        return gaBtnCss;
    }
    public void UIStartGA()
    {
        gaBtnCss = "is-danger  is-loading title=\"Disabled button\" disabled";
        //GAbtnType = "is-danger  is-loading";
        //GAbtnstatusCss = "title=\"Disabled button\" disabled";
        GABtnCssChange(gaBtnCss);
    }

    public void UIFinishGA()
    {
        gaBtnCss = "is-primary";
        //GAbtnType = "is-primary";
        //GAbtnstatusCss = "";
        GABtnCssChange(gaBtnCss);
    }
    public event Action<string>? OnGABtnCssChange;
    public void GABtnCssChange(string newGABtnCssChange) => OnGABtnCssChange?.Invoke(gaBtnCss);
    
    
    private string GAbtnType = "is-primary";
    public string GetGAbtnType()
    {
        return GAbtnType;
    }
    private string GAbtnstatusCss = "";
    public string GetGAbtnstatusCss()
    {
        return GAbtnstatusCss;
    }
    //algo configs
    private int iteratives = 1000;
    public int GetIterations()
    {
        return iteratives;
    }
    //GA history data
    private List<SchedulingHistory> schedulingHistories = new List<SchedulingHistory>();
    public void AddSchedulingData(SchedulingHistory schedulingHistory)
    {
        if (schedulingHistories.Count < 10)
        {
            schedulingHistories.Add(schedulingHistory);
        }
        else
        {
            int tmpIndex = schedulingHistories.IndexOf(schedulingHistories.MinBy(x => x.dataTimeID));
            if (tmpIndex != -1)
            {
                schedulingHistories[tmpIndex] = schedulingHistory;
            }
        }
        GAHistoryDataChamge(schedulingHistories);
    }
    public List<SchedulingHistory> GetSchedulingData()
    {
        return schedulingHistories;
    }
    public SchedulingHistory GetSchedulingHistoryByDateTimeString(DateTime dateTime)
    {
        SchedulingHistory target = schedulingHistories.FirstOrDefault(x => x.dataTimeID.ToString("yyyy MM dd HH mm ss") == dateTime.ToString("yyyy MM dd HH mm ss"));
        return target;
    }
    public event Action<List<SchedulingHistory>>? OnGAHistoryDataChamge;
    public void GAHistoryDataChamge(List<SchedulingHistory> newSchedulingHistory) => OnGAHistoryDataChamge?.Invoke(newSchedulingHistory);
    private SchedulingHistory currentScheduling = new SchedulingHistory();

    public SchedulingHistory GetCurrentScheduling()
    {
        return currentScheduling;
    }
    public void SetCurrentScheduling(SchedulingHistory newScheduling)
    {
        currentScheduling = newScheduling;
        CurrentSchedulingChange(currentScheduling);
    }
    public event Action<SchedulingHistory>? OnCurrentSchedulingChange;
    public void CurrentSchedulingChange(SchedulingHistory newScheduling) => OnCurrentSchedulingChange?.Invoke(newScheduling);


    #region machine data
    private List<MachineData> allMachineStatus = new List<MachineData>() 
    {
        new MachineData("m1", 1, 1),
        new MachineData("m1", 2, 1),
        new MachineData("m2", 1 ,1),
        new MachineData("m2", 2 ,1),
        new MachineData("m3", 1, 1),
        new MachineData("m3", 2, 1),
    };
    public List<MachineData> GetMachineData()
    {
        return allMachineStatus ?? new List<MachineData>();
    }
    public Dictionary<string, int> GetMachineTypeCount()
    {
        Dictionary<string, int> res = new Dictionary<string, int>();
        foreach (var line in allMachineStatus
            .GroupBy(x => x.machineName)
            .Select(y => new {type = y.Key, count = y.Count()})
            .OrderBy(x => x.type))
        {
            res.Add(line.type, line.count);
        }

        return res;
    }
    public void AddMachineData(MachineData machineData)
    {
        if (!allMachineStatus.Exists(x => x.machineName == machineData.machineName && x.index == machineData.index))
        {
            allMachineStatus.Add(machineData);
            NotifyStateChanged();
        }
        else
        {

        }
    }
    #endregion
    #region wo requirements
    private List<WORequirements> allWORequirements = new List<WORequirements>()
    {
        new WORequirements("wo1", new List<TimeSpan>{new TimeSpan(0, 10 ,0),  new TimeSpan(0, 20 ,0), new TimeSpan(0, 30 ,0),}, DateTime.Now.AddMinutes(10)),
        new WORequirements("wo2", new List<TimeSpan>{new TimeSpan(0, 20 ,0),  new TimeSpan(0, 50 ,0), new TimeSpan(0, 15 ,0),}, DateTime.Now.AddMinutes(20)),
        new WORequirements("wo3", new List<TimeSpan>{new TimeSpan(0, 5 ,0),  new TimeSpan(0, 30 ,0), new TimeSpan(0, 10 ,0),}, DateTime.Now.AddMinutes(15)),
        new WORequirements("wo4", new List<TimeSpan>{new TimeSpan(0, 30 ,0),  new TimeSpan(0, 10 ,0), new TimeSpan(0, 5 ,0),}, DateTime.Now.AddMinutes(50)),
        new WORequirements("wo5", new List<TimeSpan>{new TimeSpan(0, 20 ,0),  new TimeSpan(0, 10 ,0), new TimeSpan(0, 50 ,0),}, DateTime.Now.AddMinutes(100)),
    };
    public List<WORequirements> GetWORequirements()
    {
        return allWORequirements ?? new List<WORequirements>();
    }
    public void AddWORequirements(WORequirements newWORequirements)
    {
        if (!allWORequirements.Exists(x => x.wo == newWORequirements.wo))
        {
            allWORequirements.Add(newWORequirements);
            NotifyStateChanged();
        }
    }

    #endregion


    public event Action? OnChange;
    public void NotifyStateChanged() => OnChange?.Invoke();
    }

