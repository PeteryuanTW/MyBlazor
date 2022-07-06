using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public static class GenericAlgo
    {
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

		public static void ReassignJobsToMachineBuffer(List<WOJobs> allWOJobs, ref List<List<Job>> jobTypeMachineList, Dictionary<string, int> machineCounts)
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
						} while (!jobTypeMachineList[j][index].idle || !CheckLaterThanFixJobs(index, machineCounts.ElementAt(i).Value, jobTypeMachineList[i]));// 
						jobTypeMachineList[j][index] = new Job(allWOJobs[i].wo, 1, allWOJobs[i].processCost[j]);
					}
				}

			}
		}
		
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
	}
}
