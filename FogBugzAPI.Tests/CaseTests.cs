using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogBugzAPI.FogBugzClient;
using FogBugzAPI.FogBugzClient.Command;
using FogBugzAPI.Model.Cases.Fields;
using NUnit.Framework;

namespace FogBugzAPI.Tests
{
    [TestFixture]
    public class CaseTests
    {
        [Test]
        public async void TestGetCase2()
        {
            Configuration cfg = Configuration.Load();
            FogBugzClientAsync fogBugzClient = new FogBugzClientAsync(cfg.BaseUrlList[0]);

            var auth = await fogBugzClient.LogonAsync(cfg.BaseUrlList[0]);

            var @case = await fogBugzClient.ExecuteAsync(new CaseCommands.GetCaseCommand(1, new List<CaseFieldName>((CaseFieldName[])Enum.GetValues(typeof(CaseFieldName))) ));

            Assert.NotNull(@case);
            Assert.AreEqual(2,@case.GetFogBugzCaseField(CaseFieldName.PersonOpenedById).Value.GetValue<int>());

            await fogBugzClient.LogoffAsync();

        }
    }
}
