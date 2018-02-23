#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "PickSlices", Category = "Value", Help = "Basic template with one value in/out", Tags = "c#")]
	#endregion PluginInfo
	public class ValuePickSlicesNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input Spread", DefaultValue = 1.0)]
		public ISpread<double> FInputSpread;

		[Input("Keeps", DefaultValue = 1)]
		public ISpread<int> FKeeps;
		
		[Output("Output")]
		public ISpread<double> FOutput;

		[Import()]
		public ILogger FLogger;
		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			
			int count = 0;
			for (int i = 0; i < SpreadMax; i++){
				if(FKeeps[i] == 1){
					FOutput[count] = FInputSpread[i];
					count++;
				}
			}
			
			FOutput.SliceCount = count;

			//FLogger.Log(LogType.Debug, "hi tty!");
		}
	}
}
