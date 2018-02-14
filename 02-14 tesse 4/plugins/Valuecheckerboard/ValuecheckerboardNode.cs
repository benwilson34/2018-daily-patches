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
	[PluginInfo(Name = "checkerboard", Category = "Value", Help = "Basic template with one value in/out", Tags = "c#")]
	#endregion PluginInfo
	public class ValuecheckerboardNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("cols", DefaultValue = 2)]
		public ISpread<int> FCols;
		[Input("rows", DefaultValue = 2)]
		public ISpread<int> FRows;
		
		[Output("Output")]
		public ISpread<double> FOutput;

		[Import()]
		public ILogger FLogger;
		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{	
			SpreadMax = FCols[0] * FRows[0];
			FOutput.SliceCount = SpreadMax;
			
			int cols = FCols[0];
			// given numCols and index i
			for(int i = 0; i < SpreadMax; i++){
				int offset = 0;        // if odd number of columns, no offset needed...
				if(cols % 2 == 0)   // if even number of columns, offset by row number
   					offset = (int) (i / cols);

				// setting the color...
				FOutput[i] = ((i + offset) % 2 == 0 ? 0.0f : 1.0f);
			}

			//FLogger.Log(LogType.Debug, "hi tty!");
		}
	}
}
