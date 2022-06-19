﻿using MyBlazor.Shared.DataClass;
public class StateContainer
{
    //GA UI
    public void UIStartGA()
    {
        GAbtnType = "is-danger  is-loading";
        GAbtnstatusCss = "title=\"Disabled button\" disabled";
        NotifyStateChanged();
    }

    public void UIFinishGA()
    {
        GAbtnType = "is-primary";
        GAbtnstatusCss = "";
        NotifyStateChanged();
        //Console.WriteLine("...");
    }
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
    public int a = 0;
    public void A()
    {
        a++;
        NotifyStateChanged();
    }
    public int GetIteratives()
    {
        return iteratives;
    }

    private TimeSpan bestDueTime = TimeSpan.Zero;
    public void SetBsetDueTime(TimeSpan bestDueTime)
    {
        this.bestDueTime = bestDueTime;
        NotifyStateChanged();
    }
    public TimeSpan GetBsetDueTime()
    {
        return bestDueTime;
    }
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
    private List<WoInfo> woinfos = new List<WoInfo>();
    #region WO setting
    public List<WoInfo> GetWoInfo()
    {
        return woinfos ?? new List<WoInfo>();
    }
    
    public void AddWoInfo(WoInfo woInfo)
    {
        if (!woinfos.Exists(x => x.machine == woInfo.machine && x.wo == woInfo.wo))
        {
            woinfos.Add(woInfo);
            NotifyStateChanged();
        }
        else
        {

        }
    }

    public void SetWOInfos(List<WoInfo> newRecord)
    {
        ClearWoInfo();
        woinfos = newRecord;
        NotifyStateChanged();
    }

    public void ClearWoInfo()
    {
        woinfos = new List<WoInfo>();
        NotifyStateChanged();
    }
    #endregion
    public event Action? OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
    }

