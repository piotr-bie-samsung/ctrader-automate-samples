﻿using cAlgo.API;
using cAlgo.API.Internals;

namespace cAlgo
{
    /// <summary>
    /// This sample indicator shows how to get a symbol ticks data and handle its tick events
    /// </summary>
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class TickSample : Indicator
    {
        private Ticks _ticks;

        [Parameter("Symbol Name", DefaultValue = "EURUSD")]
        public string InputSymbolName { get; set; }

        protected override void Initialize()
        {
            // Getting a symbol ticks data
            _ticks = MarketData.GetTicks(InputSymbolName);
            // Subscribing to upcoming ticks
            _ticks.Tick += Ticks_Tick;

            _ticks.HistoryLoaded += Ticks_HistoryLoaded;
            // You can also pass a callback method instead of subscribing to HistoryLoaded event
            //_ticks.LoadMoreHistoryAsync(Ticks_HistoryLoaded);
            _ticks.LoadMoreHistoryAsync();

            _ticks.Reloaded += Ticks_Reloaded;
        }

        private void Ticks_Reloaded(TicksHistoryLoadedEventArgs obj)
        {
            Print("Ticks got reloaded");
        }

        private void Ticks_HistoryLoaded(TicksHistoryLoadedEventArgs obj)
        {
            Print("New ticks loaded: #", obj.Count);
        }

        private void Ticks_Tick(TicksTickEventArgs obj)
        {
            // Printing Last tick inside Ticks collection
            Print(obj.Ticks.LastTick);
        }

        public override void Calculate(int index)
        {
        }
    }
}