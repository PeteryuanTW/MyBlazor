using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public static class GenericAlgo
    {
		public static Dictionary<(string, string), DateTime> InitMachineNextAvailable(List<MachineData> machineDatas)
		{
			Dictionary<(string, string), DateTime> res = new();
			foreach (MachineData machineData in machineDatas)
			{
				res.Add((machineData.machineName, machineData.index), machineData.nextAvailableTime);
			}
			return res;
		}
		public static Dictionary<string, DateTime> InitWONextAvailable(List<WOJobs> WOJobs)
		{
			Dictionary<string, DateTime>  res = new();
			foreach (WOJobs wojobs in WOJobs)
			{
				res.Add(wojobs.wo, DateTime.MinValue);
			}
			return res;
		}

		public static List<int> InitTypeJobCounts(List<WOJobs> allWOJobs)
		{
			List<int> res = new();
			for (int i = 0; i < allWOJobs[0].processCost.Count; i++)
			{
				int typeJobs = 0;//amount of each type of jobs
				for (int j = 0; j < allWOJobs.Count; j++)
				{
					if (allWOJobs[j].processCost[i] != TimeSpan.Zero)
					{
						typeJobs++;
					}
				}
				res.Add(typeJobs);
			}
			return res;
		}
		public static List<List<Job>> InitMachineBufferByJobs(List<WOJobs> allWOJobs, List<int> typeJobsCount, Dictionary<string, int> machineTypesAndCount)
		{
			List<List<Job>> res = new();

			for (int i = 0; i < allWOJobs[0].processCost.Count; i++)
			{
				int typeAmount = machineTypesAndCount.ElementAt(i).Value;
				List<Job> tmp = Enumerable.Repeat(new Job(), typeJobsCount[i] * typeAmount).ToList();
				res.Add(tmp);
			}
			return res;
		}

		public static List<List<Job>> CopyMachineBuffer(List<List<Job>> jobs)
		{
			List<List<Job>>  res = new();
			foreach (List<Job> jobList in jobs)
			{
				res.Add(jobList.ToList());
			}
			return res;
		}

		//assign type jobs in wo to empty machine list with index
		public static void AssignJobsToMachineBuffer(List<WOJobs> allWOJobs, ref List<List<Job>> jobTypeMachineList)
		{
			for (int i = 0; i < allWOJobs.Count; i++)//get job from wo_job list
			{
				for (int j = 0; j < allWOJobs[i].processCost.Count; j++)//job types
				{
					if (allWOJobs[i].processCost[j] != TimeSpan.Zero)
					{
						int totalIndex;
						int index;
						do
						{
							totalIndex = jobTypeMachineList[j].Count;
							Random r = new Random();
							index = r.Next(0, totalIndex);
						} while (!jobTypeMachineList[j][index].idle);
						jobTypeMachineList[j][index] = new Job(allWOJobs[i].wo, 0, allWOJobs[i].processCost[j]);
					}
				}

			}
		}
		//assign type jobs in wo to machine list with index
		public static void ReassignJobsToMachineBuffer(List<WOJobs> allWOJobs, ref List<List<Job>> jobTypeMachineList, Dictionary<string, int> machineCounts)
		{
			for (int i = 0; i < allWOJobs.Count; i++)//get job from wo_job list
			{
				for (int j = 0; j < allWOJobs[i].processCost.Count; j++)//job types
				{
					if (allWOJobs[i].processCost[j] != TimeSpan.Zero)
					{
						int totalIndex;
						int iii;
						do
						{
							totalIndex = jobTypeMachineList[j].Count;
							Random r = new Random();
							iii = r.Next(0, totalIndex);
						} while (!jobTypeMachineList[j][iii].idle || !CheckLaterThanFixJobs(iii, machineCounts.ElementAt(j).Value, jobTypeMachineList[i]));
						jobTypeMachineList[j][iii] = new Job(allWOJobs[i].wo, 1, allWOJobs[i].processCost[j]);
					}
				}

			}
		}
		//check assign position in machine is valid
		private static bool CheckLaterThanFixJobs(int index, int typeMachineCounts, List<Job> jobTypeMachineList)
		{
			bool res = true;
			int amountInOneMachine = jobTypeMachineList.Count/ typeMachineCounts;
			int end = index / amountInOneMachine * amountInOneMachine + amountInOneMachine;
			for (int i = index; i < end; i++)
			{
				if (!jobTypeMachineList[i].idle && jobTypeMachineList[i].start != DateTime.MinValue)//only new job behind is allowed
				{
					res = false;
					break;
				}
			}
			return res;
		}
		

		public static void FillJobsTimes(ref List<List<Job>> jobTypeMachineList, List<MachineTypeList> machineTypeLists, List<int> typeJobsCount, ref Dictionary<(string, string), DateTime> machineNextAvailable, ref Dictionary<string, DateTime> woNextAvailable)
		{
			for (int i = 0; i < jobTypeMachineList.Count; i++)
			{
				string type = machineTypeLists[i].machineType;
				int typeAmount = typeJobsCount[i];
				for (int j = 0; j < jobTypeMachineList[i].Count; j++)
				{
					if (!jobTypeMachineList[i][j].idle && jobTypeMachineList[i][j].start == DateTime.MinValue)
					{
						int typeIndex = j / typeAmount;
						string index = machineTypeLists[i].machineIndexList[typeIndex];

						Job currentJob = jobTypeMachineList[i][j];
						currentJob.machine = type;
						currentJob.index = index;

						if (!woNextAvailable.ContainsKey(currentJob.wo))
						{
							woNextAvailable.Add(currentJob.wo, DateTime.MinValue);
						}
						DateTime startAt = machineNextAvailable[(type, index)] > woNextAvailable[currentJob.wo] ? machineNextAvailable[(type, index)] : woNextAvailable[currentJob.wo];

						currentJob.SetStartTime(startAt);
						machineNextAvailable[(type, index)] = currentJob.end;
						woNextAvailable[currentJob.wo] = currentJob.end;

					}
				}
			}
		}

		public static TimeSpan CalculateDueTime(List<WOJobs> allWOJobs, Dictionary<string, DateTime> current_woNextAvailable)
		{
			TimeSpan res = TimeSpan.Zero;
			foreach (WOJobs woJob in allWOJobs)
			{
				DateTime expect = woJob.dueTime;
				DateTime actual = current_woNextAvailable[woJob.wo];
				if (actual > expect)
				{
					res += (actual - expect);
				}
			}
			return res;
		}

		public static List<SchedulingHistory> ReorderPopluationIndex(List<SchedulingHistory> population)
		{
			List<SchedulingHistory> orderPopulation = population.OrderBy(x => x.dueTime).ToList();
			for (int i = 0; i < orderPopulation.Count; i++)
			{
				orderPopulation[i].index = i + 1;
			}
			return orderPopulation;
		}

		public static List<List<Job>> GetChildChromosomeByRoulette(List<SchedulingHistory> schedulingHistories, List<double> priorityList)
		{
			List<List<Job>> res = new();
			Random r = new();
			double randomDouble = r.NextDouble();
			if (randomDouble <= priorityList[0])
			{
				res = schedulingHistories[0].jobs;
				return res;
			}
			for (int i = 0; i < priorityList.Count-1; i++)
			{
				if (randomDouble > priorityList[i] && randomDouble < priorityList[i + 1])
				{
					res = schedulingHistories[i + 1].jobs;
					return res;
				}
			}

			res = schedulingHistories[priorityList.Count-1].jobs;
			return res;
		}

		public static List<List<List<Job>>> Crossover(List<List<Job>> child1, List<List<Job>> child2)
		{
			List<List<List<Job>>> res = new();
			List<Job> tmp = new();
			Random r = new();
			int index = r.Next(0, child1.Count);
			
			tmp = child1[index].ToList();
			child1[index] = child2[index].ToList();
			child2[index] = tmp.ToList();

			res.Add(child1);
			res.Add(child2);

			foreach (List<List<Job>> a in res)
			{
				foreach (List<Job> b in a)
				{
					foreach (Job c in b)
					{
						if (!c.idle)
						{
							c.start = DateTime.MinValue;
						}
					}
				}
			}
			return res;
		}
	}
}
