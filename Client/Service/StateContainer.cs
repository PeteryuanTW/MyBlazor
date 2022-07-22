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
        GABtnCssChange(gaBtnCss);
    }

    public void UIFinishGA()
    {
        gaBtnCss = "is-primary";
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
    private GAConfig config = new();
    public GAConfig GetGAConfig()
    {
        return config;
    }
    public void SetGAConfig(GAConfig newConfig)
    {
        if (newConfig.population > 0 && newConfig.iteration > 0 && newConfig.mutationRate >= 0.0 && newConfig.mutationRate <= 1.0)
        {
            config = newConfig;
        }
        GAConfigChange(config);
    }
    public event Action<GAConfig>? OnGAConfigChange;
    public void GAConfigChange(GAConfig newGAConfig) => OnGAConfigChange?.Invoke(newGAConfig);

    //GA history data
    private List<SchedulingHistory> schedulingHistories = new List<SchedulingHistory>();
    public void AddSchedulingData(SchedulingHistory schedulingHistory)
    {
        if (schedulingHistories.Count < 25)
        {
            schedulingHistories.Add(schedulingHistory);
        }
        else
        {
            int tmpIndex = schedulingHistories.IndexOf(schedulingHistories.MaxBy(x => x.dueTime));
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
    public SchedulingHistory GetSchedulingHistoryByDateTimeString(int index)
    {
        SchedulingHistory target = schedulingHistories.FirstOrDefault(x => x.index == index);
        return target;
    }
    public event Action<List<SchedulingHistory>>? OnGAHistoryDataChamge;
    public void GAHistoryDataChamge(List<SchedulingHistory> newSchedulingHistory) => OnGAHistoryDataChamge?.Invoke(newSchedulingHistory);
    private SchedulingHistory currentScheduling;

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
        new MachineData("m1", "1", 1),
        new MachineData("m1", "2", 1),
        new MachineData("m2", "1" ,1),
        new MachineData("m2", "2" ,1),
        new MachineData("m3", "1", 1),
        new MachineData("m3", "2", 1),
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
    public List<MachineTypeList> GetMachineTable()
    {
        List<MachineTypeList> res = new();
        foreach (MachineData machineData in allMachineStatus)
        {
            if (!res.Exists(x => x.machineType == machineData.machineName))
            {
                res.Add(new MachineTypeList(machineData.machineName));
            }
            res.FirstOrDefault(x => x.machineType == machineData.machineName).machineIndexList.Add(machineData.index);
        }
        foreach (var machine in res)
        {
            machine.machineIndexList.Sort();
        }
        res.OrderBy(x=>x.machineType);
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
    #region wo jobs
    private List<WOJobs> allWOJobs = new List<WOJobs>()
    {
        new WOJobs("wo1", new List<TimeSpan>{new TimeSpan(0, 10 ,0),  new TimeSpan(0, 20 ,0), new TimeSpan(0, 30 ,0),}, DateTime.Now.AddMinutes(10)),
        new WOJobs("wo2", new List<TimeSpan>{new TimeSpan(0, 20 ,0),  new TimeSpan(0, 50 ,0), new TimeSpan(0, 15 ,0),}, DateTime.Now.AddMinutes(20)),
        new WOJobs("wo3", new List<TimeSpan>{new TimeSpan(0, 5 ,0),  new TimeSpan(0, 30 ,0), new TimeSpan(0, 10 ,0),}, DateTime.Now.AddMinutes(15)),
        new WOJobs("wo4", new List<TimeSpan>{new TimeSpan(0, 30 ,0),  new TimeSpan(0, 10 ,0), new TimeSpan(0, 5 ,0),}, DateTime.Now.AddMinutes(50)),
        new WOJobs("wo5", new List<TimeSpan>{new TimeSpan(0, 20, 0),  new TimeSpan(0, 10 ,0), new TimeSpan(0, 50 ,0),}, DateTime.Now.AddMinutes(100)),
    };
    public List<WOJobs> GetWOJobs()
    {
        return allWOJobs ?? new List<WOJobs>();
    }
    public void AddWORequirements(WOJobs newWORequirements)
    {
        if (!allWOJobs.Exists(x => x.wo == newWORequirements.wo))
        {
            allWOJobs.Add(newWORequirements);
            NotifyStateChanged();
        }
    }

    #endregion


    public event Action? OnChange;
    public void NotifyStateChanged() => OnChange?.Invoke();
    }

