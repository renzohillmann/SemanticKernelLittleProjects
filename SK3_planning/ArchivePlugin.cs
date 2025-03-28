using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace SK3_planning
{
    public class ArchivePlugin // doesn't return any data
    {
        [KernelFunction("archive_data")]
        [Description("Saves data to a fine on your computer")]

        public async Task WriteData(Kernel kernel, string fileName, string data)
        {
            await File.WriteAllTextAsync(
                $@"C:\Users\RenzoStefanoHillmann\OneDrive - DATAKNOW S.A.S\Documentos\learning_projects\C#,SK,etc\ProjectSK2\SK3_planning\data\{fileName}.txt"
                , data);
        }


    }
}
