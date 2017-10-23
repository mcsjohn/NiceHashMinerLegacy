﻿using NiceHashMiner.Configs;
using NiceHashMiner.Enums;
using NiceHashMiner.Miners.Grouping;
using NiceHashMiner.Miners.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NiceHashMiner.Miners {
    public class ClaymoreCryptoNightMiner : ClaymoreBaseMiner {

        const string _LOOK_FOR_START = "XMR - Total Speed:";
        public ClaymoreCryptoNightMiner()
            : base("ClaymoreCryptoNightMiner", _LOOK_FOR_START) {
        }

        protected override double DevFee() {
            return 1.0;
        }

        public override void Start(string url, string btcAdress, string worker) {
            string username = GetUsername(btcAdress, worker);
            LastCommandLine = " " + GetDevicesCommandString() + " -mport -" + APIPort + " -xpool " + url + " -xwal " + username + " -xpsw x -dbg -1";
            ProcessHandle = _Start();
        }

        // benchmark stuff

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time) {
            benchmarkTimeWait = time; // Takes longer as of v10

            // network workaround
            string url = Globals.GetLocationURL(algorithm.NiceHashID, ConfigManager.GeneralConfig.ServiceLocations[0].ServiceLocation, this.ConectionType);
            // demo for benchmark
            string username = Globals.DemoUser;
            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            string ret = " " + GetDevicesCommandString() + " -mport -" + APIPort + " -xpool " + url + " -xwal " + username + " -xpsw x";
            return ret;
        }

    }
}
