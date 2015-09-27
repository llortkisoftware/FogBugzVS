using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FogBugzAPI.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        private static string appData = Configuration.CONFIG_PATH;
        private string singleUrlConfigurationFile = appData + "/test-singleurl-" + Configuration.CONFIG_FILENAME;
        private string multiUrlConfigurationFile = appData + "/test-multiurl-" + Configuration.CONFIG_FILENAME;
        private readonly string originalConfigurationFile = appData + "/" + Configuration.CONFIG_FILENAME;
        private readonly string backupConfigurationFile = appData + "/test-backup-" + Configuration.CONFIG_FILENAME;
        private bool hadOriginalSettings = false; 

        [TestFixtureSetUp]
        public void BackupOriginalConfiguration()
        {
            try
            {
                if (File.Exists(originalConfigurationFile))
                {
                    hadOriginalSettings = true;

                    if (File.Exists(backupConfigurationFile))
                    {
                        File.Delete(backupConfigurationFile);
                    }

                    File.Copy(originalConfigurationFile, backupConfigurationFile);
                    File.Delete(originalConfigurationFile);
                }
            }
            catch (Exception ex)
            {
                string error = "Error creating backup configuration file: " + ex.Message;
                try
                {
                    if (!File.Exists(originalConfigurationFile))
                    {
                        File.Move(backupConfigurationFile, originalConfigurationFile);
                    }
                }
                catch (Exception ex2)
                {
                    error += "\nError restoring original configuration file: " + ex2.Message;
                }

                Assert.Fail(error);
            }
        }

        [TestFixtureTearDown]
        public void RestoreOriginalConfiguration()
        {
            try
            {

                if (File.Exists(backupConfigurationFile))
                {
                    if (hadOriginalSettings)
                    {
                        File.Copy(backupConfigurationFile, originalConfigurationFile, true);
                    }
                    else
                    {
                        File.Delete(backupConfigurationFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Error restoring backup configuration file: " + ex.Message);
            }
        }

        readonly FogBugzUrl _testFogBugzUrl = new FogBugzUrl() { BaseUrl = "https://test.fogbugz.com", DefaultUsername = "test@test.com", DisplayName = "Test" };
        readonly FogBugzUrl _test2FogBugzUrl = new FogBugzUrl() { BaseUrl = "https://test2.fogbugz.com", DefaultUsername = "test2@test2.com", DisplayName = "Test2" };


        [Test]
        public void TestDefaultSaveThrowsNoExceptions()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.Save();

            try
            {
                File.Delete(originalConfigurationFile);
            }
            catch
            {
                // ignored
            }
        }

        [Test]
        public void TestDefaultLoadThrowsNoExceptions()
        {
           Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.Save();

            Configuration loadedCfg = Configuration.Load();
            
            try
            {
                File.Delete(originalConfigurationFile);
            }
            catch
            {
                // ignored
            }
        }

        [Test]
        public void TestCustomSaveThrowsNoExceptions()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.Save(singleUrlConfigurationFile);

            try
            {
                File.Delete(singleUrlConfigurationFile);
            }
            catch
            {
                // ignored
            }
        }

        [Test]
        public void TestCustomLoadThrowsNoExceptions()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.Save(singleUrlConfigurationFile);

            Configuration loadedCfg = Configuration.Load(singleUrlConfigurationFile);

            try
            {
                File.Delete(singleUrlConfigurationFile);
            }
            catch
            {
                // ignored
            }
        }

        [Test]
        public void TestSingleUrlConfigurationSaveThenLoad()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.Save(singleUrlConfigurationFile);
            
            Configuration loadedCfg = Configuration.Load(singleUrlConfigurationFile);
            Assert.IsTrue(loadedCfg.BaseUrlList.Count == 1, "Invalid BaseUrlList size");
            Assert.IsTrue(loadedCfg.BaseUrlList[0].Equals(_testFogBugzUrl), loadedCfg.BaseUrlList[0]  + " != " + _testFogBugzUrl);
        }

        [Test]
        public void TestMultipleUrlConfigurationSaveThenLoad()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.BaseUrlList.Add(_test2FogBugzUrl);
            cfg.Save(multiUrlConfigurationFile);
            
            Configuration loadedCfg = Configuration.Load(multiUrlConfigurationFile);
            Assert.IsTrue(loadedCfg.BaseUrlList.Count == 2, "Invalid BaseUrlList size");
            Assert.IsTrue(loadedCfg.BaseUrlList[0].Equals(_testFogBugzUrl), loadedCfg.BaseUrlList[0] + " != " + _testFogBugzUrl);
            Assert.IsTrue(loadedCfg.BaseUrlList[1].Equals(_test2FogBugzUrl), loadedCfg.BaseUrlList[1] + " != " + _test2FogBugzUrl);

        }

        [Test]
        public void TestDefaultLocationSaveThenLoad()
        {
            Configuration cfg = new Configuration();
            cfg.BaseUrlList.Add(_testFogBugzUrl);
            cfg.BaseUrlList.Add(_test2FogBugzUrl);
            cfg.Save();

            Configuration loadedCfg = Configuration.Load();
            Assert.IsTrue(loadedCfg.BaseUrlList.Count == 2, "Invalid BaseUrlList size");
            Assert.IsTrue(loadedCfg.BaseUrlList[0].Equals(_testFogBugzUrl), loadedCfg.BaseUrlList[0] + " != " + _testFogBugzUrl);
            Assert.IsTrue(loadedCfg.BaseUrlList[1].Equals(_test2FogBugzUrl), loadedCfg.BaseUrlList[1] + " != " + _test2FogBugzUrl);

        }
    }
}
